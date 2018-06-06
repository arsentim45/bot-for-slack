using System.Collections.Generic;
using System.Linq;
using System.Text;
using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using ucubot.Model;

namespace ucubot.newControllers
{
    public interface IStudentRepository
    {
        IEnumerable<Student> GetAll();
        Student GetbyId(long id);
        bool Create(Student student);
        bool Update(Student student);
        bool Remove(long id);
        
    }
}