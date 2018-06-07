using System.Collections.Generic;
using System.Linq;
using System.Text;
using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using MySql.Data.MySqlClient;
using ucubot.Model;

namespace ucubot.newControllers
{
    public interface ISignalRepository
    {
        IEnumerable<StudentSignal> GetALL();
        
    }
}