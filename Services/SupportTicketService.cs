using System.Collections.Generic;
using help_center_api.Models;
using help_center_api.Models;
using MongoDB.Driver;

namespace SupportTicketsApi.Services
{
    public class SupportTicketService
    {
        private readonly IMongoCollection<SupportTicket> _supportTickets;

        public SupportTicketService(IHelpCenterDatabaseSettings settings)
        {
            var client = new MongoClient(settings.ConnectionString);
            var database = client.GetDatabase(settings.DatabaseName);

            _supportTickets = database.GetCollection<SupportTicket>(settings.SupportTicketCollectionName);
        }

        public List<SupportTicket> Get() =>
            _supportTickets.Find(supportTicket => true).ToList();

        public SupportTicket Get(string id) =>
            _supportTickets.Find<SupportTicket>(supportTicket => supportTicket.Id == new MongoDB.Bson.ObjectId(id)).FirstOrDefault();

        public SupportTicket Create(SupportTicket SupportTicket)
        {
            _supportTickets.InsertOne(SupportTicket);
            return SupportTicket;
        }

        public void Update(string id, SupportTicket SupportTicketIn) =>
            _supportTickets.ReplaceOne(supportTicket => supportTicket.Id == new MongoDB.Bson.ObjectId(id), SupportTicketIn);

        public void Remove(SupportTicket SupportTicketIn) =>
            _supportTickets.DeleteOne(supportTicket => supportTicket.Id == SupportTicketIn.Id);

        public void Remove(string id) =>
            _supportTickets.DeleteOne(supportTicket => supportTicket.Id == new MongoDB.Bson.ObjectId(id));
    }
}