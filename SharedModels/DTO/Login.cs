namespace SharedModels.DTO
{
	public class Login
	{
		public string Email { get; set; }
		public string Password { get; set; }
		public int? TimeZoneOffset { get; set; } = null;
	}
}
