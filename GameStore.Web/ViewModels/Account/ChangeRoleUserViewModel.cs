using System.Collections.Generic;

namespace GameStore.Web.ViewModels.Account
{
    public class ChangeRoleUserViewModel
    {
        public List<string> Roles { get; set; }

        public string UserEmail { get; set; }
    }
}