using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using MySql.Data.MySqlClient;
using ucubot.Model;
using ucubot.newControllers;

namespace ucubot.Controllers
{
    [Route("api/[controller]")]
    public class StudentSignalsEndpointController : Controller
    {
        private readonly IConfiguration _configuration;
        private readonly ISignalRepository _signalRepository;
        public StudentSignalsEndpointController(IConfiguration configuration, ISignalRepository repository)
        {
            _configuration = configuration;
            _signalRepository = repository;
        }
        
        [HttpGet]
        public IEnumerable<StudentSignal> ShowSignals()
        {
            var conn = new MySqlConnection(_configuration.GetConnectionString("BotDatabase"));
            return _signalRepository.GetAll(conn);
        }
    }
}
