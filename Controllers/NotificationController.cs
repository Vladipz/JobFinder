using JobFinder.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Security.AccessControl;

namespace JobFinder.Controllers
{
	public class NotificationController : Controller
	{
		private readonly INotificationRepository _notificationRepository;

		public NotificationController(INotificationRepository notificationRepository)
        {
			_notificationRepository = notificationRepository;
		}
        // GET: NotificationController
        public ActionResult Index()
		{
			var userNotifications =  _notificationRepository.GetAllUserNotifications();
			return View(userNotifications);
		}

		// GET: NotificationController/Details/5
		public ActionResult Details(int id)
		{
			return View();
		}

		// GET: NotificationController/Create
		public ActionResult Create()
		{
			return View();
		}

		// POST: NotificationController/Create
		[HttpPost]
		[ValidateAntiForgeryToken]
		public ActionResult Create(IFormCollection collection)
		{
			try
			{
				return RedirectToAction(nameof(Index));
			}
			catch
			{
				return View();
			}
		}

		// GET: NotificationController/Edit/5
		public ActionResult Edit(int id)
		{
			return View();
		}

		// POST: NotificationController/Edit/5
		[HttpPost]
		[ValidateAntiForgeryToken]
		public ActionResult Edit(int id, IFormCollection collection)
		{
			try
			{
				return RedirectToAction(nameof(Index));
			}
			catch
			{
				return View();
			}
		}

		// GET: NotificationController/Delete/5
		public ActionResult Delete(int id)
		{
			return View();
		}

		// POST: NotificationController/Delete/5
		[HttpPost]
		[ValidateAntiForgeryToken]
		public ActionResult Delete(int id, IFormCollection collection)
		{
			try
			{
				return RedirectToAction(nameof(Index));
			}
			catch
			{
				return View();
			}
		}
	}
}
