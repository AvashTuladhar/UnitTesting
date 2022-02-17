using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Practise.Models;
using Practise.Services;

namespace Practise.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserApiController : ControllerBase
    {
        private readonly IUserServices userServices;
        public UserApiController(IUserServices userServices)
        {
            this.userServices = userServices;
        }

        [HttpGet]
        public IActionResult Get()
        {
            return Ok(userServices.GetAllUsers());
        }

        [HttpGet("{Id}")]
        public IActionResult Get(int id)
        {
            var result = userServices.GetUserByID(id);
            if (result is null)
                return NotFound();

            return Ok(result);
        }

        [HttpPost]
        public IActionResult Create(User user)
        {
            if (ModelState.IsValid)
            {
                var result = userServices.CreateUser(user);
                return Ok(result);
            }
            return BadRequest(ModelState);
        }

        [HttpPut("{id}")]
        public IActionResult Update(int id, [FromBody] User user)
        {
            if(!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            if (id != user.Id)
                return BadRequest();
            var result = userServices.GetUserByID(id);
            if (result is null)
                return NotFound();
            return Ok(userServices.UpdateUser(user));
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            var result = userServices.GetUserByID(id);
            if (result is null)
                return NotFound();
            userServices.DeleteUser(result);
            return Accepted();
        }
    }
}
