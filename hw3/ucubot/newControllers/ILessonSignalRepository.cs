using System.Collections.Generic;
using MySql.Data.MySqlClient;
using ucubot.Model;
namespace ucubot.newControllers
{
    public interface ILessonSignalRepository
    {
        IEnumerable<LessonSignalDto> GetAll();
        LessonSignalDto GetbyId(long id);
        bool Create(SlackMessage message);
        bool Remove(long id);
    }
}