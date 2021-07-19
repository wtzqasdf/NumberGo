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
        public MailSender(string host, int port, string senderAddr, string senderName)
        {
            _client = new SmtpClient(host, port);
            _senderAddr = senderAddr;
            _senderName = senderName;
        }

        public void Send(string address, string name, string subject, string body)
        {
            MailAddress from = new MailAddress(_senderAddr, _senderName);
            MailAddress to = new MailAddress(address, name, Encoding.UTF8);

            MailMessage message = new MailMessage(from, to);
            message.Body = body;
            message.BodyEncoding = Encoding.UTF8;
            message.Subject = subject;
            message.SubjectEncoding = Encoding.UTF8;

            _client.Send(message);
        }
    }
}
