using IDO_API.Entities;
using IDO_API.Request_Params;

namespace IDO_API
{
    public class BLC
    {
        private IDALC _dalc;

        public BLC(IDALC dalc)
        {
            this._dalc = dalc;
        }

        public TokenData Login(LoginParams loginParams)
        {
            Login loginResult = this._dalc.getLogin(loginParams.Email, loginParams.Password);
            if (loginResult != null)
            {
                User user = this._dalc.getUser(loginResult.Id);
                if (user != null)
                {
                    TokenData tokenData = new TokenData {
                        UserId = user.Id,
                        IsAdmin = loginResult.IsAdmin,
                    };
                    return tokenData;
                }
                return null;
            }
            return null;
        }

        public oTask createTask(CreateTaskParams taskParams , int userId)
        {
            oTask task = new oTask();
            task.UserId = userId;
            task.ImportanceId = taskParams.ImportanceId;
            task.StatusId = taskParams.StatusId;
            task.Estimate = taskParams.Estimate;
            task.Date = taskParams.Date;
            task.Category = taskParams.Category;
            task.Title = taskParams.Title;
            task.Position = taskParams.Position;

            return this._dalc.createTask(task);
        }

        public bool deleteTask(int taskId , int userId)
        {
            return this._dalc.deleteTask(taskId,  userId);
        }

        public List<oTask> getUserTasks(int userId)
        {
            return this._dalc.getTasks(userId);
        }

        public oTask editTask(EditTaskParams taskParams , int userId)
        {
            oTask task = new oTask();
            task.Id = taskParams.Id;
            task.UserId = userId;
            task.ImportanceId = taskParams.ImportanceId;
            task.StatusId = taskParams.StatusId;
            task.Estimate = taskParams.Estimate;
            task.Date = taskParams.Date;
            task.Category = taskParams.Category;
            task.Title = taskParams.Title;
            task.Position = taskParams.Position;

            return this._dalc.editTask(task);
        }
    }
}
