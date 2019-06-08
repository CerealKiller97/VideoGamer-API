using System.Linq;
using FluentAssertions;
using SharedModels.DTO;
using SharedModels.Fluent.User;
using Xunit;

namespace VideoGamerTests.User
{
    public class UserValidationTests
    {
        [Fact]
        public void DoesntReturnError_WhenFirstNameIsNotNull()
        {
            var user = new Register { FirstName = "Stefan"};

            var validator = new RegisterFluentValidator();

            var result = validator.Validate(user);
            var error = result.Errors.Where(err => err.ErrorMessage == "First name is required.");

            error.Should().HaveCount(0);
        }

        [Fact]
        public void ReturnsAnError_WhenFirstNameIsNull()
        {
            var user = new Register { FirstName = ""};

            var validator = new RegisterFluentValidator();

            var result = validator.Validate(user);
            var error = result.Errors.Where(err => err.ErrorMessage == "First name is required.");

            error.Should().HaveCount(1);
        }

        [Fact]
        public void ReturnsAnError_WhenFirstNameDoesntMatchRegex()
        {
            var user = new Register { FirstName = "asdasda"};

            var validator = new RegisterFluentValidator();

            var result = validator.Validate(user);
            var error = result.Errors.Where(err => err.ErrorMessage == "First name must start with capital letter.");

            error.Should().HaveCount(1);
        }

        [Fact]
        public void DoesntReturnError_WhenFirstNameMatchesRegex()
        {
            var user = new Register { FirstName = "Stefan"};

            var validator = new RegisterFluentValidator();

            var result = validator.Validate(user);
            var error = result.Errors.Where(err => err.ErrorMessage == "First name must start with capital letter.");

            error.Should().HaveCount(0);
        }
        
        [Fact]
        public void DoesntReturnError_WhenLastNameIsNotNull()
        {
            var user = new Register { LastName = "Bogdanovic"};

            var validator = new RegisterFluentValidator();

            var result = validator.Validate(user);
            var error = result.Errors.Where(err => err.ErrorMessage == "Last name is required.");

            error.Should().HaveCount(0);
        }

        [Fact]
        public void ReturnsAnError_WhenLastNameIsNull()
        {
            var user = new Register { LastName = ""};

            var validator = new RegisterFluentValidator();

            var result = validator.Validate(user);
            var error = result.Errors.Where(err => err.ErrorMessage == "Last name is required.");

            error.Should().HaveCount(1);
        }

        [Fact]
        public void ReturnsAnError_WhenLastNameDoesntMatchRegex()
        {
            var user = new Register { LastName = "asdasda"};

            var validator = new RegisterFluentValidator();

            var result = validator.Validate(user);
            var error = result.Errors.Where(err => err.ErrorMessage == "Last name must start with capital letter.");

            error.Should().HaveCount(1);
        }

        [Fact]
        public void DoesntReturnError_WhenLastNameMatchesRegex()
        {
            var user = new Register { FirstName = "Stefan"};

            var validator = new RegisterFluentValidator();

            var result = validator.Validate(user);
            var error = result.Errors.Where(err => err.ErrorMessage == "Last name must start with capital letter.");

            error.Should().HaveCount(0);
        }

        [Fact]
        public void DoesntReturnError_WhenEmailIsValid()
        {
            var user = new Register { Email = "bogdanovic.stefan@outlook.com" };
            
            var validator = new RegisterFluentValidator();
            
            var result = validator.Validate(user);
            
            var error = result.Errors.Where(err => err.ErrorMessage == "Invalid email address.");
            
            error.Should().HaveCount(0);
        }

        [Fact]
        public void ReturnsError_WhenEmailIsNotValid()
        {
            var user = new Register { Email = "bogdanovic.outlook.com" };
            
            var validator = new RegisterFluentValidator();
            
            var result = validator.Validate(user);
            
            var error = result.Errors.Where(err => err.ErrorMessage == "Invalid email address.");
            
            error.Should().HaveCount(1);
        }
    }
}