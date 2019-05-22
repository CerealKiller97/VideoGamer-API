using System.Linq;
using FluentValidation.Results;

namespace SharedModels.Formatters
{
	public static class ValidationFormatter
	{ 
		public static object Format(this ValidationResult result) => result.Errors.Select(e => new {Name = e.PropertyName, Errors = e.ErrorMessage});
	}
}
