using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using IDO_API.Entities;
using IDO_API.Request_Params;
using IDO_API.Tools;

namespace IDO_API.Controllers
{
    [Route("ido")]
    [ApiController]
    public class IDO : ControllerBase
    {
        private readonly BLC _blc;
        private JWT_AUTH jwtAuth;
        private IConfiguration _conf;

        public IDO(BLC blc , IConfiguration conf)
        {
            this._blc = blc;
            this._conf = conf;
            this.jwtAuth = new JWT_AUTH(_conf["SecretKey"]);
        }

        [Route("createTask")]
        [HttpPost]
        public ActionResult<oTask> CreateTask([FromBody] CreateTaskParams task , [FromHeader(Name = "Token")] string token)
        {
            try
            {
                Console.WriteLine(_conf["SecretKey"]);
                var tokenData = jwtAuth.DecodeToken(token);
                if (tokenData != null)
                {
                    if (ModelState.IsValid)
                    {
                        oTask createdTask = _blc.createTask(task, tokenData.UserId);
                        if (createdTask != null)
                        {
                            return Ok(createdTask);
                        }
                        return StatusCode(500, "Failed to create the task.");
                    }
                    return BadRequest(ModelState);
                }
                return Unauthorized("Invalid credentials");
            }
            catch (Exception ex)
            {
                return StatusCode(500, "Failed to create the task.");
            }

        }

        [Route("delete/{taskId}")]
        [HttpGet]
        public IActionResult deleteTask(int taskId , [FromHeader(Name = "Token")] string token)
        {
            try
            {
                var tokenData = this.jwtAuth.DecodeToken(token);
                if (tokenData != null)
                {
                    var result = _blc.deleteTask(taskId, tokenData.UserId);
                    if (result)
                    {
                        return Ok($"Deleted Task with id: {taskId}");
                    }
                    return StatusCode(500, "Failed to delete the task.");
                }
                return Unauthorized("Invalid credentials");
            }
            catch (Exception ex)
            {
                return StatusCode(500, "Failed to delete the task.");
            }
        }

        [Route("getTasks")]
        [HttpGet]
        public ActionResult<List<oTask>> getUserTasks([FromHeader(Name = "Token")] string token)
        {
            try
            {
                var tokenData = this.jwtAuth.DecodeToken(token);
                if (tokenData != null)
                { 
                    var result = _blc.getUserTasks(tokenData.UserId);
                    if (result != null)
                    {
                        return Ok(result);
                    }
                    return StatusCode(500, "Failed to get tasks.");
                }
                return Unauthorized("Invalid credentials");
            }
            catch(Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }

        [Route("EditTask")]
        [HttpPost]
        public ActionResult<oTask> editTask([FromBody] EditTaskParams taskParams , [FromHeader(Name = "Token")] string token)
        {
            try
            {
                var tokenData = this.jwtAuth.DecodeToken(token);
                if (tokenData != null)
                {
                    var result = _blc.editTask(taskParams , tokenData.UserId);
                    if (result != null)
                    {
                        return Ok(result);
                    }
                    return StatusCode(500, "Cannot Edit Task");
                }
                return Unauthorized("Invalid credentials");
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }
    }
}
