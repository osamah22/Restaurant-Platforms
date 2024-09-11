using Microsoft.AspNetCore.Identity;
using Restaurants_Platform.Shared;

namespace Restaurants_Platform.Errors;


public class ProcessErrors
{
    public class LoginErrors
    {
        public static readonly Error UserNotFound = new Error(
            "Login.UserNotFound",
            "username or password is not valid.");

        public static readonly Error CannotSignIn = new Error(
            "Login.AccountIsLockedOut",
            "Account is locked out.");

        public static readonly Error AccountIsLockedOut = new Error(
            "Login.AccountIsLockedOut",
            "Account is locked out, try later.");

        public static readonly Error SignInNotAllowed = new Error(
            "Login.SignInNotAllowed",
            "Sign in is not allowed.");
    }
}