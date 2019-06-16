namespace VideoGamer.Mailer
{
	public class SendGridEmailService : IEmailService
	{
		public string ToEmail { get; set ; }
		public string Body { get; set; }
		public string Subject { get; set; }

		public void Send()
		{
			// TODO: Implement SendGrid
		}
	}
}
