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
            Login loginResult = this._dalc.getLogin(loginParams.email, loginParams.password);
            if (loginResult != null)
            {
                User user = this._dalc.getUser(loginResult.id);
                if (user != null)
                {
                    TokenData tokenData = new TokenData {
                        UserId = user.id,
                        IsAdmin = loginResult.isAdmin,
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
            task.userId = userId;
            task.importanceId = taskParams.importanceId;
            task.statusId = taskParams.statusId;
            task.estimate = taskParams.estimate;
            task.date = taskParams.date;
            task.Category = taskParams.Category;
            task.title = taskParams.title;
            task.position = taskParams.position;

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
            task.id = taskParams.id;
            task.userId = userId;
            task.importanceId = taskParams.importanceId;
            task.statusId = taskParams.statusId;
            task.estimate = taskParams.estimate;
            task.date = taskParams.date;
            task.Category = taskParams.Category;
            task.title = taskParams.title;
            task.position = taskParams.position;

            return this._dalc.editTask(task);
        }
    }
}
