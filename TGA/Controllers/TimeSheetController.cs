using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.Design;
using System.Numerics;
using TGA.Model;

namespace TGA.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TimeSheetController : ControllerBase
    {
        private UserDbContext _context { get; set; }
        public TimeSheetController(UserDbContext context)
        {
            _context = context;
        }

        [HttpPost("TimeSheetEntry")]
        public IActionResult TimeSheetEntry(Timesheet obj)
        {
             var userExistAlready = _context.Timesheets.Where(x =>x.Id==obj.Id && x.UserId == obj.UserId && x.WorkingDate.Date.Year == obj.WorkingDate.Date.Year && x.WorkingDate.Month==obj.WorkingDate.Month && x.WorkingDate.Day == obj.WorkingDate.Day).ToList();
            if (userExistAlready.Count>0)
            {
                _context.Timesheets.Update(obj);
                _context.SaveChanges();
                return Created("TimeSheet entry updated Successfully", obj);

            }
            else
            {               
                _context.Timesheets.Add(obj);
                _context.SaveChanges();
                return Created("TimeSheet entry added Successfully", obj);
            }
        }

        [HttpGet("GetAllTimeSheetEntry")]
        public IActionResult GetAllTimeSheetEntry()
        {
            var list = _context.Timesheets.ToList();            
            return Ok(list);
        }

        [HttpGet("GetTimeSheetEntryByUserId")]
        public IActionResult GetTimeSheetEntryByUserId(int UserId)
        {            
            var list = _context.Users.Include("Timesheets").Where(x=>x.Id==UserId).ToList();            
            return Ok(list);
        }


        [HttpGet("GetTimeSheetEntryByUserEmail")]
        public IActionResult GetTimeSheetEntryByUserEmail(string EmailId)
        {            
            var list = _context.Users.Include("Timesheets").Where(x => x.EmailId == EmailId).ToList();
            return Ok(list);
        }

        [HttpGet("GetTimeSheetEntryByUserDateRange")]
        public IActionResult GetTimeSheetEntryByUserDateRange(int UserId,DateTime date)
        {           
            var list = _context.Users.Where(x=>x.Id==UserId)
               .Include(ts => ts.Timesheets.Where(y => y.WorkingDate.Date >= date.Date && y.WorkingDate.Date <= date.Date)).ToList();            
            return Ok(list);
        }

        [HttpGet("GetTimeSheetEntryByDateRange")]
        public IActionResult GetTimeSheetEntryByDateRange(DateTime date)
        {           
            var list = _context.Timesheets.Where(y => y.WorkingDate.Date >= date.Date && y.WorkingDate.Date <= date.Date).ToList();               
            return Ok(list);
        }
    }
}
