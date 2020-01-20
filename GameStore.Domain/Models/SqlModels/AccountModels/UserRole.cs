using System;

namespace GameStore.Domain.Models.SqlModels.AccountModels
{
    public class UserRole
    {
        public Role Role { get; set; }

        public Guid UserId { get; set; }

        public User User { get; set; }

        public Guid RoleId { get; set; }
    }
}