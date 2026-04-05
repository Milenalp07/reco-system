using Xunit;
using System.ComponentModel.DataAnnotations;
using reco_system.Models;

namespace reco_system.Tests
{
    public class LoginViewModelTests
    {
        private List<ValidationResult> ValidateModel(object model)
        {
            var results = new List<ValidationResult>();
            var context = new ValidationContext(model);
            Validator.TryValidateObject(model, context, results, true);
            return results;
        }

        [Fact]
        public void LoginViewModel_ValidEmailAndPassword_ShouldPassValidation()
        {
            var model = new LoginViewModel { Email = "test@example.com", Password = "password123" };
            var results = ValidateModel(model);
            Assert.Empty(results);
        }

        [Fact]
        public void LoginViewModel_EmptyEmail_ShouldFailValidation()
        {
            var model = new LoginViewModel { Email = "", Password = "password123" };
            var results = ValidateModel(model);
            Assert.NotEmpty(results);
        }

        [Fact]
        public void LoginViewModel_InvalidEmail_ShouldFailValidation()
        {
            var model = new LoginViewModel { Email = "notanemail", Password = "password123" };
            var results = ValidateModel(model);
            Assert.NotEmpty(results);
        }

        [Fact]
        public void LoginViewModel_EmptyPassword_ShouldFailValidation()
        {
            var model = new LoginViewModel { Email = "test@example.com", Password = "" };
            var results = ValidateModel(model);
            Assert.NotEmpty(results);
        }
    }
}