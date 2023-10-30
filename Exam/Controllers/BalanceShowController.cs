using AutoMapper;
using Exam.Data;
using Exam.DTOs;
using Exam.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Exam.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BalanceShowController : ControllerBase
    {
        private readonly WalletDbContext _context;
        private IMapper _mapper;

        public BalanceShowController(WalletDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        [HttpPost("get-balance")]
        public ActionResult<decimal> GetBalance(UserDto userDto)
        {
            var userWallet = _context.Wallets.SingleOrDefault(w => w.User.UserName == userDto.UserName && w.User.Password == userDto.Password);

            if (userWallet == null)
            {
                return NotFound("User not found");
            }

            return Ok(userWallet.UserBalance);
        }

    }
}

