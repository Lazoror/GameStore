using System.Collections.Generic;

namespace GameStore.Domain.Models.SqlModels.AccountModels
{
    public class User : BaseEntity
    {
        public string Name { get; set; }

        public string Email { get; set; }

        public string Password { get; set; }

        public bool IsDeleted { get; set; }

        public ICollection<UserRole> Roles { get; set; }
    }
}