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
    public class StudentEndpointController : Controller
    {
        private readonly IConfiguration _configuration;
        private readonly IStudentRepository _studentRepository;

        public StudentEndpointController(IConfiguration configuration, IStudentRepository studentRepository)
        {
            _configuration = configuration;
            _studentRepository = studentRepository;
        }

        [HttpGet]
        public IEnumerable<Student> ShowSignals()
        {
            return _studentRepository.GetAll();
        }

        [HttpGet("{id}")]
        public Student ShowStudent(long id)
        {
            return _studentRepository.GetbyId(id);
        }

        [HttpPost]
        public async Task<IActionResult> CreateStudent(Student student)
        {
            var value = _studentRepository.Create(student);
            if (value == false)
            {
                return StatusCode(409);
            }
            else
            {
                return Accepted();
            }
            
            
        }

        [HttpPut]
        public async Task<IActionResult> UpdateStudent(Student student)
        {
            var value = _studentRepository.Update(student);
            if (value == true)
            {
                return Accepted();
            }
            else
            {
                return BadRequest();
            }
            
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> RemoveStudent(long id)
        {
            var value = _studentRepository.Remove(id);
            if (value == true)
            {
                return Accepted();
            }
            else
            {
                return StatusCode(409);
            }
            
        }
    }
}