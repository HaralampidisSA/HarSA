namespace HarSA.Net.Mail
{
    public static class EmailSettingNames
    {
        public const string DefaultFromAddress = "Har.Net.Email.DefaultFromAddress";
        public const string DefaultFromDisplayName = "Har.Net.Email.DefaultFromDisplayName";

        public static class Smtp
        {
            public const string Host = "Har.Net.Mail.Smtp.Host";

            public const string Port = "Har.Net.Mail.Smtp.Port";

            public const string UserName = "Har.Net.Mail.Smtp.UserName";

            public const string Password = "Har.Net.Mail.Smtp.Password";

            public const string EnableSsl = "Har.Net.Mail.Smtp.EnableSsl";

            public const string UseDefaultCredentials = "Har.Net.Mail.Smtp.UseDefaultCredentials";
        }
    }
}
