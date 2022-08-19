using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MainWeb
{
    public  class AuthorizationConstants
    {
        public const string AUTH_KEY = "AuthKeyOfDoomThatMustBeAMinimumNumberOfBytes";

        // TODO: Don't use this in production
        public const string DEFAULT_PASSWORD = "Pass@word1";

        // TODO: Change this to an environment variable

        public const string JWT_ValidAudience= "https://localhost:7086/";
        public const string JWT_ValidIssuer = "https://localhost:7086/";
        public const string JWT_SECRET_KEY = "SecretKeyOfDoomThatMustBeAMinimumNumberOfBytes";

    }
}
