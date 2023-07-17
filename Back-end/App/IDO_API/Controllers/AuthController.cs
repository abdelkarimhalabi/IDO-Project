using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using IDO_API.Tools;
using IDO_API.Request_Params;

namespace IDO_API.Controllers
{
    [Route("Auth")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private JWT_AUTH jwtAuth;
        private IConfiguration _conf;
        private readonly BLC _blc;
        public AuthController(IConfiguration conf , BLC blc)
        {
            this._blc = blc;
            this._conf = conf;
            this.jwtAuth = new JWT_AUTH(_conf["SecretKey"]);
        }

        [Route("Login")]
        [HttpPost]
        public ActionResult<string> userLogin([FromBody] LoginParams loginParams)
        {
            try
            {
                var loginResult = this._blc.Login(loginParams);
                if (loginResult != null)
                {
                    string token = this.jwtAuth.EncodeToken(loginResult);
                    return Ok(token);
                }
                return Unauthorized("Invalid credentials");
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}