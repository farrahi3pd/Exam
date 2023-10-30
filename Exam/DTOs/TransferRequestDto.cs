namespace Exam.DTOs
{
    public class TransferRequestDto
    {
        public int WalletId { get; set; }
        public int DestinationWalletId { get; set; }
        public string SourceUsername { get; set; }
        public string SourcePassword { get; set; }
        public double TransactionAmount { get; set; }
    }
}
