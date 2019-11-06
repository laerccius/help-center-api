using System.Collections.Generic;
using help_center_api.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using SupportTicketsApi.Services;

namespace help_center_api.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class SupportTicketController : ControllerBase
    {

        private readonly ILogger<SupportTicketController> _logger;
        private readonly SupportTicketService _supportTicketService;

        public SupportTicketController(ILogger<SupportTicketController> logger, SupportTicketService SupportTicketService)
        {
            _logger = logger;
            _supportTicketService = SupportTicketService;
        }

        [HttpGet]
        public IEnumerable<SupportTicket> Get()
        {
            return _supportTicketService.Get();
        }

        [HttpPost]
        public SupportTicket Post(SupportTicket supportTicket)
        {
            return _supportTicketService.Create(supportTicket);
        }
    }
}
