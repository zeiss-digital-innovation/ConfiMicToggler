using System.Threading.Tasks;
using ConfiMicToggler.Data;
using Microsoft.AspNetCore.Mvc;

namespace ConfiMicToggler.Component
{
    /// <summary>
    /// View component which contains the user list with users who used the conference micro toggler
    /// </summary>
    /// <seealso cref="Microsoft.AspNetCore.Mvc.ViewComponent" />
    public class UserListViewComponent : ViewComponent
    {
        private readonly StatisticData _statisticData;

        /// <summary>
        /// Initializes a new instance of the <see cref="UserListViewComponent"/> class.
        /// </summary>
        /// <param name="statisticData">The user store.</param>
        public UserListViewComponent(StatisticData statisticData)
        {
            _statisticData = statisticData;
        }
        
        /// <summary>
        /// Returns the user list view components
        /// </summary>
        /// <returns>The user list view components</returns>
        public IViewComponentResult Invoke()
        {
            return View(_statisticData.GetAllLazyUsers());
        }
    }
}
