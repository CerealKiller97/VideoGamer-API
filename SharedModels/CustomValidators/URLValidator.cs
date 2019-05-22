
using System;
using System.Net;
using System.Threading.Tasks;
using FluentValidation;
namespace SharedModels.CustomValidators
{
	public static class URLValidator
	{
        //public async static Task<IRuleBuilderOptions<T, string>> IsUri<T, string>(this IRuleBuilder<T, string> ruleBuilder, string element)
        //{
        //    return ruleBuilder.CustomAsync((url, context) =>
        //    {
        //        if(!UriTryCreate(url, UriKind.Absolute, out  Uri uri))
        //        {
        //            context.AddFailure("URL is not valid");
        //        } 
        //    });
        //}
    }
}
