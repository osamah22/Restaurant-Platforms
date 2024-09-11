using Microsoft.AspNetCore.Identity;

namespace Restaurants_Platform.Errors;

public static class IdentityResultMessageErros
{
    public static string GetUserFriendlyErrorMessage(this IdentityError error)
    {
        return error.Code switch
        {
            "DuplicateUserName" => "This username is already taken.",
            "DuplicateEmail" => "This email address is already in use.",
            "InvalidUserName" => "The username is invalid.",
            "InvalidEmail" => "The email address is invalid.",
            "PasswordTooShort" => "The password is too short.",
            "PasswordRequiresNonAlphanumeric" => "The password must contain at least one special character.",
            "PasswordRequiresDigit" => "The password must contain at least one digit.",
            "PasswordRequiresLower" => "The password must contain at least one lowercase letter.",
            "PasswordRequiresUpper" => "The password must contain at least one uppercase letter.",
            "PasswordRequiresUniqueChars" => "The password must contain at least one unique character.",
            _ => "An unknown error occurred."
        };
    }
}
