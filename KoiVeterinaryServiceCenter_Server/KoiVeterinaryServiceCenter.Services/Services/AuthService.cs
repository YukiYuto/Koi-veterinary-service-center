using KoiVeterinaryServiceCenter.DataAccess.IRepository;
using KoiVeterinaryServiceCenter.DataAccess.Repository;
using KoiVeterinaryServiceCenter.Model.Domain;
using KoiVeterinaryServiceCenter.Model.DTO;
using KoiVeterinaryServiceCenter.Services.IServices;
using KoiVeterinaryServiceCenter.Utility.Constants;
using Microsoft.AspNetCore.Identity;


namespace KoiVeterinaryServiceCenter.Services.Services
{
    public class AuthService : IAuthService
    {
        private readonly IUserManagerRepository _userManagerRepository;
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly IUnitOfWork _unitOfWork;

        public AuthService
        (
            IUserManagerRepository userManagerRepository,
            RoleManager<IdentityRole> roleManager,
            IUnitOfWork unitOfWork
        )
        {
            _userManagerRepository = userManagerRepository;
            _roleManager = roleManager;
            _unitOfWork = unitOfWork;
        }

        public async Task<ResponseDTO> SignUpCustomer(RegisterCustomerDTO registerCustomerDTO)
        {
            try
            {
                //Check email is exist
                var isEmailExit = await _userManagerRepository.FindByEmailAsync(registerCustomerDTO.Email);
                if (isEmailExit is not null)
                {
                    return new ResponseDTO()
                    {
                        Message = "Email is using by another user",
                        Result = registerCustomerDTO,
                        IsSuccess = false,
                        StatusCode = 400
                    };
                }

                //Check phone number is exist
                var isPhonenumerExit =
                    await _userManagerRepository.CheckIfPhoneNumberExistsAsync(registerCustomerDTO.PhoneNumber);
                if (isPhonenumerExit)
                {
                    return new ResponseDTO()
                    {
                        Message = "Phone number is using by another user",
                        Result = registerCustomerDTO,
                        IsSuccess = false,
                        StatusCode = 400
                    };
                }

                //Create new instance of ApplicationUser
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

                //Create new User to db
                var createUserResult = await _userManagerRepository.CreateAsync(newUser, registerCustomerDTO.Password);

                //Check if error occur
                if (!createUserResult.Succeeded)
                {
                    //Return result internal service error 
                    return new ResponseDTO()
                    {
                        Message = "Create user failed",
                        IsSuccess = false,
                        StatusCode = 400,
                        Result = registerCustomerDTO
                    };
                }

                var user = await _userManagerRepository.FindByPhoneAsync(registerCustomerDTO.PhoneNumber);

                //Create new Doctor
                Customer customer = new Customer()
                {
                    UserId = user.Id,
                    
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
                        Result = registerCustomerDTO
                    };
                }

                //Create new Doctor relate with ApplicationUser
                var isDoctorAdd = await _unitOfWork.CustomerRepository.AddAsync(customer);
                if (isDoctorAdd == null)
                {
                    return new ResponseDTO()
                    {
                        Message = "Failed to add doctor",
                        IsSuccess = false,
                        StatusCode = 500,
                        Result = registerCustomerDTO
                    };
                }

                // Save change to database
                var isSuccess = await _unitOfWork.SaveAsync();
                return new ResponseDTO()
                {
                    Message = "Create new user successfully",
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
    

        //SignUpDoctor
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
    }
}
