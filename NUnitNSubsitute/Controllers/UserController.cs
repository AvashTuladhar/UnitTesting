using Microsoft.AspNetCore.Mvc;
using Practise.Models;
using Practise.Services;

namespace Practise.Controllers
{
    [Route("[controller]/[action]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IUserServices userServices;
        public UserController(IUserServices userServices)
        {
            this.userServices = userServices;
        }

        [HttpGet]
        [ActionName("GetAll")]
        public ActionResult<List<User>> GetAll()
        {
            return Ok(userServices.GetAllUsers());
        }

        [HttpGet]
        [ActionName("GetById")]
        public ActionResult<User> GetByID(int Id)
        {
            var result = userServices.GetUserByID(Id);
            if (result == null)
            {
                return NotFound();
            }
            return Ok(result);
        }

        [HttpPost]
        public ActionResult<User> Create(User user)
        {
            if (ModelState.IsValid)
            {
                return Ok(userServices.CreateUser(user));
            }
            else
            {
                return BadRequest(ModelState);
            }
        }

        [HttpGet]
        [ActionName("Delete")]
        public ActionResult<User> DeleteUser(int Id)
        {
            var result = userServices.GetUserByID(Id);
            if (result == null)
            {
                return NotFound();
            }
            else
            {
                userServices.DeleteUser(result);
                return Accepted();
            }
        }
    }
}
