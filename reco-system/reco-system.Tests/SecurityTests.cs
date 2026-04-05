using Xunit;
using reco_system.Models;
using System.ComponentModel.DataAnnotations;

namespace reco_system.Tests
{
    public class SecurityTests
    {
        private List<ValidationResult> ValidateModel(object model)
        {
            var results = new List<ValidationResult>();
            var context = new ValidationContext(model);
            Validator.TryValidateObject(model, context, results, true);
            return results;
        }

        [Fact]
        public void Login_WithSQLInjection_ShouldFailValidation()
        {
            var model = new LoginViewModel
            {
                Email = "' OR '1'='1",
                Password = "password"
            };
            var results = ValidateModel(model);
            Assert.NotEmpty(results);
        }

        [Fact]
        public void Login_WithXSS_ShouldFailEmailValidation()
        {
            var model = new LoginViewModel
            {
                Email = "<script>alert('xss')</script>",
                Password = "password"
            };
            var results = ValidateModel(model);
            Assert.NotEmpty(results);
        }

        [Fact]
        public void Register_WithVeryLongName_ShouldFailValidation()
        {
            var model = new RegisterViewModel
            {
                FullName = new string('A', 200),
                Email = "test@example.com",
                Password = "pass123",
                ConfirmPassword = "pass123"
            };
            var results = ValidateModel(model);
            Assert.NotEmpty(results);
        }

        [Fact]
        public void Book_RatingBelowZero_ShouldBeInvalid()
        {
            var book = new Book { Rating = -1 };
            var results = new List<ValidationResult>();
            var context = new ValidationContext(book);
            Validator.TryValidateObject(book, context, results, true);
            Assert.NotEmpty(results);
        }

        [Fact]
        public void Book_RatingAboveFive_ShouldBeInvalid()
        {
            var book = new Book { Rating = 6 };
            var results = new List<ValidationResult>();
            var context = new ValidationContext(book);
            Validator.TryValidateObject(book, context, results, true);
            Assert.NotEmpty(results);
        }

        [Fact]
        public void AppUser_DefaultPlanShouldBeFree()
        {
            var user = new AppUser();
            Assert.Equal("Free", user.Plan);
        }

        [Fact]
        public void AppUser_PremiumPlanShouldBeSettable()
        {
            var user = new AppUser { Plan = "Premium" };
            Assert.Equal("Premium", user.Plan);
        }
    }
}