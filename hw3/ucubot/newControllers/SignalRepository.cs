using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using MySql.Data.MySqlClient;
using ucubot.Model;
using Dapper;
using ucubot.newControllers;

namespace ucubot.newControllers
{
    
    public class SignalRepository : ISignalRepository
    {
        private readonly IConfiguration _configuration;
        private MySqlConnection _connection;
        public SignalRepository(IConfiguration configuration, MySqlConnection connection)
        {
            _configuration = configuration;
            _connection = connection;
        }
        public IEnumerable<StudentSignal> GetALL(MySqlConnection connection)
        {
            connection.Open();
            var value = connection
                .Query<StudentSignal>(
                    "SELECT lesson_signal.Id Id, lesson_signal.Timestamp Timestamp, lesson_signal.SignalType Type, student.user_id UserId FROM lesson_signal LEFT JOIN student ON (lesson_signal.student_id = student.id);")
                .ToList();
            connection.Close();
            return value;
        }
        
    }
}
