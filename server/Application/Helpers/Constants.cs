namespace Application.Helpers
{
    public static class Constants
    {
        public enum AnswerCodes : int
        {
            NoCommand = 0,
            SignedIn = 3,
            GoToGoogleRegistrationPage = 10,
        }

        public enum GoogleAuthResultCodes : int
        {
            UserFound = 1,
            NoUserInDB = 2,
            EmailNotConnectedWithAccount = 3,
        }

        public static class RoleManager
        {
            public const string Guest = "Guest";

            public const string Admin = "Admin";

            public const string Customer = "Customer";
        }

        public static class AnswerMessage
        {
            public const string LoggedAs = "Logged as: ";

            public const string WrongCreds = "Wrong login or password";

            public const string LoginError = "Sorry, can't log in.";

            public const string NotConnectedGoogle = "Account with this e-mail is not connected to the Google account.";

            public const string LogOutSucceed = "Log out completed";

            public const string RegisteredSuccessfully = "New customer was registered: ";

            public const string RegisteredUnsuccessfully = "Error. Customer wasn't registered.";

            public const string Redirection = "Redirecting...";
        }
    }
}
