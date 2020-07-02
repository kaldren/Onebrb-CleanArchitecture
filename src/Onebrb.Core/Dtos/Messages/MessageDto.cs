using System;
using System.Collections.Generic;
using System.Text;

namespace Onebrb.Core.Dtos.Messages
{
    public class MessageDto
    {
        public int Id { get; set; }
        //[Required]
        //[StringLength(100, ErrorMessage = "The {0} must be at least {2} and at max {1} characters long.", MinimumLength = 2)]

        //[Required]
        //[StringLength(100, ErrorMessage = "The {0} must be at least {2} and at max {1} characters long.", MinimumLength = 2)]
        public string Body { get; set; }
        public DateTime DateSent { get; set; }
        public int AuthorId { get; set; }
        public string AuthorUserName { get; set; }
        public int RecipientId { get; set; }
        public string RecipientUserName { get; set; }
        public bool IsDeletedForAuthor { get; set; }
        public bool IsArchivedForAuthor { get; set; }
        public bool IsDeletedForRecipient { get; set; }
        public bool IsArchivedForRecipient { get; set; }
    }
}
