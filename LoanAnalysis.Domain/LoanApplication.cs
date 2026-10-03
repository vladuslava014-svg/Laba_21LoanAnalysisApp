namespace LoanAnalysis.Domain
{
    public class LoanApplication
    {
        public int Id { get; set; }
        public double Amount { get; set; }
        public string Intent { get; set; } = string.Empty;
        public double InterestRate { get; set; }
        public int ApprovalStatus { get; set; } 

        public Applicant Applicant { get; set; } = new Applicant();
        public CreditHistory CreditHistory { get; set; } = new CreditHistory();
    }
}
