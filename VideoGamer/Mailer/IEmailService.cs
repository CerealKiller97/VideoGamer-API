namespace VideoGamer.Mailer
{
	public interface IEmailService
	{
		string ToEmail { get; set; }
		string Body { get; set; }
		string Subject { get; set; }
		void Send();
	}
}
