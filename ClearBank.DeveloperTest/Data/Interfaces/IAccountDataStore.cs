using ClearBank.DeveloperTest.Models;

namespace ClearBank.DeveloperTest.Data.Interfaces
{
    public interface IAccountDataStore
    {
        /// <summary>
        /// Gets the account details by the account number
        /// </summary>
        /// <param name="accountNumber">The account number</param>
        /// <returns>An entity Account from the db</returns>
        Account GetAccount(string accountNumber);

        /// <summary>
        /// Updates an account in the db
        /// </summary>
        /// <param name="account">The account object with new details to update</param>
        void UpdateAccount(Account account);
    }
}
