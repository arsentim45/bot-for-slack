using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using MySql.Data.MySqlClient;
using ucubot.Model;
using Dapper;
using ucubot.newControllers;


namespace ucubot.Controllers
{
    [Route("api/[controller]")]
    public class LessonSignalEndpointController : Controller
    {
        private readonly ILessonSignalRepository _lessonSignalRepository;

        public LessonSignalEndpointController(ILessonSignalRepository lessonSignalRepository)
        {
            _lessonSignalRepository = lessonSignalRepository;
        }

        [HttpGet]
        public IEnumerable<LessonSignalDto> ShowSignals()
        {
            return _lessonSignalRepository.GetALL();
        }

        [HttpGet("{id}")]
        public LessonSignalDto ShowSignal(long id)
        {
            return _lessonSignalRepository.GetbyId(id);
        }

        [HttpPost]
        public async Task<IActionResult> CreateSignal(SlackMessage message)
        {
            var value = _lessonSignalRepository.Create(message);
            if (value == false)
            {
                return BadRequest();   
            }
            else
            {
                return Accepted();
            }
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> RemoveSignal(long id)
        {
            var value = _lessonSignalRepository.Remove(id);
            if (value == false)
            {
                return BadRequest();
            }
            else
            {
                return Accepted();
            }

            
        }
    }
}