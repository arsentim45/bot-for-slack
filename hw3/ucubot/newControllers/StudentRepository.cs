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

namespace ucubot.newControllers
{
    
    public class StudentRepository : IStudentRepository
    {
        private readonly IConfiguration _configuration;

        public StudentRepository(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        
        public IEnumerable<Student> GetAll()
        {
            var connectionString = _configuration.GetConnectionString("BotDatabase");
            var connection = new MySqlConnection(connectionString);

            connection.Open();
            var res = connection.Query<Student>(
                    "select id as Id, first_name as FirstName, last_name as LastName, user_id as UserId from student")
                .AsList();

            connection.Close();

            return res;
        }

        
        public Student GetbyId(long id)
        {
            var connectionString = _configuration.GetConnectionString("BotDatabase");
            var connection = new MySqlConnection(connectionString);

            connection.Open();
            var value = connection.Query<Student>(
                "select id as Id, first_name as FirstName, last_name as LastName, user_id as UserId from student where id=@Id",
                new {Id = id}).AsList();
            connection.Close();
            if (value.Count == 0)
            {
                return null;
            }
            else
            {
                return value[0];
            }
        }

        
        public bool Create(Student student)
        {
            var connectionString = _configuration.GetConnectionString("BotDatabase");
            var connection = new MySqlConnection(connectionString);

            connection.Open();
            try
            {
                connection.Execute("INSERT INTO student (first_name, last_name, user_id) VALUES (@fn, @ln, @uid)",
                    new {fn = student.FirstName, ln = student.LastName, uid = student.UserId});
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

       
        public bool Update(Student student)
        {
            var connectionString = _configuration.GetConnectionString("BotDatabase");
            var connection = new MySqlConnection(connectionString);

            connection.Open();
            try
            {
                connection.Execute(
                    "update student SET first_name = @fn, last_name = @ln, user_id = @uid where id = @id;",
                    new {fn = student.FirstName, ln = student.LastName, uid = student.UserId, id = student.Id});
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                return false;
            }

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
                connection.Execute("DELETE FROM student WHERE id = @id;", new {id = id});
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
