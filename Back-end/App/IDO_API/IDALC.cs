using IDO_API.Entities;

namespace IDO_API
{
    public interface IDALC
    {
        Login getLogin(string email, string password);
        User getUser(int  loginId);
        List<oTask> getTasks(int userId);
        oTask editTask(oTask task);
        oTask createTask(oTask task);
        bool deleteTask(int taskId , int userId);
    }
}
