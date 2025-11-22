using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Identity.Client;
using TGA.Model;

namespace TGA.Controllers
{
    //[Route("api/[controller]")]
    //[ApiController]
    //[EnableCors("allowCors")]
    public class UserController : Controller
    {
        private readonly UserDbContext _context;

        public UserController(UserDbContext context)
        {
            _context = context;
        }

        [HttpPost("CreateNewUser")]
        public IActionResult CreateUser(User obj)
        {
            var userExistAlready = _context.Users.SingleOrDefault(x => x.EmailId == obj.EmailId);
            if (userExistAlready ==null)
            {
                _context.Users.Add(obj);
                _context.SaveChanges();
                return Created("User Registred Success", obj);
            }
            else
            {
                return StatusCode(500, "Email already Exisit");
            }
            
        }

        [HttpPost("Login")]
        public IActionResult Login(UserLogin userLogin)
        {
            var user = _context.Users.SingleOrDefault(x => x.EmailId == userLogin.emailId && x.Password == userLogin.Password);
            if (user == null) {
                return StatusCode(401,"Incorrect User name or password");
            }
            return StatusCode(200, user);
        }

        [HttpGet]
        public IActionResult GetUsers()
        {
            var userList= _context.Users.ToList();
            return Ok(userList);
        }

        public class UserLogin
        {
            public string emailId { get; set; }
            public string Password { get; set; }
        }
        public IActionResult Index()
        {
            return View();
        }
    }
}
