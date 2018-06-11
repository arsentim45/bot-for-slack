using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using MySql.Data.MySqlClient;
using ucubot.Model;
using Dapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;


namespace ucubot.newControllers
{
    public class LessonSignalRepository : ILessonSignalRepository
    {
        private readonly IConfiguration _configuration;

        public LessonSignalRepository(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public IEnumerable<LessonSignalDto> GetALL()
        {
            var connectionString = _configuration.GetConnectionString("BotDatabase");
            var connection = new MySqlConnection(connectionString);

            connection.Open();
            var value = connection.Query<LessonSignalDto>(
                    "select lesson_signal.id as Id, lesson_signal.time_stamp as Timestamp, lesson_signal.signal_type as Type, student.user_id as UserId from lesson_signal join (student) on (lesson_signal.student_id = student.id);")
                .AsList();
            connection.Close();

            return value;
        }

        
        public LessonSignalDto GetbyId(long id)
        {
            var connectionString = _configuration.GetConnectionString("BotDatabase");
            var connection = new MySqlConnection(connectionString);

            connection.Open();
            var value = connection.Query<LessonSignalDto>(
                "select lesson_signal.id as Id, lesson_signal.time_stamp as Timestamp, lesson_signal.signal_type as Type, student.user_id as UserId from lesson_signal join (student) on (lesson_signal.student_id = student.id) where lesson_signal.id=@myid;",
                new {myid = id}).ToList();
            connection.Close();
            if (value.Count == 0)
            {
                connection.Close();
                return null;
            }
            else
            {
                connection.Close();
                return value[0];
            }
        }

        
        public bool Create(SlackMessage message)
        {
            var userId = message.user_id;
            var signalType = message.text.ConvertSlackMessageToSignalType();

            var connectionString = _configuration.GetConnectionString("BotDatabase");
            var connection = new MySqlConnection(connectionString);

            connection.Open();
            var value = connection.Query<Student>(
                "select id as Id, first_name as FirstName, last_name as LastName, user_id as UserId from student where user_id=@myuser",
                new {myuser = userId}).AsList();
            if (!value.Any())
            {    
                connection.Close();
                return false;
            }
            connection.Execute("INSERT INTO lesson_signal (student_id, signal_type) VALUES (@mystudentid, @mysignaltype)",
                new {mystudentid = value[0].Id, mysignaltype = signalType});
            

            connection.Close();
            return true;
        }

        
        public bool Remove(long id)
        {
            var connectionString = _configuration.GetConnectionString("BotDatabase");
            var connection = new MySqlConnection(connectionString);

            connection.Open();
            try
            {
                connection.Execute("DELETE FROM lesson_signal WHERE id = @myid;", new {myid = id});
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                connection.Close();
                return false;
            }

            connection.Close();
            return true;
        }
    }
}
    

