<<<<<<< HEAD
﻿using FluentValidation;

namespace SharedModels.Fluent.Platform
{
	public class PlatformFluentValidator : AbstractValidator<DTO.Platform>
	{
		public PlatformFluentValidator()
		{
			RuleFor(p => p.PlatformName)
=======
﻿using Domain;
using FluentValidation;

namespace SharedModels.Fluent
{
	public class PlatformFluentValidator : AbstractValidator<Platform>
	{
		public PlatformFluentValidator()
		{
			RuleFor(p => p.Name)
>>>>>>> b00eedcc014bee4f52c7e012c5e5369fe8f1f4a9
				.NotEmpty()
				.IsInEnum();
		}
	}
}
