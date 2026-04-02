using Microsoft.AspNetCore.Mvc;
using TaskTrackerApi.Models;

namespace TaskTrackerApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class TasksController : ControllerBase
    {
        private static readonly List<TaskItem> Tasks = new()
        {
            new TaskItem { Id = 1, Title = "Study C#", IsDone = false },
            new TaskItem { Id = 2, Title = "Build API", IsDone = true }
        };

        [HttpGet]
        public IActionResult GetTasks()
        {
            return Ok(Tasks);
        }

        [HttpPost]
        public IActionResult CreateTask([FromBody] TaskItem newTask)
        {
            newTask.Id = Tasks.Count == 0 ? 1 : Tasks.Max(t => t.Id) + 1;
            Tasks.Add(newTask);

            return CreatedAtAction(nameof(GetTasks), newTask);
        }

        [HttpPut("{id}")]
        public IActionResult UpdateTask(int id, [FromBody] TaskItem updatedTask)
        {
            var existingTask = Tasks.FirstOrDefault(t => t.Id == id);

            if (existingTask == null)
            {
                return NotFound();
            }

            existingTask.Title = updatedTask.Title;
            existingTask.IsDone = updatedTask.IsDone;

            return Ok(existingTask);
        }

        [HttpDelete("{id}")]
        public IActionResult DeleteTask(int id)
        {
            var task = Tasks.FirstOrDefault(t => t.Id == id);

            if (task == null)
            {
                return NotFound();
            }

            Tasks.Remove(task);
            return NoContent();
        }
    }
}
