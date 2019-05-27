using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ConfiMicToggler.Model
{
    public class UserViewModel
    {
        /// <summary>
        /// Gets or sets the name of the user.
        /// </summary>
        /// <value>
        /// The name.
        /// </value>
        public string Name { get; set; }

        /// <summary>
        /// Gets or sets the usage count how often the user used the toggler and was to lazy to go to the microfon ;).
        /// </summary>
        /// <value>
        /// The usage count.
        /// </value>
        public int UsageCount { get; set; }
    }
}
