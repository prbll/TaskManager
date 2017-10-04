using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Migrations;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using TaskManager.DataAccessLayer;
using TaskManager.DataAccessLayer.Models;
using TaskManager.Models;

namespace TaskManager.Controllers
{
    public class HomeController : Controller
    {
        private TaskContext _db;

        public HomeController()
        {
            _db = new TaskContext();
        }

        [HttpGet]
        public ActionResult TaskForm(string id)
        {
            var task=new Task();
            if (!string.IsNullOrEmpty(id))
            {
                task=_db.Tasks.FirstOrDefault(x => x.Id.ToString() == id);
            }
            return View(task);
        }

        [HttpPost]
        public ActionResult ProcessForm(NewTask model)
        {
            if (string.IsNullOrEmpty(model.Description))
            {
                ModelState.AddModelError("EmptyDescription", "Необходимо указать описание.");
            }
            if (DateTime.MinValue == model.Deadline)
            {
                ModelState.AddModelError("EmptyDeadline", "Необходимо указать дату дедлайна.");
            }
            if(!ModelState.IsValid) return View("TaskForm", new Task {Description = model.Description, Priority = model.Priority, Deadline = model.Deadline });
            _db.Tasks.Add(new Task
            {
                CreatedDate = DateTime.Now,
                Deadline = model.Deadline,
                Description = model.Description,
                Priority = model.Priority,
            });
            _db.SaveChanges();
            return RedirectToAction("Index");
        }

        public ActionResult Index()
        {
            // возвращаем представление
            return View(GetTasks());
        }

        [HttpGet]
        public ActionResult DeleteTask(long id)
        {
            var task = _db.Tasks.FirstOrDefault(x => x.Id == id);
            if (task == null)
            {
                //ModelState.AddModelError("NullTask", "Не существует такой задачи.");
                return RedirectToAction("Index");
            }
            else
            {
                _db.Entry(task).State = EntityState.Deleted;
                _db.SaveChanges();
                return RedirectToAction("Index");
            }
        }

        [HttpPost]
        public ActionResult EditTask(Task model)
        {
            if (string.IsNullOrEmpty(model.Description))
            {
                ModelState.AddModelError("EmptyDescription", "Необходимо указать описание.");
            }
            if (DateTime.MinValue == model.Deadline)
            {
                ModelState.AddModelError("EmptyDeadline", "Необходимо указать дату дедлайна.");
            }
            if (!ModelState.IsValid) return RedirectToAction("TaskForm", new Task { Id = model.Id, Description = model.Description, Priority = model.Priority, Deadline = model.Deadline });
            var task = _db.Tasks.FirstOrDefault(x => x.Id == model.Id);
            if (task == null)
            {
                //ModelState.AddModelError("NullTask", "Не существует такой задачи.");
                return RedirectToAction("Index");
            }
            else
            {
                task.Description = model.Description;
                task.Deadline = model.Deadline;
                task.Priority = model.Priority;
                _db.Entry(task).State = EntityState.Modified;
                _db.SaveChanges();
                return RedirectToAction("Index");
            }
        }

        private List<Task> GetTasks()
        {
            return _db.Tasks
                .OrderBy(x => x.CreatedDate)
                .ToList();
        }
}
}