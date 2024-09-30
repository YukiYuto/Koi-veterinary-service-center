using FirebaseAdmin.Auth;
using KoiVeterinaryServiceCenter.DataAccess.IRepository;
using KoiVeterinaryServiceCenter.DataAccess.Repository;
using KoiVeterinaryServiceCenter.Model.Domain;
using KoiVeterinaryServiceCenter.Model.DTO;
using KoiVeterinaryServiceCenter.Services.IServices;
using KoiVeterinaryServiceCenter.Utility.Constants;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json.Linq;
using System.Collections.Concurrent;


namespace KoiVeterinaryServiceCenter.Services.Services
{
    public class AuthService : IAuthService
    {
        private readonly IUserManagerRepository _userManagerRepository;
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly IUnitOfWork _unitOfWork;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly ITokenService _tokenService;


        private static readonly ConcurrentDictionary<string, (int Count, DateTime LastRequest)> ResetPasswordAttempts =
            new();

        public AuthService
        (
            IUserManagerRepository userManagerRepository,
            RoleManager<IdentityRole> roleManager,
            IUnitOfWork unitOfWork,
            UserManager<ApplicationUser> userManager,
            ITokenService tokenService
        )
        {
            _userManagerRepository = userManagerRepository;
            _roleManager = roleManager;
            _unitOfWork = unitOfWork;
            _userManager = userManager;
            _tokenService = tokenService;
        }


        /// <summary>
        /// Registers a new Customer in the system.
        /// </summary>
        /// <param name="registerCustomerDTO"></param>
        /// <returns></returns>
        public async Task<ResponseDTO> SignUpCustomer(RegisterCustomerDTO registerCustomerDTO)
        {
            try
            {
                // Kiểm tra email đã tồn tại
                var isEmailExit = await _userManagerRepository.FindByEmailAsync(registerCustomerDTO.Email);
                if (isEmailExit is not null)
                {
                    return new ResponseDTO()
                    {
                        Message = "Email is being used by another user",
                        Result = registerCustomerDTO,
                        IsSuccess = false,
                        StatusCode = 400
                    };
                }

                // Kiểm tra số điện thoại đã tồn tại
                var isPhoneNumberExit =
                    await _userManagerRepository.CheckIfPhoneNumberExistsAsync(registerCustomerDTO.PhoneNumber);
                if (isPhoneNumberExit)
                {
                    return new ResponseDTO()
                    {
                        Message = "Phone number is being used by another user",
                        Result = registerCustomerDTO,
                        IsSuccess = false,
                        StatusCode = 400
                    };
                }

                // Tạo đối tượng ApplicationUser mới
                ApplicationUser newUser = new ApplicationUser()
                {
                    Email = registerCustomerDTO.Email,
                    UserName = registerCustomerDTO.Email,
                    FullName = registerCustomerDTO.FullName,
                    Address = registerCustomerDTO.Address,
                    Country = registerCustomerDTO.Country,
                    Gender = registerCustomerDTO.Gender,
                    BirthDate = registerCustomerDTO.BirthDate,
                    PhoneNumber = registerCustomerDTO.PhoneNumber,
                    AvatarUrl = "",
                    LockoutEnabled = false
                };

                // Thêm người dùng mới vào database
                var createUserResult = await _userManagerRepository.CreateAsync(newUser, registerCustomerDTO.Password);

                // Kiểm tra lỗi khi tạo
                if (!createUserResult.Succeeded)
                {
                    return new ResponseDTO()
                    {
                        Message = "Create user failed",
                        IsSuccess = false,
                        StatusCode = 400,
                        Result = registerCustomerDTO
                    };
                }

                var user = newUser; 
                var isRoleExist = await _roleManager.RoleExistsAsync(StaticUserRoles.Customer);

                if (!isRoleExist)
                {
                    await _roleManager.CreateAsync(new IdentityRole(StaticUserRoles.Customer));
                }

                // Thêm role "Customer" cho người dùng
                var isRoleAdded = await _userManagerRepository.AddToRoleAsync(user, StaticUserRoles.Customer);

                if (!isRoleAdded.Succeeded)
                {
                    return new ResponseDTO()
                    {
                        Message = "Error adding role",
                        IsSuccess = false,
                        StatusCode = 500,
                        Result = registerCustomerDTO
                    };
                }

                // Lưu thay đổi vào cơ sở dữ liệu
                var isSuccess = await _unitOfWork.SaveAsync();
                return new ResponseDTO()
                {
                    Message = "User created successfully",
                    IsSuccess = true,
                    StatusCode = 200,
                    Result = registerCustomerDTO
                };
            }
            catch (Exception ex)
            {
                return new ResponseDTO()
                {
                    Message = ex.Message,
                    Result = registerCustomerDTO,
                    IsSuccess = false,
                    StatusCode = 500
                };
            }
        }


