using Exam.Data;
using Exam.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Exam.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LoanRequestsShowController : ControllerBase
    {
        private readonly WalletDbContext _context;

        public LoanRequestsShowController(WalletDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<LoanRequest>>> GetLoanRequests()
        {
            var loanRequests = await _context.LoanRequests.ToListAsync();
            return loanRequests;
        }
    }
}
