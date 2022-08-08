using Section_11___Notes_App.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Section_11___Notes_App.ViewModel.Helpers.Database
{
    public interface IAuthHelper
    {
        public Task<bool> Register(IUser user);

        public Task<bool> Login(IUser user);
    }
}
