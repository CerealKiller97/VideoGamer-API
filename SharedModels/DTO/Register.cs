namespace SharedModels.DTO
{
	public class Register : BaseDTO
	{
		public string FirstName { get; set; }
		public string LastName { get; set; }	
		public string Email { get; set; }
		public string Password { get; set; }
	}
}
