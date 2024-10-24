using System.Collections.Concurrent;
using System.Security.Claims;
using System.Web;
using FirebaseAdmin.Auth;
using KoiVeterinaryServiceCenter.DataAccess.IRepository;
using KoiVeterinaryServiceCenter.Model.Domain;
using KoiVeterinaryServiceCenter.Models.Domain;
using KoiVeterinaryServiceCenter.Models.DTO;
using KoiVeterinaryServiceCenter.Models.DTO.Auth;
using KoiVeterinaryServiceCenter.Services.IServices;
using KoiVeterinaryServiceCenter.Utility.Constants;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace KoiVeterinaryServiceCenter.Services.Services;

public class AuthService : IAuthService
{
    //private readonly IUserManagerRepository _userManagerRepository;
    private readonly RoleManager<IdentityRole> _roleManager;
    private readonly IUnitOfWork _unitOfWork;
    private readonly UserManager<ApplicationUser> _userManager;
    private readonly ITokenService _tokenService;
    private readonly IFirebaseService _firebaseService;
    private readonly IEmailService _emailService;


    private static readonly ConcurrentDictionary<string, (int Count, DateTime LastRequest)> ResetPasswordAttempts =
        new();

    public AuthService
    (
        //IUserManagerRepository userManagerRepository,
        RoleManager<IdentityRole> roleManager,
        IUnitOfWork unitOfWork,
        UserManager<ApplicationUser> userManager,
        ITokenService tokenService,
        IFirebaseService firebaseService,
        IEmailService emailService
    )
    {
        //_userManagerRepository = userManagerRepository;
        _roleManager = roleManager;
        _unitOfWork = unitOfWork;
        _userManager = userManager;
        _tokenService = tokenService;
        _firebaseService = firebaseService;
        _emailService = emailService;
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
            var isEmailExit = await _userManager.FindByEmailAsync(registerCustomerDTO.Email);
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
            var isPhoneNumberExit = await _userManager.Users
                .AnyAsync(u => u.PhoneNumber == registerCustomerDTO.PhoneNumber);
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
                LockoutEnabled = false,
                EmailConfirmed = true
            };

            // Thêm người dùng mới vào database
            var createUserResult = await _userManager.CreateAsync(newUser, registerCustomerDTO.Password);

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
            var isRoleAdded = await _userManager.AddToRoleAsync(user, StaticUserRoles.Customer);

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
            var isEmailExit = await _userManager.FindByEmailAsync(registerDoctorDTO.Email);
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
            var isPhoneNumberExit = await _userManager.Users
                .AnyAsync(u => u.PhoneNumber == registerDoctorDTO.PhoneNumber);
            if (isPhoneNumberExit)
            {
                return new ResponseDTO()
                {
                    Message = "Phone number is being used by another user",
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
                LockoutEnabled = false,
                EmailConfirmed = true
            };

            //Create new User to db
            var createUserResult = await _userManager.CreateAsync(newUser, registerDoctorDTO.Password);

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

            var user = await _userManager.FindByEmailAsync(registerDoctorDTO.Email);

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
            var isRoledAdd = await _userManager.AddToRoleAsync(user, StaticUserRoles.Doctor);

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
            await _tokenService.StoreRefreshToken(user.Id, refreshToken);

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

    /// <summary>
    /// This API uploads the user's avatar to Firebase Storage.
    /// </summary>
    /// <param name="file"></param>
    /// <param name="User"></param>
    /// <returns></returns>
    public async Task<ResponseDTO> UploadUserAvatar(IFormFile file, ClaimsPrincipal User)
    {
        try
        {
            var userId = User.Claims.FirstOrDefault(x => x.Type == ClaimTypes.NameIdentifier)?.Value;

            if (userId is null)
            {
                throw new Exception("Not authentication!");
            }

            var user = await _userManager.FindByIdAsync(userId);

            if (user is null)
            {
                throw new Exception("User does not exist");
            }

            var responseDto = await _firebaseService.UploadImage(file, StaticFirebaseFolders.UserAvatars);

            if (!responseDto.IsSuccess)
            {
                throw new Exception("Image upload fail!");
            }

            user.AvatarUrl = responseDto.Result?.ToString();

            var updateResult = await _userManager.UpdateAsync(user);

            if (!updateResult.Succeeded)
            {
                throw new Exception("Update user avatar fail!");
            }

            return new ResponseDTO()
            {
                Message = "Upload user avatar successfully!",
                Result = null,
                IsSuccess = true,
                StatusCode = 200
            };
        }
        catch (Exception e)
        {
            return new ResponseDTO()
            {
                Message = e.Message,
                Result = null,
                IsSuccess = false,
                StatusCode = 500
            };
        }
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="User"></param>
    /// <returns></returns>
    public async Task<MemoryStream> GetUserAvatar(ClaimsPrincipal User)
    {
        try
        {
            var userId = User.Claims.FirstOrDefault(x => x.Type == ClaimTypes.NameIdentifier)?.Value;

            var user = await _userManager.FindByIdAsync(userId);

            var stream = await _firebaseService.GetImage(user.AvatarUrl);

            return stream;
        }
        catch (Exception e)
        {
            return null;
        }
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="email"></param>
    /// <param name="confirmationLink"></param>
    /// <returns></returns>
    public async Task<ResponseDTO> SendVerifyEmail(string email, string confirmationLink)
    {
        try
        {
            await _emailService.SendVerifyEmail(email, confirmationLink);
            return new()
            {
                Message = "Send verify email successfully",
                IsSuccess = true,
                StatusCode = 200,
                Result = null
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

    /// <summary>
    /// 
    /// </summary>
    /// <param name="userId"></param>
    /// <param name="token"></param>
    /// <returns></returns>
    public async Task<ResponseDTO> VerifyEmail(string userId, string token)
    {
        var user = await _userManager.FindByIdAsync(userId);

        if (user.EmailConfirmed)
        {
            return new ResponseDTO()
            {
                Message = "Your email has been confirmed!",
                IsSuccess = true,
                StatusCode = 200,
                Result = null
            };
        }

        string decodedToken = HttpUtility.UrlDecode(token);

        var confirmResult = await _userManager.ConfirmEmailAsync(user, decodedToken);

        if (!confirmResult.Succeeded)
        {
            return new()
            {
                Message = "Invalid token",
                StatusCode = 400,
                IsSuccess = false,
                Result = null
            };
        }

        return new()
        {
            Message = "Confirm Email Successfully",
            IsSuccess = true,
            StatusCode = 200,
            Result = null
        };
    }
}