using AutoMapper;
using HRMS.EntityDto;
using Microsoft.AspNetCore.Identity;
using HRMS.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace HRMS.Services
{
    public class UserService : IUserService
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly IMapper _mapper;
        private readonly IAuditLogService _auditLogService;
        private readonly ILogger<UserService> _logger;

        public UserService(UserManager<ApplicationUser> userManager, IMapper mapper, IAuditLogService auditLogService, ILogger<UserService> logger)
        {
            _auditLogService = auditLogService;
            _userManager = userManager;
            _mapper = mapper;
            _logger = logger;
        }

        public async Task<IEnumerable<UserDto>> GetAllUsersAsync()
        {
            var users = await _userManager.Users.ToListAsync();
            return _mapper.Map<List<UserDto>>(users);
        }

        public async Task<UserDto> GetUserByIdAsync(string id)
        {
            var user = await _userManager.FindByIdAsync(id);
            return _mapper.Map<UserDto>(user);
        }

        //public async Task CreateUserAsync(UserDto userDto)
        //{
        //    var user = _mapper.Map<ApplicationUser>(userDto);
        //    var result = await _userManager.CreateAsync(user);
        //    if (result.Succeeded)
        //    {
        //        _logger.LogInformation("User created successfully.");
        //    }
        //    else
        //    {
        //        _logger.LogError("Failed to create user: {Errors}", result.Errors);
        //        throw new ApplicationException("Failed to create user.");
        //    }
        //}
        // Services/UserService.cs
        public async Task CreateUserAsync(UserDto userDto)
        {
            var user = _mapper.Map<ApplicationUser>(userDto);
            var result = await _userManager.CreateAsync(user);
            if (result.Succeeded)
            {
                _logger.LogInformation("User created successfully.");
                await _auditLogService.LogActionAsync("CreateUser", userDto.Username, "User created.");
            }
            else
            {
                _logger.LogError("Failed to create user: {Errors}", result.Errors);
                throw new ApplicationException("Failed to create user.");
            }
        }

        public async Task UpdateUserAsync(UserDto userDto)
        {
            var user = await _userManager.FindByIdAsync(userDto.Id);
            if (user == null)
            {
                throw new ApplicationException($"User with ID {userDto.Id} not found.");
            }

            _mapper.Map(userDto, user);
            var result = await _userManager.UpdateAsync(user);
            if (!result.Succeeded)
            {
                throw new ApplicationException("Failed to update user.");
            }
        }

        public async Task DeleteUserAsync(string id)
        {
            var user = await _userManager.FindByIdAsync(id);
            if (user == null)
            {
                throw new ApplicationException($"User with ID {id} not found.");
            }

            var result = await _userManager.DeleteAsync(user);
            if (!result.Succeeded)
            {
                throw new ApplicationException("Failed to delete user.");
            }
        }

        public async Task AssignRoleAsync(string userId, string roleName)
        {
            var user = await _userManager.FindByIdAsync(userId);
            if (user == null)
            {
                throw new ApplicationException($"User with ID {userId} not found.");
            }

            var currentRoles = await _userManager.GetRolesAsync(user);
            var result = await _userManager.AddToRoleAsync(user, roleName);
            if (!result.Succeeded)
            {
                throw new ApplicationException($"Failed to assign role {roleName} to user.");
            }

            foreach (var role in currentRoles)
            {
                if (role != roleName)
                {
                    result = await _userManager.RemoveFromRoleAsync(user, role);
                    if (!result.Succeeded)
                    {
                        throw new ApplicationException($"Failed to remove role {role} from user.");
                    }
                }
            }
        }
    }
}
