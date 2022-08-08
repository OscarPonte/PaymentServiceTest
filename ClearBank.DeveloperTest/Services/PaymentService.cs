using ClearBank.DeveloperTest.Data;
using ClearBank.DeveloperTest.Data.Interfaces;
using ClearBank.DeveloperTest.Enums;
using ClearBank.DeveloperTest.Models;
using ClearBank.DeveloperTest.Services.Interfaces;
using System;
using System.Configuration;

namespace ClearBank.DeveloperTest.Services
{
    public class PaymentService : IPaymentService
    {
        private readonly string _dataStore = ConfigurationManager.AppSettings["DataStoreType"] ?? string.Empty;

        private readonly IAccountDataStore _accountDataStore;
        private readonly IBackupAccountDataStore _backupAccountDataStore;

        public PaymentService(IAccountDataStore accountDataStore, IBackupAccountDataStore backupAccountDataStore)
        {
            _accountDataStore = accountDataStore;
            _backupAccountDataStore = backupAccountDataStore;
        }

        public MakePaymentResult MakePayment(MakePaymentRequest request)
        {
            var result = new MakePaymentResult();

            try
            {
                if (request == null)
                    return result;

                var dataStoreType = Enum.TryParse<DataStoreType>(_dataStore, out var parsedType) ? parsedType : DataStoreType.Current;

                Account account;

                if (dataStoreType == DataStoreType.Backup)                
                    account = _backupAccountDataStore.GetAccount(request.DebitOrAccountNumber);                
                else                
                    account = _accountDataStore.GetAccount(request.DebitOrAccountNumber);                

                result.Success = IsPaymentRequesValid(account, request);

                if (result.Success)
                {
                    account.Balance -= request.Amount;

                    if (dataStoreType == DataStoreType.Backup)
                        _backupAccountDataStore.UpdateAccount(account);
                    else                    
                        _accountDataStore.UpdateAccount(account);                    
                }
            }
            catch (Exception)
            {
                // log and handle exception
                throw;
            }           

            return result;
        }

        private bool IsPaymentRequesValid(Account account, MakePaymentRequest request)
        {
            if (account == null)
                return false;

            switch (request.PaymentScheme)
            {
                case PaymentScheme.Bacs:
                    return account.AllowedPaymentSchemes.HasFlag(AllowedPaymentSchemes.Bacs);

                case PaymentScheme.FasterPayments:
                    return account.AllowedPaymentSchemes.HasFlag(AllowedPaymentSchemes.FasterPayments) && account.Balance >= request.Amount;

                case PaymentScheme.Chaps:
                    return account.AllowedPaymentSchemes.HasFlag(AllowedPaymentSchemes.Chaps) && account.Status == AccountStatus.Live;

                default:
                    return false;
            }
        }
    }
}
