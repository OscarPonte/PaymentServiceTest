using ClearBank.DeveloperTest.Models;

namespace ClearBank.DeveloperTest.Services.Interfaces
{
    public interface IPaymentService
    {
        /// <summary>
        /// Process the payment request
        /// </summary>
        /// <param name="request">The payment request to be processed</param>
        /// <returns>A payment result object with the output from this process</returns>
        MakePaymentResult MakePayment(MakePaymentRequest request);
    }
}
