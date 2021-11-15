using System;
using System.Collections.Generic;
using System.Text;

namespace HttpHelper
{
    public class TokenRequest
    {
        public TokenRequest(string username, string password)
        {
            this.Username = username;
            this.Password = password;
        }
        public string Username { get; set; }
        public string Password { get; set; }
    }
}
