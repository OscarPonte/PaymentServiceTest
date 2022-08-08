using ClearBank.DeveloperTest.Enums;
using System;

namespace ClearBank.DeveloperTest.Models
{
    public class MakePaymentRequest
    {
        public string CreditOrAccountNumber { get; set; }

        public string DebitOrAccountNumber { get; set; }

        public decimal Amount { get; set; }

        public DateTime PaymentDate { get; set; }

        public PaymentScheme PaymentScheme { get; set; }
    }
}
