using AutoMapper;
using Exam.Data;
using Exam.DTOs;
using Exam.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Exam.Controllers
{
    [Route("api/wallet")]
    [ApiController]
    public class TransactionsController : ControllerBase
    {
        private readonly WalletDbContext _context;
        private IMapper _mapper;

        public TransactionsController(WalletDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        [HttpPost("transfer")]
        public IActionResult TransferMoney([FromBody] TransferRequestDto transferRequestDto)
        {
            var sourceWallet = _context.Wallets.FirstOrDefault(w => w.WalletId == transferRequestDto.WalletId && w.User.UserName == transferRequestDto.SourceUsername && w.User.Password == transferRequestDto.SourcePassword);
            var destinationWallet = _context.Wallets.FirstOrDefault(w => w.WalletId == transferRequestDto.DestinationWalletId);

            if (sourceWallet == null || destinationWallet == null)
            {
                return BadRequest("Invalid source or destination wallet.");
            }

            if (sourceWallet.Blocked)
            {
                return BadRequest("Source wallet is blocked.");
            }

            if (sourceWallet.UserBalance < transferRequestDto.TransactionAmount)
            {
                sourceWallet.Blocked = true;
                return BadRequest("Transaction is failed due to insufficient balance.");
            }
            if (destinationWallet.UserBalance + transferRequestDto.TransactionAmount > 100000 && destinationWallet.Blocked)
            {
                destinationWallet.Blocked = false;
                _context.SaveChanges();
                return Ok("Wallet is unblock");
            }

            sourceWallet.UserBalance -= transferRequestDto.TransactionAmount;
            destinationWallet.UserBalance += transferRequestDto.TransactionAmount;

            var transaction = new Transaction
            {
                WalletId = sourceWallet.WalletId,
                DestinationWalletId = destinationWallet.WalletId,
                TransactionAmount = transferRequestDto.TransactionAmount
            };

            _context.Transactions.Add(transaction);
            _context.SaveChanges();

            return Ok("Money transfered successfully.");
        }

    }
}
