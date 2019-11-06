using System;
using MongoDB.Bson;

namespace help_center_api.Models
{
    public class SupportTicket
    {
        public ObjectId Id { get; set; }

        public string UserName { get; set; }

        public string Description { get; set; }
    }
}
