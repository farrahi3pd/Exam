using AutoMapper;
using Exam.Data;
using Exam.DTOs;
using Exam.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Exam.Controllers
{
    [Route("api/loan")]
    [ApiController]
    public class LoanRequestsController : ControllerBase
    {
        private readonly WalletDbContext _context;
        private readonly IMapper _mapper;

        public LoanRequestsController(WalletDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        [HttpPost]
        public async Task<IActionResult> RequestLoan([FromBody] LoanRequestDto loanRequestDto)
        {
            var user = _context.Wallets.FirstOrDefault(w => w.User.UserName == loanRequestDto.UserName && w.User.Password == loanRequestDto.Password);



            if (user == null)
            {
                return BadRequest("Invalid username or password.");
            }

            if (user.Blocked)
            {
                return BadRequest("User is blocked.");
            }

            if (user.UserBalance < loanRequestDto.LoanAmount / 2)
            {
                return BadRequest("Insufficient wallet balance.");
            }

            var loanRequest = new LoanRequest
            {
                WalletId = user.WalletId,
                RequestAmount = loanRequestDto.LoanAmount,
                ApplicantUserValidity = user.UserBalance
            };

            _context.LoanRequests.Add(loanRequest);
            await _context.SaveChangesAsync();

            return Ok("Loan request submitted.");
        }
    }
}