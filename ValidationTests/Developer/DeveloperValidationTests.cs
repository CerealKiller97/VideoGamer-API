using System;
using System.Collections.Generic;
using FluentAssertions;
using SharedModels.Fluent.Developer;
using System.Linq;
using SharedModels.DTO;
using Xunit;
using EntityConfiguration;

namespace ValidationTests.Developer
{
    public class DeveloperValidationTests
    {
//        private readonly VideoGamerDbContext _Context;
//
//        [Fact]
//         public void ReturnsError_WhenNameIsNull()
//         {
//             var developer = new SharedModels.DTO.CreateDeveloperDTO
//             {
//                 Name = ""
//             };
//             var validator = new DeveloperFluentValidatior();
//
//             var result = validator.Validate(developer);
//
//             var errors = result.Errors.Where(err => err.ErrorMessage == "Name is required.");
//
//             errors.Should().HaveCount(1);
//         }
//
//         [Fact]
//         public void DoesntReturnError_WhenNameIsNotNull()
//         {
//             var developer = new SharedModels.DTO.CreateDeveloperDTO
//             {
//                 Name = "asdasd"
//             };
//             var validator = new DeveloperFluentValidatior();
//
//             var result = validator.Validate(developer);
//
//             var errors = result.Errors.Where(err => err.ErrorMessage == "Name is required.");
//
//             errors.Should().HaveCount(0);
//         }
//
//         [Fact]
//         public void ReturnsError_WhenHQIsNotDefined()
//         {
//             var developer = new SharedModels.DTO.CreateDeveloperDTO
//             {
//                 HQ = ""
//             };
//             var validator = new DeveloperFluentValidatior();
//
//             var result = validator.Validate(developer);
//
//             var errors = result.Errors.Where(err => err.ErrorMessage == "HQ is required.");
//
//             errors.Should().HaveCount(1);
//         }
//
//         [Fact]
//         public void DoesntReturnError_WhenHQIsDefined()
//         {
//             var developer = new SharedModels.DTO.CreateDeveloperDTO
//             {
//                 HQ = "Hasdaadasdasdasdad"
//             };
//             var validator = new DeveloperFluentValidatior();
//
//             var result = validator.Validate(developer);
//
//             var errors = result.Errors.Where(err => err.ErrorMessage == "HQ is required.");
//
//             errors.Should().HaveCount(0);
//         }
//
//         [Fact]
//         public void ReturnsAnError_WhenFoundedIsNull()
//         {
//             var developer = new CreateDeveloperDTO
//             {
//             };
//            
//             var validator = new DeveloperFluentValidatior();
//
//             var results = validator.Validate(developer);
//
//             var error = results.Errors.Where(err => err.ErrorMessage == "Foundation date is required.");
//
//             error.Should().HaveCount(1);
//         }
//        
//         [Fact]
//         public void DoesntReturnAnError_WhenFoundedIsNotNull()
//         {
//             var developer = new CreateDeveloperDTO
//             {
//                 Founded = new DateTime(2019,12,8)
//             };
//            
//             var validator = new DeveloperFluentValidatior();
//
//             var results = validator.Validate(developer);
//
//             var error = results.Errors.Where(err => err.ErrorMessage == "Foundation date is required.");
//
//             error.Should().HaveCount(0);
//         }
//
//         [Fact]
//         public void ReturnsAnError_WhenWebsiteIsNull()
//         {
//             var developer = new CreateDeveloperDTO
//             {
//                 Website = ""
//             };
//            
//             var validator = new DeveloperFluentValidatior();
//             var result = validator.Validate(developer);
//
//             var error = result.Errors.Where(err => err.ErrorMessage == "Website URL is required.");
//
//             error.Should().HaveCount(1);
//         }
//
//         [Fact]
//         public void DoesntReturnAnError_WhenWebsiteIsNull()
//         {
//             var developer = new CreateDeveloperDTO
//             {
//                 Website = "http://www.site.com"
//             };
//            
//             var validator = new DeveloperFluentValidatior();
//             var result = validator.Validate(developer);
//
//             var error = result.Errors.Where(err => err.ErrorMessage == "Website URL is required.");
//
//             error.Should().HaveCount(0);
//         }
    }
}
