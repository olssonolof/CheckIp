using System;
using System.Collections.Generic;
using System.Text;
using System.Net.Mail;
using System.Threading;

namespace CheckIp
{
    class Mail
    {


        public static string SendMail(string eMail, string ipNumber, string mailLogin, string password, string mailSubject, string mailBody)
        {

            MailMessage mail = new MailMessage("UbuntuServer@test.com", eMail);
            SmtpClient client = new SmtpClient("smtp.gmail.com");
            client.Port = 587;
            client.DeliveryMethod = SmtpDeliveryMethod.Network;
            client.UseDefaultCredentials = false;
            client.EnableSsl = true;

            client.Credentials = new System.Net.NetworkCredential(mailLogin, password);
            mail.Subject = mailSubject;
            mail.BodyEncoding = UTF8Encoding.UTF8;
            mail.Body = $"{mailBody}\n{ipNumber}";

            bool mailSent = false;

            do
            {
                try
                {
                    client.Send(mail);
                    mailSent = true;
                }
                catch (Exception e) 
                {
                    Console.WriteLine($"Something went wrong when trying to send the mail. Trying again in five minutes. Errormessage: \n{e.Message}");
                    Thread.Sleep(300000);
                }
            } while (!mailSent);
            return "Mail sent";
        }

    }
}
