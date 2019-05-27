using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Security.Principal;
using System.Threading.Tasks;

namespace SkypeMicToggler
{
    public class UserStore
    {
        public UserStore()
        {
            this.User = new Dictionary<string, int>();
        }


        public Dictionary<string, int> User { get; set; }

        public void Add(ClaimsPrincipal user)
        {
            WindowsPrincipal winUser = user as WindowsPrincipal;
            if (winUser != null)
            {
                this.User[winUser.Identity.Name] = this.User.ContainsKey(winUser.Identity.Name) ? ++this.User[winUser.Identity.Name] : 1;
            }

        }
    }
}
