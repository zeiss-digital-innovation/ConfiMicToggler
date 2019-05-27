using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace SkypeMicToggler.Component
{
    public class UserListViewComponent : ViewComponent
    {
        private readonly UserStore _userStore;

        public UserListViewComponent(UserStore userStore)
        {
            _userStore = userStore;
        }
        public async Task<IViewComponentResult> InvokeAsync()
        {
            return View(_userStore.User);
        }
    }
}
