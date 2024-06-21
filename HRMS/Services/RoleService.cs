using HRMS.EntityDto;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HRMS.Services
{
    public class RoleService : IRoleService
    {
        private readonly List<RoleDto> _roles = new List<RoleDto>();

        public Task<IEnumerable<RoleDto>> GetAllRolesAsync()
        {
            return Task.FromResult<IEnumerable<RoleDto>>(_roles);
        }

        public Task<RoleDto> GetRoleByIdAsync(int id)
        {
            var role = _roles.FirstOrDefault(r => r.RoleId == id);
            return Task.FromResult(role);
        }

        public Task CreateRoleAsync(RoleDto roleDto)
        {
            roleDto.RoleId = _roles.Any() ? _roles.Max(r => r.RoleId) + 1 : 1;
            _roles.Add(roleDto);
            return Task.CompletedTask;
        }

        public Task UpdateRoleAsync(RoleDto roleDto)
        {
            var role = _roles.FirstOrDefault(r => r.RoleId == roleDto.RoleId);
            if (role != null)
            {
                role.RoleName = roleDto.RoleName;
                role.Permissions = roleDto.Permissions;
            }
            return Task.CompletedTask;
        }

        public Task DeleteRoleAsync(int id)
        {
            var role = _roles.FirstOrDefault(r => r.RoleId == id);
            if (role != null)
            {
                _roles.Remove(role);
            }
            return Task.CompletedTask;
        }
    }
}
