using System.Collections.Generic;

namespace GameStore.Domain.Models.SqlModels.AccountModels
{
    public class Role : BaseEntity
    {
        public string Name { get; set; }

        public ICollection<UserRole> Roles { get; set; }
    }
}