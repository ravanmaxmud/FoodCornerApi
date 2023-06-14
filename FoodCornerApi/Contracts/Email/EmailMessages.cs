namespace FoodCornerApi.Contracts.Email
{
    public class EmailMessages
    {
        public static class Subject
        {
            public const string ACTIVATION_MESSAGE = $"Hesabin aktivlesdirilmesi";
            public const string NOTIFICATION_MESSAGE = $"Orderinizin Statusu Yenilendi Xais Edirik Yoxlayin";
            public const string CHANGEPASSWORD_MESSAGE = $"CHANGE PASSWORD";
        }

        public static class Body
        {
            public const string ACTIVATION_MESSAGE = $"Sizin activation urliniz : {EmailMessageKeyword.ACTIVATION_URL}";
            public const string CHANGEPASSWORD_MESSAGE = $"Sizin change urlniz : {EmailMessageKeyword.CHANGEPASSWORD_URL}";
        }
    }
}
