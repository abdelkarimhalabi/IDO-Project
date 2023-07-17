using IDO_API.Entities;
using System.Data;
using System.Data.SqlClient;

namespace IDO_API
{
    public class MSSQL : IDALC
    {
        private string _connectionString;
        private IConfiguration _config;

        public MSSQL(IConfiguration config)
        {
            this._config = config;
            this._connectionString = _config["ConnectionString"];
        }
        public oTask createTask(oTask task)
        {
            try
            {
                using (SqlConnection _con = new SqlConnection(_connectionString))
                {
                    _con.Open();
                    string SqlOperation = "CREATE_TASK";
                    using (SqlCommand _cmd = new SqlCommand(SqlOperation, _con))
                    {
                        _cmd.CommandType = CommandType.StoredProcedure;
                        _cmd.Parameters.AddWithValue("@USER_ID", task.userId);
                        _cmd.Parameters.AddWithValue("@STATUS_ID", task.statusId);
                        _cmd.Parameters.AddWithValue("@IMPORTANCE_ID", task.importanceId);
                        _cmd.Parameters.AddWithValue("@ESTIMATE", task.estimate);
                        _cmd.Parameters.AddWithValue("@DUE_DATE", task.date);
                        _cmd.Parameters.AddWithValue("@TITLE", task.title);
                        _cmd.Parameters.AddWithValue("@CATEGORY", task.Category);
                        _cmd.Parameters.AddWithValue("@POSITION", task.position);
                        SqlDataReader reader = _cmd.ExecuteReader();
                        if (reader.HasRows)
                        {
                            reader.Read();
                            oTask newTask = new oTask();
                            newTask.id = reader.GetInt32("TASK_ID");
                            newTask.userId = reader.GetInt32("USER_ID");
                            newTask.statusId = reader.GetInt32("STATUS_ID");
                            newTask.importanceId = reader.GetInt32("IMPORTANCE_ID");
                            newTask.estimate = reader.GetInt32("ESTIMATE");
                            newTask.date = DateOnly.FromDateTime(reader.GetDateTime("DUE_DATE")).ToString();
                            newTask.title = reader.GetString("TITLE");
                            newTask.Category = reader.GetString("CATEGORY");
                            newTask.position = reader.GetInt32("POSITION");

                            return newTask;
                        }
                        else
                        {
                            return null;
                        }
                    }
                }
            }catch (Exception ex)
            {
                Console.WriteLine(ex.Message);  
                return null;
            }
        }