        /// <summary>
        /// Registers a new Doctor in the system.
        /// </summary>
        /// <param name="registerDoctorDTO"></param>
        /// <returns></returns>
        public async Task<ResponseDTO> SignUpDoctor(RegisterDoctorDTO registerDoctorDTO)
        {
            try
            {
                //Check email is exist
                var isEmailExit = await _userManagerRepository.FindByEmailAsync(registerDoctorDTO.Email);
                if (isEmailExit is not null)
                {
                    return new ResponseDTO()
                    {
                        Message = "Email is using by another user",
                        Result = registerDoctorDTO,
                        IsSuccess = false,
                        StatusCode = 400
                    };
                }

                //Check phone number is exist
                var isPhonenumerExit =
                    await _userManagerRepository.CheckIfPhoneNumberExistsAsync(registerDoctorDTO.PhoneNumber);
                if (isPhonenumerExit)
                {
                    return new ResponseDTO()
                    {
                        Message = "Phone number is using by another user",
                        Result = registerDoctorDTO,
                        IsSuccess = false,
                        StatusCode = 400
                    };
                }

                //Create new instance of ApplicationUser
                ApplicationUser newUser = new ApplicationUser()
                {
                    Email = registerDoctorDTO.Email,
                    UserName = registerDoctorDTO.Email,
                    FullName = registerDoctorDTO.FullName,
                    Address = registerDoctorDTO.Address,
                    Country = registerDoctorDTO.Country,
                    Gender = registerDoctorDTO.Gender,
                    BirthDate = registerDoctorDTO.BirthDate,
                    PhoneNumber = registerDoctorDTO.PhoneNumber,
                    AvatarUrl = "",
                    LockoutEnabled = false
                };

                //Create new User to db
                var createUserResult = await _userManagerRepository.CreateAsync(newUser, registerDoctorDTO.Password);

                //Check if error occur
                if (!createUserResult.Succeeded)
                {
                    //Return result internal service error 
                    return new ResponseDTO()
                    {
                        Message = "Create user failed",
                        IsSuccess = false,
                        StatusCode = 400,
                        Result = registerDoctorDTO
                    };
                }

                var user = await _userManagerRepository.FindByPhoneAsync(registerDoctorDTO.PhoneNumber);

                //Create new Doctor
                Doctor doctor = new Doctor()
                {
                    UserId = user.Id,
                    Specialization = registerDoctorDTO.Specialization,
                    Experience = registerDoctorDTO.Experience,
                    Degree = registerDoctorDTO.Degree,
                };

                var isRoleExist = await _roleManager.RoleExistsAsync(StaticUserRoles.Doctor);

                if (isRoleExist is false)
                {
                    await _roleManager.CreateAsync(new IdentityRole(StaticUserRoles.Doctor));
                }

                //Add role for the user 
                var isRoledAdd = await _userManagerRepository.AddToRoleAsync(user, StaticUserRoles.Doctor);

                if (!isRoledAdd.Succeeded)
                {
                    return new ResponseDTO()
                    {
                        Message = "Error adding role",
                        IsSuccess = false,
                        StatusCode = 500,
                        Result = registerDoctorDTO
                    };
                }

                //Create new Doctor relate with ApplicationUser
                var isDoctorAdd = await _unitOfWork.DoctorRepository.AddAsync(doctor);
                if (isDoctorAdd == null)
                {
                    return new ResponseDTO()
                    {
                        Message = "Failed to add doctor",
                        IsSuccess = false,
                        StatusCode = 500,
                        Result = registerDoctorDTO
                    };
                }

                // Save change to database
                var isSuccess = await _unitOfWork.SaveAsync();
                return new ResponseDTO()
                {
                    Message = "Create new user successfully",
                    IsSuccess = true,
                    StatusCode = 200,
                    Result = registerDoctorDTO
                };
            }
            catch (Exception ex)
            {
                return new ResponseDTO()
                {
                    Message = ex.Message,
                    Result = registerDoctorDTO,
                    IsSuccess = false,
                    StatusCode = 500
                };
            }
        }


