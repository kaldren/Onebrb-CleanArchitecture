using System;
using System.Collections.Generic;
using System.Text;

namespace Onebrb.Core.Entities
{
    public class ApplicationUserMessage
    {
        public int MessageId { get; set; }
        public Message Message { get; set; }
        public int ApplicationUserId { get; set; }
        public ApplicationUser ApplicationUser { get; set; }
    }
}
