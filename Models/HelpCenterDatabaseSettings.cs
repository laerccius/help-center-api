

namespace help_center_api.Models
{
    public class HelpCenterDatabaseSettings : IHelpCenterDatabaseSettings
    {
        public string SupportTicketCollectionName { get; set; }
        public string ConnectionString { get; set; }
        public string DatabaseName { get; set; }
    }

    public interface IHelpCenterDatabaseSettings
    {
        string SupportTicketCollectionName { get; set; }
        string ConnectionString { get; set; }
        string DatabaseName { get; set; }
    }
}