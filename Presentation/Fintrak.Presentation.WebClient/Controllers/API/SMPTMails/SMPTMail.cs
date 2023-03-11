//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Web;
//using System.Net.Mail;

//namespace MailingBuyLicense
//{
//    public partial class SMPTMail
//    {
//        public static void button1_Click()
//        {
//            try
//            {
//                MailMessage mail = new MailMessage();
//                SmtpClient SmtpServer = new SmtpClient("smtp.gmail.com");

//                mail.From = new MailAddress("ikennabenedict@gmail.com");
//                mail.To.Add("ejiolawale4@gmail.com");
//                mail.Subject = "Test Mail";
//                mail.Body = "There is a License request from first bank for 3 years.";

//                SmtpServer.Port = 587;
//                SmtpServer.Credentials = new System.Net.NetworkCredential("ikennabenedict@gmail.com", "chinedu1996");
//                SmtpServer.EnableSsl = true;

//                SmtpServer.Send(mail);
//                Console.WriteLine("mail Send");
//            }
//            catch (Exception ex)
//            {
//                Console.WriteLine(ex.ToString());
//            }
//        }
//    }
//}