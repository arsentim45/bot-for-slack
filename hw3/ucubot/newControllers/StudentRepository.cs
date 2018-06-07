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
                "select id as Id, first_name as FirstName, last_name as LastName, user_id as UserId from student where id=@myid",
                new {myid = id}).AsList();
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
                connection.Execute("INSERT INTO student (first_name, last_name, user_id) VALUES (@myfirstname, @mylastname, @myuser)",
                    new {myfirstname = student.FirstName, mylastname = student.LastName, myuser = student.UserId});
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
                    "update student SET first_name = @myfirstname, last_name = @mylastname, user_id = @myuser where id = @myid;",
                    new {myfirstname = student.FirstName, mylastname = student.LastName, myuser = student.UserId, myid = student.Id});
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
                connection.Execute("DELETE FROM student WHERE id = @myid;", new {myid = id});
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
