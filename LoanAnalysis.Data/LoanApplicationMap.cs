using System;
using System.Globalization;
using CsvHelper.Configuration;
using LoanAnalysis.Domain;

namespace LoanAnalysis.Data
{
    public class LoanApplicationMap : ClassMap<LoanApplication>
    {
        public LoanApplicationMap()
        {
            Map(m => m.Id).Convert(args => args.Row.Context.Parser.Row);

            Map(m => m.Applicant.Age).Name("person_age").Convert(args =>
            {
                var raw = args.Row.GetField("person_age");
                if (double.TryParse(raw, NumberStyles.Any, CultureInfo.InvariantCulture, out double val))
                    return (int)Math.Round(val);
                return 0;
            });

            Map(m => m.Applicant.Gender).Name("person_gender");
            Map(m => m.Applicant.Education).Name("person_education");
            Map(m => m.Applicant.Income).Name("person_income");
            Map(m => m.Applicant.EmploymentLength).Name("person_emp_exp");
            Map(m => m.Applicant.HomeOwnership).Name("person_home_ownership");

            Map(m => m.Amount).Name("loan_amnt");
            Map(m => m.Intent).Name("loan_intent");
            Map(m => m.InterestRate).Name("loan_int_rate");
            Map(m => m.ApprovalStatus).Name("loan_status");

            Map(m => m.CreditHistory.HistoricalDefault).Name("previous_loan_defaults_on_file").Convert(args =>
            {
                var val = args.Row.GetField("previous_loan_defaults_on_file");
                return val?.ToLower() == "y" || val == "1" ? 1 : 0;
            });

            Map(m => m.CreditHistory.CreditHistoryLength).Name("cb_person_cred_hist_length").Convert(args =>
            {
                var raw = args.Row.GetField("cb_person_cred_hist_length");
                if (double.TryParse(raw, NumberStyles.Any, CultureInfo.InvariantCulture, out double val))
                    return (int)Math.Round(val);
                return 0;
            });

            Map(m => m.CreditHistory.CreditScore).Name("credit_score").Convert(args =>
            {
                var raw = args.Row.GetField("credit_score");
                if (double.TryParse(raw, NumberStyles.Any, CultureInfo.InvariantCulture, out double val))
                    return (int)Math.Round(val);
                return 0;
            });
        }
    }
}