using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using TaskManager.Models;
using Microsoft.AspNetCore.Identity;
namespace TaskManager.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly AppDBContext _context;

        public HomeController(ILogger<HomeController> logger, AppDBContext context)
        {
            _logger = logger;
            _context = context;
        }

        //顯示注冊頁面
        [HttpGet]
        public IActionResult Register()
        {
            return View();
        }
        //接受注冊資料
        [HttpPost]
        public IActionResult Register(RegisterViewModel model)
        {
            if (ModelState.IsValid)
            {
                var member = new Member
                {
                    Name = model.Name,
                    Email = model.Email
                };

                var passwordHasher = new PasswordHasher<Member>();
                member.PasswordHash = passwordHasher.HashPassword(member, model.Password);
                _context.Members.Add(member);
                _context.SaveChanges();
                TempData["msg"] = "注冊成功,請登入";
                return RedirectToAction("Login");
            }
            return View(model);

        }

        public IActionResult CheckRepeatEmail(string email)
        {
            var isEmailRepeat=_context.Members.Any(x => x.Email==email);
            if(isEmailRepeat)
            {
                return Json($"{email} is already in use.");
            }
            return Json(true);
        }

        [HttpGet]
        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]  //防止CSRF攻擊
        public IActionResult Login(LoginViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }
            var member = _context.Members.FirstOrDefault(m => m.Email == model.Email);
            if(member!=null)
            {
                var passwordHasher = new PasswordHasher<Member>();
                var result = passwordHasher.VerifyHashedPassword(member, member.PasswordHash, model.Password);
                if (result == PasswordVerificationResult.Success)
                {
                    HttpContext.Session.SetString("UserName", member.Name);
                    return RedirectToAction("Welcome");
                }
            }
            ModelState.AddModelError("Password", "帳號或密碼錯誤");
            return View(model);
        }
        public IActionResult Welcome()
        {
            var name = HttpContext.Session.GetString("UserName");
            if(string.IsNullOrEmpty(name))
            {
                return RedirectToAction("Login");
            }
            ViewBag.Name = name;
            return View();
        }

        [HttpGet]
        public IActionResult Logout()
        {
            HttpContext.Session.Clear();
            TempData["WelcomeMsg"] = "你已登出";
            return RedirectToAction("Login");
        }

        public JsonResult OnSubmit()
        {
            string name = Request.Form["name"];
            string isGoing = Request.Form["isGoing"];
            string returnAddress = Request.Form["returnAddress"];
            string value = Request.Form["value"];
            return Json(value);
        }

        //[HttpPost]
        //public JsonResult CheckEmail(string email)
        //{
        //    bool exits = _context.Members.Any(m => m.Email == email);
        //    return Json(exits);
        //}
        //顯示任務列表
        public IActionResult Index()
        {
            var tasks=_context.Tasks.OrderBy(t=>t.DueDate).ToList();
            return View(tasks);
        }

        public IActionResult Details(int id)
        {
            var tasks=_context.Tasks.FirstOrDefault(t=>t.Id==id);
            if (tasks == null) return NotFound();
            return View(tasks);
        }
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Create(TaskItem task)
        {
            if (ModelState.IsValid)
            {
                _context.Tasks.Add(task);
                _context.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(task);
        }
        public IActionResult Edit(int id)
        {
            var task = _context.Tasks.FirstOrDefault(t => t.Id == id);
            if (task == null) return NotFound();
            return View(task);
        }

        // 編輯任務 POST
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(TaskItem task)
        {
            if (ModelState.IsValid)
            {
                _context.Tasks.Update(task);
                _context.SaveChanges();
                return RedirectToAction(nameof(Index));
            }
            return View(task);
        }
        public IActionResult Delete(int id)
        {
            var task = _context.Tasks.FirstOrDefault(t => t.Id == id);
            if (task == null) return NotFound();
            return View(task);
        }

        // 刪除任務 POST
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public IActionResult DeleteConfirmed(int id)
        {
            var task = _context.Tasks.FirstOrDefault(t => t.Id == id);
            if (task != null)
            {
                _context.Tasks.Remove(task);
                _context.SaveChanges();
            }
            return RedirectToAction(nameof(Index));
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