        public bool deleteTask(int taskId , int userId)
        {
            try
            {
                using (SqlConnection _con = new SqlConnection(_connectionString))
                {
                    _con.Open();
                    string SqlOperation = "DELETE_TASK";
                    using (SqlCommand _cmd = new SqlCommand(SqlOperation, _con))
                    {
                        _cmd.CommandType = CommandType.StoredProcedure;
                        _cmd.Parameters.AddWithValue("@USER_ID", userId);
                        _cmd.Parameters.AddWithValue("@TASK_ID", taskId);
                        _cmd.ExecuteNonQuery();
                    }
                }
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }

        public List<oTask> getTasks(int userId)
        {
            try
            {
                using (SqlConnection _con = new SqlConnection(_connectionString))
                {
                    _con.Open();
                    string SqlOperation = "GET_USER_TASKS";
                    using (SqlCommand _cmd = new SqlCommand(SqlOperation, _con))
                    {
                        _cmd.CommandType = CommandType.StoredProcedure;
                        _cmd.Parameters.AddWithValue("@USER_ID", userId);
                        SqlDataReader reader = _cmd.ExecuteReader();
                        if (reader.HasRows)
                        {
                            List<oTask> tasks = new List<oTask>();
                            while (reader.Read())
                            {
                                oTask task = new oTask();
                                task.id = reader.GetInt32("ID");
                                task.userId = reader.GetInt32("USER_ID");
                                task.importanceId = reader.GetInt32("IMPORTANCE_ID");
                                task.estimate = reader.GetInt32("ESTIMATE");
                                task.date = DateOnly.FromDateTime(reader.GetDateTime("DUE_DATE")).ToString();
                                task.title = reader.GetString("TITLE");
                                task.Category = reader.GetString("CATEGORY");
                                task.position = reader.GetInt32("POSITION");
                                task.statusId = reader.GetInt32("STATUS_ID");

                                tasks.Add(task);
                            }

                            return tasks;
                        }
                        else
                        {
                            return null;
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        public oTask editTask(oTask task)
        {
            try
            {
                using (SqlConnection _con = new SqlConnection(_connectionString))
                {
                    _con.Open();
                    string SqlOperation = "EDIT_TASK";
                    using (SqlCommand _cmd = new SqlCommand(SqlOperation, _con))
                    {
                        _cmd.CommandType = CommandType.StoredProcedure;
                        _cmd.Parameters.AddWithValue("@USER_ID", task.userId);
                        _cmd.Parameters.AddWithValue("@TASK_ID", task.id);
                        _cmd.Parameters.AddWithValue("@STATUS_ID", task.statusId);
                        _cmd.Parameters.AddWithValue("@IMPORTANCE_ID", task.importanceId);
                        _cmd.Parameters.AddWithValue("@ESTIMATE", task.estimate);
                        _cmd.Parameters.AddWithValue("@DUE_DATE", task.date);
                        _cmd.Parameters.AddWithValue("@TITLE", task.title);
                        _cmd.Parameters.AddWithValue("@CATEGORY", task.Category);
                        _cmd.Parameters.AddWithValue("@POSITION", task.position);
                        SqlDataReader reader = _cmd.ExecuteReader();
                        if (reader.HasRows)
                        {
                            reader.Read();
                            oTask editedTask = new oTask();
                            editedTask.id = reader.GetInt32("ID");
                            editedTask.userId = task.userId;
                            editedTask.statusId = reader.GetInt32("STATUS_ID");
                            editedTask.importanceId = reader.GetInt32("IMPORTANCE_ID");
                            editedTask.estimate = reader.GetInt32("ESTIMATE");
                            editedTask.date = DateOnly.FromDateTime(reader.GetDateTime("DUE_DATE")).ToString();
                            editedTask.title = reader.GetString("TITLE");
                            editedTask.Category = reader.GetString("CATEGORY");
                            editedTask.position = reader.GetInt32("POSITION");

                            return editedTask;
                        }
                        else
                        {
                            return null;
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                return null ;
            }
        }

        public Login getLogin(string email, string password)
        {
            try
            {
                using (SqlConnection _con = new SqlConnection(_connectionString))
                {
                    _con.Open();
                    string SqlOperation = "GET_LOGIN";
                    using (SqlCommand _cmd = new SqlCommand(SqlOperation, _con))
                    {
                        _cmd.CommandType = CommandType.StoredProcedure;
                        _cmd.Parameters.AddWithValue("@EMAIL", email);
                        _cmd.Parameters.AddWithValue("@PASSWORD", password);
                        SqlDataReader reader = _cmd.ExecuteReader();
                        if (reader.HasRows)
                        {
                            Login login = new Login();
                            reader.Read();
                            login.id = reader.GetInt32("ID");
                            login.email = reader.GetString("EMAIL");
                            login.password = reader.GetString("PASSWORD");
                            login.isAdmin = reader.GetBoolean("IS_ADMIN");

                            return login;
                        }
                        else
                        {
                            return null;
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        public User getUser(int loginId)
        {
            try
            {
                using (SqlConnection _con = new SqlConnection(_connectionString))
                {
                    _con.Open();
                    string SqlOperation = "GET_USER_BY_LOGIN_ID";
                    using (SqlCommand _cmd = new SqlCommand(SqlOperation, _con))
                    {
                        _cmd.CommandType = CommandType.StoredProcedure;
                        _cmd.Parameters.AddWithValue("@LOGIN_ID", loginId);
                        SqlDataReader reader = _cmd.ExecuteReader();
                        if (reader.HasRows)
                        {
                            User user = new User();

                            reader.Read();
                            user.id = reader.GetInt32("ID");
                            user.name = reader.GetString("NAME");
                            user.loginId = reader.GetInt32("LOGIN_ID");
                            if (!reader.IsDBNull(reader.GetOrdinal("PROFILE_URL")))
                            {
                                user.profileUrl = reader.GetString(reader.GetOrdinal("PROFILE_URL"));
                            }

                            return user;
                        }
                        else
                        {
                            return null;
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return null;
            }
        }
    }
}
