using IDO_API.Entities;
using System.Data;
using System.Data.SqlClient;
using System.Globalization;

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
                        _cmd.Parameters.AddWithValue("@USER_ID", task.UserId);
                        _cmd.Parameters.AddWithValue("@STATUS_ID", task.StatusId);
                        _cmd.Parameters.AddWithValue("@IMPORTANCE_ID", task.ImportanceId);
                        _cmd.Parameters.AddWithValue("@ESTIMATE", task.Estimate);
                        _cmd.Parameters.AddWithValue("@DUE_DATE", task.Date);
                        _cmd.Parameters.AddWithValue("@TITLE", task.Title);
                        _cmd.Parameters.AddWithValue("@CATEGORY", task.Category);
                        _cmd.Parameters.AddWithValue("@POSITION", task.Position);
                        SqlDataReader reader = _cmd.ExecuteReader();
                        if (reader.HasRows)
                        {
                            reader.Read();
                            oTask newTask = new oTask();
                            newTask.Id = reader.GetInt32("TASK_ID");
                            newTask.UserId = reader.GetInt32("USER_ID");
                            newTask.StatusId = reader.GetInt32("STATUS_ID");
                            newTask.ImportanceId = reader.GetInt32("IMPORTANCE_ID");
                            newTask.Estimate = reader.GetInt32("ESTIMATE");
                            newTask.Date = DateOnly.FromDateTime(reader.GetDateTime("DUE_DATE")).ToString();
                            newTask.Title = reader.GetString("TITLE");
                            newTask.Category = reader.GetString("CATEGORY");
                            newTask.Position = reader.GetInt32("POSITION");

                            return newTask;
                        }
                        return null;
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
                                task.Id = reader.GetInt32("ID");
                                task.UserId = reader.GetInt32("USER_ID");
                                task.ImportanceId = reader.GetInt32("IMPORTANCE_ID");
                                task.Estimate = reader.GetInt32("ESTIMATE");
                                task.Date = DateOnly.FromDateTime(reader.GetDateTime("DUE_DATE")).ToString("yy/MM/dd" , CultureInfo.InvariantCulture);
                                task.Title = reader.GetString("TITLE");
                                task.Category = reader.GetString("CATEGORY");
                                task.Position = reader.GetInt32("POSITION");
                                task.StatusId = reader.GetInt32("STATUS_ID");

                                tasks.Add(task);
                            }

                            return tasks;
                        }
                        return null;
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
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
                        _cmd.Parameters.AddWithValue("@USER_ID", task.UserId);
                        _cmd.Parameters.AddWithValue("@TASK_ID", task.Id);
                        _cmd.Parameters.AddWithValue("@STATUS_ID", task.StatusId);
                        _cmd.Parameters.AddWithValue("@IMPORTANCE_ID", task.ImportanceId);
                        _cmd.Parameters.AddWithValue("@ESTIMATE", task.Estimate);
                        _cmd.Parameters.AddWithValue("@DUE_DATE", task.Date);
                        _cmd.Parameters.AddWithValue("@TITLE", task.Title);
                        _cmd.Parameters.AddWithValue("@CATEGORY", task.Category);
                        _cmd.Parameters.AddWithValue("@POSITION", task.Position);
                        SqlDataReader reader = _cmd.ExecuteReader();
                        if (reader.HasRows)
                        {
                            reader.Read();
                            oTask editedTask = new oTask();
                            editedTask.Id = reader.GetInt32("ID");
                            editedTask.UserId = task.UserId;
                            editedTask.StatusId = reader.GetInt32("STATUS_ID");
                            editedTask.ImportanceId = reader.GetInt32("IMPORTANCE_ID");
                            editedTask.Estimate = reader.GetInt32("ESTIMATE");
                            editedTask.Date = DateOnly.FromDateTime(reader.GetDateTime("DUE_DATE")).ToString("yy/MM/dd", CultureInfo.InvariantCulture);
                            editedTask.Title = reader.GetString("TITLE");
                            editedTask.Category = reader.GetString("CATEGORY");
                            editedTask.Position = reader.GetInt32("POSITION");

                            return editedTask;
                        }
                        return null;
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
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
                            login.Id = reader.GetInt32("ID");
                            login.Email = reader.GetString("EMAIL");
                            login.Password = reader.GetString("PASSWORD");
                            login.IsAdmin = reader.GetBoolean("IS_ADMIN");

                            return login;
                        }
                        return null;
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
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
                            user.Id = reader.GetInt32("ID");
                            user.Name = reader.GetString("NAME");
                            user.LoginId = reader.GetInt32("LOGIN_ID");
                            if (!reader.IsDBNull(reader.GetOrdinal("PROFILE_URL")))
                            {
                                user.ProfileUrl = reader.GetString(reader.GetOrdinal("PROFILE_URL"));
                            }

                            return user;
                        }
                        return null;
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
