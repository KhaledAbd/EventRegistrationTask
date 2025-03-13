using RAG.EventRegistrationTask.Base;
using RAG.EventRegistrationTask.Events;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Volo.Abp;
using Volo.Abp.Identity;

namespace RAG.EventRegistrationTask.Organizer
{
    public class OrganizerAppService : BaseApplicationService, IOrganizerAppService
    {
        private readonly IIdentityUserRepository _userRepository;
        private readonly IIdentityRoleRepository _identityRoleRepository;

        public OrganizerAppService(IIdentityUserRepository userRepository, IIdentityRoleRepository identityRoleRepository)
        {
            _userRepository = userRepository;
            _identityRoleRepository = identityRoleRepository;
        }
        public async Task<List<OrganizerDto>> GetListAsync()
        {
            // Get the role by name
            var role = await _identityRoleRepository.FindByNormalizedNameAsync(EventRegistrationTaskConsts.OrganizerRoleName);
          
            if (role == null)
            {
                throw new BusinessException("","Please Add Organizer Users");
            }

            // Get the users assigned to the specified role
            var users = await _userRepository.GetListAsync(includeDetails:true);

            var usersInRole = users
                 .Where(user => user.Roles != null && user.IsInRole(role.Id))
                 .Select(u => new OrganizerDto
                 {
                     Id = u.Id,
                     Name = u.Name,
                 })
                 .ToList();


            return usersInRole;
        }
    }
}
