namespace Exam.Models
{
    public class Transaction
    {
        public int TransactionId { get; set; }
        public int WalletId { get; set; }
        public int DestinationWalletId { get; set; }
        public double TransactionAmount { get; set; }
        public virtual Wallet Wallet { get; set; }

    }
}
