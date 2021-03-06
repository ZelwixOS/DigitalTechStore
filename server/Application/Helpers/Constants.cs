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

            public const string Courier = "Courier";

            public const string Manager = "Manager";

            public const string ShopAssistant = "ShopAssistant";

            public const string WarehouseWorker = "WarehouseWorker";
        }

        public static class AuthManager
        {
            public const string AdminManager = "Admin,Manager";

            public const string AdminOutlet = "Admin,Manager,ShopAssistant";

            public const string Customer = "Customer";

            public const string Courier = "Courier";

            public const string Manager = "Manager";

            public const string ShopAssistant = "ShopAssistant";

            public const string WarehouseWorker = "WarehouseWorker";

            public const string Worker = "Admin,Manager,ShopAssistant,Courier,WarehouseWorker";

            public const string WorkerNotCourier = "Admin,Manager,ShopAssistant,WarehouseWorker";
        }

        public static class AnswerMessage
        {
            public const string LoggedAs = "Вы вошли как: ";

            public const string WrongCreds = "Неверный логин или пароль";

            public const string LoginError = "Не удалось войти в систему";

            public const string NotConnectedGoogle = "Аккаунт с указаным адрессом не привязан к аккаунту Google";

            public const string LogOutSucceed = "Выполнен выход";

            public const string RegisteredSuccessfully = "Зарегистрирован новый покупатель: ";

            public const string RegisteredWorkerSuccessfully = "Зарегистрирован новый работник: ";

            public const string RegisteredWorkerUnSuccessfully = "Ошибка. Не удалось зарегистрировать работника";

            public const string RegisteredUnsuccessfully = "Ошибка. Не удалось зарегистрировать покупателя";

            public const string Redirection = "Перенаправление...";
        }
    }
}