        /// <summary>
        /// Sign in to the system
        /// </summary>
        /// <param name="signDto"></param>
        /// <returns></returns>
        public async Task<ResponseDTO> SignIn(SignDTO signDto)
        {
            try
            {
                var user = await _userManager.FindByEmailAsync(signDto.Email);
                if (user == null)
                {
                    return new ResponseDTO()
                    {
                        Message = "User does not exist!",
                        Result = null,
                        IsSuccess = false,
                        StatusCode = 404
                    };
                }

                var isPasswordCorrect = await _userManager.CheckPasswordAsync(user, signDto.Password);

                if (!isPasswordCorrect)
                {
                    return new ResponseDTO()
                    {
                        Message = "Incorrect email or password",
                        Result = null,
                        IsSuccess = false,
                        StatusCode = 400
                    };
                }

                if (!user.EmailConfirmed)
                {
                    return new ResponseDTO()
                    {
                        Message = "You need to confirm email!",
                        Result = null,
                        IsSuccess = false,
                        StatusCode = 401
                    };
                }

                if (user.LockoutEnd is not null)
                {
                    return new ResponseDTO()
                    {
                        Message = "User has been locked",
                        IsSuccess = false,
                        StatusCode = 403,
                        Result = null
                    };
                }

                var accessToken = await _tokenService.GenerateJwtAccessTokenAsync(user);
                var refreshToken = await _tokenService.GenerateJwtRefreshTokenAsync(user);
                //await _tokenService.StoreRefreshToken(user.Id, refreshToken);

                return new ResponseDTO()
                {
                    Result = new SignResponseDTO()
                    {
                        AccessToken = accessToken,
                        RefreshToken = refreshToken,
                    },
                    Message = "Sign in successfully",
                    IsSuccess = true,
                    StatusCode = 200
                };
            }
            catch
                (Exception e)
            {
                return new ResponseDTO()
                {
                    Message = e.Message,
                    IsSuccess = false,
                    StatusCode = 500,
                    Result = null
                };
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="signInByGoogleDto"></param>
        /// <returns></returns>
        public async Task<ResponseDTO> SignInByGoogle(SignInByGoogleDTO signInByGoogleDto)
        {
            try
            {
                //lấy thông tin từ google
                FirebaseToken googleTokenS =
                    await FirebaseAuth.DefaultInstance.VerifyIdTokenAsync(signInByGoogleDto.Token);
                string userId = googleTokenS.Uid;
                string email = googleTokenS.Claims["email"].ToString();
                string name = googleTokenS.Claims["name"].ToString();
                string avatarUrl = googleTokenS.Claims["picture"].ToString();

                //tìm kiem người dùng trong database
                var user = await _userManager.FindByEmailAsync(email);
                UserLoginInfo? userLoginInfo = null;
                if (user is not null)
                {
                    userLoginInfo = (await _userManager.GetLoginsAsync(user))
                        .FirstOrDefault(x => x.LoginProvider == StaticLoginProvider.Google);
                }

                if (user.LockoutEnd is not null)
                {
                    return new ResponseDTO()
                    {
                        Message = "User has been locked",
                        IsSuccess = false,
                        StatusCode = 403,
                        Result = null
                    };
                }

                if (user is not null && userLoginInfo is null)
                {
                    return new ResponseDTO()
                    {
                        Result = new SignResponseDTO()
                        {
                            RefreshToken = null,
                            AccessToken = null,
                        },
                        Message = "The email is using by another user",
                        IsSuccess = false,
                        StatusCode = 400
                    };
                }

                if (userLoginInfo is null && user is null)
                {
                    //tạo một user mới khi chưa có trong database
                    user = new ApplicationUser
                    {
                        Email = email,
                        FullName = name,
                        UserName = email,
                        AvatarUrl = avatarUrl,
                        EmailConfirmed = true
                    };

                    await _userManager.CreateAsync(user);
                    await _userManager.AddLoginAsync(user,
                        new UserLoginInfo(StaticLoginProvider.Google, userId, "GOOGLE"));
                }

                var accessToken = await _tokenService.GenerateJwtAccessTokenAsync(user);
                var refreshToken = await _tokenService.GenerateJwtRefreshTokenAsync(user);
                await _tokenService.StoreRefreshToken(user.Id, refreshToken);
                await _userManager.UpdateAsync(user);

                return new ResponseDTO()
                {
                    Result = new SignResponseDTO()
                    {
                        AccessToken = accessToken,
                        RefreshToken = refreshToken,
                    },
                    Message = "Sign in successfully",
                    IsSuccess = true,
                    StatusCode = 200
                };
            }
            catch (FirebaseAuthException e)
            {
                return new ResponseDTO()
                {
                    Result = new SignResponseDTO()
                    {
                        AccessToken = null,
                        RefreshToken = null,
                    },
                    Message = "Something went wrong",
                    IsSuccess = false,
                    StatusCode = 500
                };
            }
        }

        /// <summary>
        /// This method for check email exist or not
        /// </summary>
        /// <param name="email"></param>
        /// <returns></returns>
        public async Task<ResponseDTO> CheckEmailExist(string email)
        {
            try
            {
                var user = await _userManager.FindByEmailAsync(email);
                return new()
                {
                    Result = user is not null,
                    Message = user is null ? "Email does not exist" : "Email is existed",
                    IsSuccess = true,
                    StatusCode = 200
                };
            }
            catch (Exception e)
            {
                return new()
                {
                    Message = e.Message,
                    IsSuccess = false,
                    StatusCode = 500,
                    Result = null
                };
            }
        }


        /*
                //Forgot password
                private string ip;
                private string city;
                private string region;
                private string country;
                private const int MaxAttemptsPerDay = 3;

                public async Task<ResponseDTO> ForgotPassword(ForgotPasswordDTO forgotPasswordDto)
                {
                    try
                    {
                        // Tìm người dùng theo Email/Số điện thoại
                        var user = await _userManager.FindByEmailAsync(forgotPasswordDto.EmailOrPhone);
                        if (user == null)
                        {
                            user = await _userManager.Users.FirstOrDefaultAsync(
                                u => u.PhoneNumber == forgotPasswordDto.EmailOrPhone);
                        }

                        if (user == null || !user.EmailConfirmed)
                        {
                            return new ResponseDTO
                            {
                                IsSuccess = false,
                                Message = "No user found or account not activated.",
                                StatusCode = 400
                            };
                        }

                        // Kiểm tra giới hạn gửi yêu cầu đặt lại mật khẩu
                        var email = user.Email;
                        var now = DateTime.Now;

                        if (ResetPasswordAttempts.TryGetValue(email, out var attempts))
                        {
                            // Kiểm tra xem đã quá 1 ngày kể từ lần thử cuối cùng chưa
                            if (now - attempts.LastRequest >= TimeSpan.FromSeconds(1))
                            {
                                // Reset số lần thử về 0 và cập nhật thời gian thử cuối cùng
                                ResetPasswordAttempts[email] = (1, now);
                            }
                            else if (attempts.Count >= MaxAttemptsPerDay)
                            {
                                // Quá số lần reset cho phép trong vòng 1 ngày, gửi thông báo
                                await _emailService.SendEmailAsync(user.Email,
                                    "Password Reset Request Limit Exceeded",
                                    $"You have exceeded the daily limit for password reset requests. Please try again after 24 hours."
                                );

                                // Vẫn trong thời gian chặn, trả về lỗi
                                return new ResponseDTO
                                {
                                    IsSuccess = false,
                                    Message =
                                        "You have exceeded the daily limit for password reset requests. Please try again after 24 hours.",
                                    StatusCode = 429
                                };
                            }
                            else
                            {
                                // Chưa vượt quá số lần thử và thời gian chờ, tăng số lần thử và cập nhật thời gian
                                ResetPasswordAttempts[email] = (attempts.Count + 1, now);
                            }
                        }
                        else
                        {
                            // Email chưa có trong danh sách, thêm mới với số lần thử là 1 và thời gian hiện tại
                            ResetPasswordAttempts.AddOrUpdate(email, (1, now), (key, old) => (old.Count + 1, now));
                        }

                        // Tạo mã token
                        var token = await _userManager.GeneratePasswordResetTokenAsync(user);

                        // Gửi email chứa đường link đặt lại mật khẩu. //reset-password

                        var resetLink = $"https://nostran.w3spaces.com?token={token}&email={user.Email}";

                        // Lấy ngày hiện tại
                        var currentDate = DateTime.Now.ToString("MMMM d, yyyy");

                        var userAgent = _httpContextAccessor.HttpContext?.Request.Headers["User-Agent"];

                        // Lấy tên hệ điều hành
                        var operatingSystem = GetUserAgentOperatingSystem(userAgent);

                        // Lấy tên trình duyệt
                        var browser = GetUserAgentBrowser(userAgent);

                        // Lấy location
                        var url = "https://ipinfo.io/14.169.10.115/json?token=823e5c403c980f";
                        using (HttpClient client = new HttpClient())
                        {
                            HttpResponseMessage response = await client.GetAsync(url);
                            if (response.IsSuccessStatusCode)
                            {
                                string jsonContent = await response.Content.ReadAsStringAsync();
                                JObject data = JObject.Parse(jsonContent);

                                this.ip = data["ip"].ToString();
                                this.city = data["city"].ToString();
                                this.region = data["region"].ToString();
                                this.country = data["country"].ToString();
                            }
                            else
                            {
                                return new ResponseDTO
                                {
                                    IsSuccess = false,
                                    Message = "Error: Unable to retrieve data.",
                                    StatusCode = 400
                                };
                            }
                        }

                        // Gửi email chứa đường link đặt lại mật khẩu
                        await _emailService.SendEmailResetAsync(user.Email, "Reset password for your Cursus account", user,
                            currentDate, resetLink, operatingSystem, browser, ip, region, city, country);

                        // Helper functions (you might need to refine these based on your User-Agent parsing logic)
                        string GetUserAgentOperatingSystem(string userAgent)
                        {
                            // ... Logic to extract the operating system from the user-agent string
                            // Example:
                            if (userAgent.Contains("Windows")) return "Windows";
                            else if (userAgent.Contains("Mac")) return "macOS";
                            else if (userAgent.Contains("Linux")) return "Linux";
                            else return "Unknown";
                        }

                        string GetUserAgentBrowser(string userAgent)
                        {
                            // ... Logic to extract the browser from the user-agent string
                            // Example:
                            if (userAgent.Contains("Chrome")) return "Chrome";
                            else if (userAgent.Contains("Firefox")) return "Firefox";
                            else if (userAgent.Contains("Safari")) return "Safari";
                            else return "Unknown";
                        }

                        return new ResponseDTO
                        {
                            IsSuccess = true,
                            Message = "The password reset link has been sent to your email.",
                            StatusCode = 200
                        };
                    }
                    catch (Exception e)
                    {
                        return new ResponseDTO
                        {
                            IsSuccess = false,
                            Message = e.Message,
                            StatusCode = 500
                        };
                    }
                }*/
    }
}