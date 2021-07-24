using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Text;
using System.Net.Mail;

namespace NumberGo.Utils
{
    public class MailSender
    {
        SmtpClient _client;
        string _senderAddr;
        string _senderName;
        public MailSender(string host, int port, string senderAddr, string senderName, string userName, string password)
        {
            _client = new SmtpClient(host, port);
            _client.Credentials = new System.Net.NetworkCredential(userName, password);
            _senderAddr = senderAddr;
            _senderName = senderName;
        }

        public async Task SendAsync(string address, string subject, string body)
        {
            MailAddress from = new MailAddress(_senderAddr, _senderName, Encoding.UTF8);
            MailAddress to = new MailAddress(address);

            MailMessage message = new MailMessage(from, to);
            message.Body = body;
            message.BodyEncoding = Encoding.UTF8;
            message.IsBodyHtml = true;
            message.Subject = subject;
            message.SubjectEncoding = Encoding.UTF8;
          
            await _client.SendMailAsync(message);
        }
    }
}
