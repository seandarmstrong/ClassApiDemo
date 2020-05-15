using ClassApiDemo.DAL;
using ClassApiDemo.Models;
using Microsoft.AspNetCore.Identity.UI.V3.Pages.Internal.Account;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using System.Threading.Tasks;

namespace ClassApiDemo.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TaskController : ControllerBase
    {
        private readonly ApiDemoContext _context;

        public TaskController(ApiDemoContext context)
        {
            _context = context;
        }

        [HttpPost]
        public ActionResult Post(CreateTask model)
        {
            if(model == null)
            {
                return BadRequest();
            }

            var newTask = new DAL.Task
            {
                Name = model.Name,
                IsComplete = model.IsComplete,
                UserId = model.UserId
            };

            _context.Add(newTask);
            _context.SaveChanges();

            return Ok(newTask);
        }

        [HttpPatch]
        public ActionResult Patch(DAL.Task userTask)
        {
            var task = _context.Tasks.Find(userTask.Id);
            task.IsComplete = userTask.IsComplete;

            _context.Update(task);
            _context.SaveChanges();
            return Ok(task);
        }
    }
}
