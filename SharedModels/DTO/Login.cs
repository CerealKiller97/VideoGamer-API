namespace SharedModels.DTO
{
	public class Login : BaseDTO
	{
		public string Email { get; set; }
		public string Password { get; set; }
		public int? TimeZoneOffset { get; set; } = null;
	}
}
