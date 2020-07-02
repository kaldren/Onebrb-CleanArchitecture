using Onebrb.Core.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace Onebrb.Core.Entities
{
    public class Message : BaseEntity<int>
    {
        public string Body { get; set; }
        public DateTime DateSent { get; set; } = DateTime.UtcNow;
        public int AuthorId { get; set; }
        public string AuthorUserName { get; set; }
        public int RecipientId { get; set; }
        public string RecipientUserName { get; set; }
        public bool IsDeletedForAuthor { get; set; }
        public bool IsArchivedForAuthor { get; set; }
        public bool IsDeletedForRecipient { get; set; }
        public bool IsArchivedForRecipient { get; set; }
        [JsonIgnore]
        public List<ApplicationUserMessage> ApplicationUserMessage { get; set; }
    }
}
