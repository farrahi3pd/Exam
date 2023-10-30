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
    public class CreateWalletController : ControllerBase
    {
        private readonly WalletDbContext _context;
        private readonly IMapper _mapper;

        public CreateWalletController(WalletDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        [HttpPost("create-wallet")]
        public async Task<IActionResult> CreateWallet([FromBody] CreateUserWalletDto userDto)
        {
            var existingUser = await _context.Users
                .Include(u => u.UserWallet)
                .FirstOrDefaultAsync(u => u.UserName == userDto.Username);

            if (existingUser == null)
            {
                var newUser = _mapper.Map<User>(userDto);
                newUser.UserWallet = new Wallet { UserBalance = 30000 };

                _context.Users.Add(newUser);
                await _context.SaveChangesAsync();

                return Ok("User and wallet are created successfully!");
            }
            else
            {
                return BadRequest("User with the same username is already existed!");
            }
        }
    }
}
