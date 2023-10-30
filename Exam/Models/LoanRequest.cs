namespace Exam.Models
{
    public class LoanRequest
    {
        public int LoanRequestId { get; set; }
        public int WalletId { get; set; }
        public DateTime RequestDate { get; set; }
        public double RequestAmount { get; set; }
        public double ApplicantUserValidity { get; set; }
        public virtual Wallet Wallet { get; set; }

    }
}
