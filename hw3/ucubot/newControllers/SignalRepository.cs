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
        private readonly MySqlConnection connection;
        public SignalRepository(IConfiguration configuration)
        {
            connection = new MySqlConnection(configuration.GetConnectionString("BotDatabase"));
        }
        
        public IEnumerable<StudentSignal> GetALL()
        {    
            connection.Open();
            var value = connection
                .Query<StudentSignal>(
                        "SELECT first_name as FirstName, last_name LastName, signal_type SignalType, count as Count FROM student_signals;")
                .AsList();
            connection.Close();
            return value;
        }
        
    }
    
}
