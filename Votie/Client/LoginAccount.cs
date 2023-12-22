using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Votie.Shared
{
    public class LoginAccount
    {

        public string UserName { get; set; }
        public string Password { get; set; }
        public void Clear() => UserName = Password = null;
    }
    public class LoginResult
    {
        public string accessToken { get; set; }
        public string refreshToken { get; set; }
    }
}
