using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mime;
using System.Threading.Tasks;
using ClassApiDemo.DAL;
using ClassApiDemo.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity.UI.V3.Pages.Internal.Account;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace ClassApiDemo.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly ApiDemoContext _context;

        public UserController(ApiDemoContext context)
        {
            _context = context;
        }

        [HttpGet]
        public ActionResult<User> GetUsers()
        {
            var userList = new List<UserResponse>();
            var users = _context.Users.ToList();
            foreach(var user in users)
            {
                var taskList = new List<TaskResponse>();
                var tasks = _context.Tasks.Where(x => x.UserId == user.Id).ToList();
                foreach(var task in tasks)
                {
                    var tempTask = new TaskResponse
                    {
                        Id = task.Id,
                        Name = task.Name,
                        IsComplete = task.IsComplete,
                        UserId = task.UserId
                    };
                    taskList.Add(tempTask);
                }
                var tempUser = new UserResponse
                {
                    FirstName = user.FirstName,
                    LastName = user.LastName,
                    Tasks = taskList
                };
                userList.Add(tempUser);
            }
            return Ok(userList);
        }

        //api/user/5
        [HttpGet("{userId}")]
        public ActionResult<User> GetUser(int userId)
        {
            var user = _context.Users.Find(userId);
            return user;
        }

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        public ActionResult<User> Post(CreateUser user)
        {
            if(user == null)
            {
                return BadRequest();
            }

            var newUser = new User
            {
                FirstName = user.FirstName,
                LastName = user.LastName
            };
            _context.Add(newUser);
            _context.SaveChanges();

            return newUser;
        }
    }
}