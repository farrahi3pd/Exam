namespace Exam.Models
{
    public class Wallet
    {
        public int WalletId { get; set; }
        public int UserId { get; set; }
        public double UserBalance { get; set; }
        public bool Blocked { get; set; }
        public virtual User User { get; set; }
        public ICollection<LoanRequest> UserLoanRequest { get; } = new List<LoanRequest>();
        public ICollection<Transaction> WalletTransaction { get; } = new List<Transaction>();


    }
}
