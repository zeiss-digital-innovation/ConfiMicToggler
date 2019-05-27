using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Security.Principal;
using ConfiMicToggler.Model;

namespace ConfiMicToggler.Data
{
    /// <summary>
    /// Contains all users which used the mic toggler
    /// </summary>
    public class StatisticData
    {
        private Dictionary<string, int> _users;

        /// <summary>
        /// Initializes a new instance of the <see cref="StatisticData"/> class.
        /// </summary>
        public StatisticData()
        {
            this._users = new Dictionary<string, int>();
        }

        /// <summary>
        /// Adds the given <see cref="user"/> to the statistics as a lazy person which used this tool, instead of going to the microfon.
        /// If the user already exists in the statistics, the usage will incremented by +1
        /// </summary>
        /// <param name="user">The user.</param>
        public void AddLazyUserCounter(ClaimsPrincipal user)
        {
            WindowsPrincipal winUser = user as WindowsPrincipal;
            if (winUser != null)
            {
                this._users[winUser.Identity.Name] = this._users.ContainsKey(winUser.Identity.Name) ? ++this._users[winUser.Identity.Name] : 1;
            }
        }

        /// <summary>
        /// Gets all lazy users which used this tool and was too lazy to stand up and toggle the microfon by hand.
        /// </summary>
        /// <returns>All lazy users which used this tool</returns>
        public IEnumerable<UserViewModel> GetAllLazyUsers()
        {
            return this._users.Select(u => new UserViewModel() { Name = u.Key, UsageCount = u.Value });
        }
    }
}
