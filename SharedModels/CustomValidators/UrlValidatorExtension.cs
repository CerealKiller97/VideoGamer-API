using System.Collections.Generic;
using System.Net;
using System.Threading;
using System.Threading.Tasks;
using FluentValidation;
using FluentValidation.Results;

namespace SharedModels.CustomValidators
{
	public static class UrlValidatorExtension
	{
		public static IRuleBuilderOptions<string, string> IsUri(this IRuleBuilder<string, string> ruleBuilder,
			string element)
		{
			return ruleBuilder.SetValidator(new UriValidator("Url is not valid"));
		}
	}
}