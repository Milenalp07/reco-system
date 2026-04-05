using Xunit;
using System.ComponentModel.DataAnnotations;
using reco_system.Models;

namespace reco_system.Tests
{
    public class RegisterViewModelTests
    {
        private List<ValidationResult> ValidateModel(object model)
        {
            var results = new List<ValidationResult>();
            var context = new ValidationContext(model);
            Validator.TryValidateObject(model, context, results, true);
            return results;
        }

        [Fact]
        public void Register_ValidModel_ShouldPassValidation()
        {
            var model = new RegisterViewModel { FullName = "John Doe", Email = "john@example.com", Password = "pass123", ConfirmPassword = "pass123" };
            var results = ValidateModel(model);
            Assert.Empty(results);
        }

        [Fact]
        public void Register_EmptyFullName_ShouldFailValidation()
        {
            var model = new RegisterViewModel { FullName = "", Email = "john@example.com", Password = "pass123", ConfirmPassword = "pass123" };
            var results = ValidateModel(model);
            Assert.NotEmpty(results);
        }

        [Fact]
        public void Register_PasswordMismatch_ShouldFailValidation()
        {
            var model = new RegisterViewModel { FullName = "John Doe", Email = "john@example.com", Password = "pass123", ConfirmPassword = "different" };
            var results = ValidateModel(model);
            Assert.NotEmpty(results);
        }

        [Fact]
        public void Register_ShortPassword_ShouldFailValidation()
        {
            var model = new RegisterViewModel { FullName = "John Doe", Email = "john@example.com", Password = "123", ConfirmPassword = "123" };
            var results = ValidateModel(model);
            Assert.NotEmpty(results);
        }

        [Fact]
        public void Register_InvalidEmail_ShouldFailValidation()
        {
            var model = new RegisterViewModel { FullName = "John Doe", Email = "invalidemail", Password = "pass123", ConfirmPassword = "pass123" };
            var results = ValidateModel(model);
            Assert.NotEmpty(results);
        }
    }
}