using ClearBank.DeveloperTest.Data.Interfaces;
using ClearBank.DeveloperTest.Enums;
using ClearBank.DeveloperTest.Models;
using ClearBank.DeveloperTest.Services;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System;

namespace ClearBank.DeveloperTest.Tests.Services
{
	[TestClass]
	public class PaymentServiceTest
	{
		[TestMethod]
		public void MakePayment_WithCorrectRequest_ReturnsSuccessful()
		{
			// Arrange
			MakePaymentRequest request = new MakePaymentRequest { Amount = 5m, DebitOrAccountNumber = "123", PaymentDate = DateTime.Now, PaymentScheme = PaymentScheme.Bacs};

			Mock<IAccountDataStore> mockAccountDataStore = new Mock<IAccountDataStore>();
			Mock<IBackupAccountDataStore> mockBackupAccountDataStore = new Mock<IBackupAccountDataStore>();

			var account = new Account { AccountNumber = "123", AllowedPaymentSchemes = AllowedPaymentSchemes.Bacs, Balance = 10m, Status = AccountStatus.Live };

			mockAccountDataStore.Setup(x => x.GetAccount(It.Is<string>(x => !string.IsNullOrEmpty(x)))).Returns(account);
			mockAccountDataStore.Setup(x => x.UpdateAccount(It.IsAny<Account>()));
			mockBackupAccountDataStore.Setup(x => x.GetAccount(It.Is<string>(x => !string.IsNullOrEmpty(x)))).Returns(account);
			mockBackupAccountDataStore.Setup(x => x.UpdateAccount(It.IsAny<Account>()));

			var paymentService = new PaymentService(mockAccountDataStore.Object, mockBackupAccountDataStore.Object);

			// Act
			var result = paymentService.MakePayment(request);

			// Assert
			Assert.IsTrue(result.Success);
		}

		[TestMethod]
		public void MakePayment_WithWrongAccountNumber_ReturnsUnsuccessful()
		{
			// Arrange
			MakePaymentRequest request = new MakePaymentRequest { Amount = 5m, DebitOrAccountNumber = null, PaymentDate = DateTime.Now, PaymentScheme = PaymentScheme.Bacs };

			Mock<IAccountDataStore> mockAccountDataStore = new Mock<IAccountDataStore>();
			Mock<IBackupAccountDataStore> mockBackupAccountDataStore = new Mock<IBackupAccountDataStore>();

			var account = new Account { AccountNumber = "123", AllowedPaymentSchemes = AllowedPaymentSchemes.Bacs, Balance = 10m, Status = AccountStatus.Live };

			mockAccountDataStore.Setup(x => x.GetAccount(It.Is<string>(x => !string.IsNullOrEmpty(x)))).Returns(account);
			mockAccountDataStore.Setup(x => x.UpdateAccount(It.IsAny<Account>()));
			mockBackupAccountDataStore.Setup(x => x.GetAccount(It.Is<string>(x => !string.IsNullOrEmpty(x)))).Returns(account);
			mockBackupAccountDataStore.Setup(x => x.UpdateAccount(It.IsAny<Account>()));

			var paymentService = new PaymentService(mockAccountDataStore.Object, mockBackupAccountDataStore.Object);

			// Act
			var result = paymentService.MakePayment(request);

			// Assert
			Assert.IsFalse(result.Success);
		}

		[TestMethod]
		public void MakePayment_WithLowBalance_ReturnsUnsuccessful()
		{
			// Arrange
			MakePaymentRequest request = new MakePaymentRequest { Amount = 15m, DebitOrAccountNumber = "123", PaymentDate = DateTime.Now, PaymentScheme = PaymentScheme.FasterPayments };

			Mock<IAccountDataStore> mockAccountDataStore = new Mock<IAccountDataStore>();
			Mock<IBackupAccountDataStore> mockBackupAccountDataStore = new Mock<IBackupAccountDataStore>();

			var account = new Account { AccountNumber = "123", AllowedPaymentSchemes = AllowedPaymentSchemes.FasterPayments, Balance = 10m, Status = AccountStatus.Live };

			mockAccountDataStore.Setup(x => x.GetAccount(It.Is<string>(x => !string.IsNullOrEmpty(x)))).Returns(account);
			mockAccountDataStore.Setup(x => x.UpdateAccount(It.IsAny<Account>()));
			mockBackupAccountDataStore.Setup(x => x.GetAccount(It.Is<string>(x => !string.IsNullOrEmpty(x)))).Returns(account);
			mockBackupAccountDataStore.Setup(x => x.UpdateAccount(It.IsAny<Account>()));

			var paymentService = new PaymentService(mockAccountDataStore.Object, mockBackupAccountDataStore.Object);			

			// Act
			var result = paymentService.MakePayment(request);

			// Assert
			Assert.IsFalse(result.Success);
		}

		[TestMethod]
		public void MakePayment_WithAccountDisabled_ReturnsUnsuccessful()
		{
			// Arrange
			MakePaymentRequest request = new MakePaymentRequest { Amount = 5m, DebitOrAccountNumber = "123", PaymentDate = DateTime.Now, PaymentScheme = PaymentScheme.Chaps };

			Mock<IAccountDataStore> mockAccountDataStore = new Mock<IAccountDataStore>();
			Mock<IBackupAccountDataStore> mockBackupAccountDataStore = new Mock<IBackupAccountDataStore>();

			var account = new Account { AccountNumber = "123", AllowedPaymentSchemes = AllowedPaymentSchemes.Chaps, Balance = 10m, Status = AccountStatus.Disabled };

			mockAccountDataStore.Setup(x => x.GetAccount(It.Is<string>(x => !string.IsNullOrEmpty(x)))).Returns(account);
			mockAccountDataStore.Setup(x => x.UpdateAccount(It.IsAny<Account>()));
			mockBackupAccountDataStore.Setup(x => x.GetAccount(It.Is<string>(x => !string.IsNullOrEmpty(x)))).Returns(account);
			mockBackupAccountDataStore.Setup(x => x.UpdateAccount(It.IsAny<Account>()));

			var paymentService = new PaymentService(mockAccountDataStore.Object, mockBackupAccountDataStore.Object);

			// Act
			var result = paymentService.MakePayment(request);

			// Assert
			Assert.IsFalse(result.Success);
		}

		[TestMethod]
		public void MakePayment_WithNullRequest_ReturnsUnsuccessful()
		{
			// Arrange
			MakePaymentRequest request = null;

			Mock<IAccountDataStore> mockAccountDataStore = new Mock<IAccountDataStore>();
			Mock<IBackupAccountDataStore> mockBackupAccountDataStore = new Mock<IBackupAccountDataStore>();

			var account = new Account { AccountNumber = "123", AllowedPaymentSchemes = AllowedPaymentSchemes.Bacs, Balance = 10m, Status = AccountStatus.Live };

			mockAccountDataStore.Setup(x => x.GetAccount(It.Is<string>(x => !string.IsNullOrEmpty(x)))).Returns(account);
			mockAccountDataStore.Setup(x => x.UpdateAccount(It.IsAny<Account>()));
			mockBackupAccountDataStore.Setup(x => x.GetAccount(It.Is<string>(x => !string.IsNullOrEmpty(x)))).Returns(account);
			mockBackupAccountDataStore.Setup(x => x.UpdateAccount(It.IsAny<Account>()));

			var paymentService = new PaymentService(mockAccountDataStore.Object, mockBackupAccountDataStore.Object);

			// Act
			var result = paymentService.MakePayment(request);

			// Assert
			Assert.IsFalse(result.Success);
		}
	}
}
