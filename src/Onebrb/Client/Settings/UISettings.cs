using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Onebrb.Client.Settings
{
    public class UISettings
    {
        public UIMessages UIMessages { get; set; }
        public UIStyles UIStyles { get; set; }
    }

    public class UIMessages
    {
        public string Loading { get; set; }
        public string PleaseWait { get; set; }
        public string SendingMessage { get; set; }
        public string MessageSent { get; set; }
        public string MessageSendingFailed { get; set; }
        public string ReceivedMessages { get; set; }
        public string ReceivedMessagesEmpty { get; set; }
        public string SentMessages { get; set; }
        public string SentMessagesEmpty { get; set; }
        public string ArchivedMessages { get; set; }
        public string ArchivedMessagesEmpty { get; set; }
        public string DeletingMessage { get; set; }
        public string DeletingMessageSuccess { get; set; }
        public string DeletingMessageFailed { get; set; }
        public string UserNotFound { get; set; }
    }

    public class UIStyles
    {
        public string AlertDanger { get; set; }
        public string AlertInfo { get; set; }
        public string AlertSuccess { get; set; }
    }
}
