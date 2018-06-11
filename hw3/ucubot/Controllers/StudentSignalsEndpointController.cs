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
        //private readonly IConfiguration _configuration;
        private readonly SignalRepository _signalRepository;
        public StudentSignalsEndpointController(IConfiguration configuration)
        {
            //_configuration = configuration;
            _signalRepository = new SignalRepository(configuration);
        }
        
        [HttpGet]
        public IEnumerable<StudentSignal> ShowSignals()
        {
            //var conn = new MySqlConnection(_configuration.GetConnectionString("BotDatabase"));
            return _signalRepository.GetALL();
        }
    }
}
