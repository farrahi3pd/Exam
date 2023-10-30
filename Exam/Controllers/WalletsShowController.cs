using Exam.Data;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Exam.Controllers
{
    [Route("api/wallets")]
    [ApiController]
    public class WalletsShowController : ControllerBase
    {
        private readonly WalletDbContext _context;

        public WalletsShowController(WalletDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        public IActionResult GetWallets()
        {
            var wallets = _context.Wallets
                .Include(p => p.User)
                .OrderBy(p => p.UserBalance)
                .Select(p => new
                {
                    UserFirstName = p.User.FirstName,
                    UserLastName = p.User.LastName,
                    UserBalance = p.UserBalance

                })
                .ToList();
            return Ok(wallets);
        }
    }
}