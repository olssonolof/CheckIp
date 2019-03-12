using System;
using System.Collections.Generic;
using System.Text;
using System.Net.Mail;

namespace CheckIp
{
    class Mail
    {


        public static string SendMail(string eMail, string ipNumber, string mailLogin, string password)
        {

            MailMessage mail = new MailMessage("UbuntuServer@test.com", eMail);
            SmtpClient client = new SmtpClient("smtp.gmail.com");
            client.Port = 587;
            client.DeliveryMethod = SmtpDeliveryMethod.Network;
            client.UseDefaultCredentials = false;
            client.EnableSsl = true;
            
            client.Credentials = new System.Net.NetworkCredential(mailLogin, password);
            mail.Subject = "You have a new external IP adress.";
            mail.BodyEncoding = UTF8Encoding.UTF8;
            mail.Body =ipNumber;
            client.Send(mail);
            return "Mail sent";
        }

    }
}
