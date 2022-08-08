using ClearBank.DeveloperTest.Models;

namespace ClearBank.DeveloperTest.Data.Interfaces
{
    public interface IBackupAccountDataStore
    {
        /// <summary>
        /// Gets the account details by the account number from the backup db
        /// </summary>
        /// <param name="accountNumber">The account number</param>
        /// <returns>An entity Account from the backup db</returns>
        Account GetAccount(string accountNumber);

        /// <summary>
        /// Updates an account in the backup db
        /// </summary>
        /// <param name="account">The account object with new details to update</param>
        void UpdateAccount(Account account);
    }
}
