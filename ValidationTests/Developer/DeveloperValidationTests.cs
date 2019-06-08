using FluentAssertions;
using SharedModels.Fluent.Developer;
using System.Linq;
using Xunit;

namespace ValidationTests.Developer
{
    public class DeveloperValidationTests
    {
        [Fact]
        public void ReturnsError_WhenNameIsNull()
        {
            var developer = new SharedModels.DTO.CreateDeveloperDTO
            {
                Name = ""
            };
            var validator = new DeveloperFluentValidatior();

            var result = validator.Validate(developer);

            var errors = result.Errors.Where(err => err.ErrorMessage == "Name is required.");

            errors.Should().HaveCount(1);
        }

        [Fact]
        public void DoesntReturnError_WhenNameIsNotNull()
        {
            var developer = new SharedModels.DTO.CreateDeveloperDTO
            {
                Name = "asdasd"
            };
            var validator = new DeveloperFluentValidatior();

            var result = validator.Validate(developer);

            var errors = result.Errors.Where(err => err.ErrorMessage == "Name is required.");

            errors.Should().HaveCount(0);
        }

        [Fact]
        public void ReturnsError_WhenHQIsNotDefined()
        {
            var developer = new SharedModels.DTO.CreateDeveloperDTO
            {
                HQ = ""
            };
            var validator = new DeveloperFluentValidatior();

            var result = validator.Validate(developer);

            var errors = result.Errors.Where(err => err.ErrorMessage == "HQ is required.");

            errors.Should().HaveCount(1);
        }

        [Fact]
        public void DoesntReturnError_WhenHQIsDefined()
        {
            var developer = new SharedModels.DTO.CreateDeveloperDTO
            {
                HQ = "Hasdaadasdasdasdad"
            };
            var validator = new DeveloperFluentValidatior();

            var result = validator.Validate(developer);

            var errors = result.Errors.Where(err => err.ErrorMessage == "HQ is required.");

            errors.Should().HaveCount(0);
        }
    }
}
