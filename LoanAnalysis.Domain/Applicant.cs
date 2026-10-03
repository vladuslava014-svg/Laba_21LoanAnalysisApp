namespace LoanAnalysis.Domain
{
    public class Applicant
    {
        public int Age { get; set; }
        public double Income { get; set; }
        public string HomeOwnership { get; set; } = string.Empty;
        public double EmploymentLength { get; set; }
        public string Gender { get; set; } = string.Empty;
        public string Education { get; set; } = string.Empty;
    }
}
