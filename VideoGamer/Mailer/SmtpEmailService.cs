using System.Net;
using System.Net.Mail;

namespace VideoGamer.Mailer
{
	public class SmtpEmailService : IEmailService
	{
		private readonly string _host;
		private readonly int _port;
		private readonly string _from;
		private readonly string _password;

		public SmtpEmailService(string host, int port, string from, string password)
		{
			_host = host;
			_port = port;
			_from = from;
			_password = password;
		}

		public string ToEmail { get; set; }
		public string Body { get; set; }
		public string Subject { get; set; }

		public void Send()
		{
			var smtp = new SmtpClient
			{
				Host = _host,
				Port = _port,
				EnableSsl = true,
				DeliveryMethod = SmtpDeliveryMethod.Network,
				UseDefaultCredentials = false,
				Credentials = new NetworkCredential(_from, _password)
			};

			using (var message = new MailMessage(_from, ToEmail) { Subject = Subject, Body = Body})
			{
				smtp.Send(message);
			}
		}

	}
}
