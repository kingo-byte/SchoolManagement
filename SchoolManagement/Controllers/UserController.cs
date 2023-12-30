using Microsoft.AspNetCore.Mvc;
using SchoolManagement.Models;
using SchoolManagement.Repository;

namespace SchoolManagement.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class UserController : ControllerBase
    {
        private readonly ILogger<UserController> _logger;

        private readonly DapperAccess _dapper;

        public UserController(
            ILogger<UserController> logger,
            DapperAccess dapper
            )
        {
            _logger = logger;
            _dapper = dapper;
        }

        [HttpPost(Name = "LoginOrSignUp")]
        public IActionResult LoginOrSignUp(User user)
        {
            try
            {
                object obj = new
                {
                    UserName = user.UserName,
                    Email = user.Email,
                    Password = user.Password,
                };

                if (user.Action == null)
                {
                    return Ok("Failure");
                }

                if (user.Action.Equals("SignIn"))
                {
                    User dbUser = _dapper.QueryFirst<User>("sp_Sign_In", obj);

                    if (dbUser == null)
                    {
                        return NotFound("User was not found");
                    }

                    return Ok(dbUser);
                }
                else
                {
                    _dapper.Execute("sp_Sign_Up", obj);

                    return Ok("Success");
                }
            } catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
    }
}
