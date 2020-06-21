namespace Onebrb.Client.Settings
{
    public class ApiSettings
    {
        public MessagesSettings MessagesSettings { get; set; }
        public UsersSettings UsersSettings { get; set; }
    }

    public class MessagesSettings
    {
        public string CreateApiEndpoint { get; set; }
        public string DeleteApiEndpoint { get; set; }
        public string GetAllApiEndpoint { get; set; }
        public string GetAllReceivedApiEndpoint { get; set; }
        public string GetAllSentApiEndpoint { get; set; }
        public string GetAllArchivedApiEndpoint { get; set; }
    }

    public class UsersSettings
    {
        public string GetUserApiEndpoint { get; set; }
    }
}
