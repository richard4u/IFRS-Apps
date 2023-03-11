//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Net.Mail;
//using System.Web.UI;
//using System.Web;
//using Limilabs.Mail.Appointments;

//namespace Fintrak.Presentation.WebClient.Controllers.API.SMPTMails
//{
//    public class MailApiController
//    {
//        public void ProcessRequest(HttpContext context)
//        {
//            var RegisteredUsers = new List<Mail>();
//            string jsonString = String.Empty;
//            HttpContext.Current.Request.InputStream.Position = 0;
//            using (System.IO.StreamReader inputStream =
//            new System.IO.StreamReader(HttpContext.Current.Request.InputStream))
//            {
//                jsonString = inputStream.ReadToEnd();
//                System.Web.Script.Serialization.JavaScriptSerializer jSerialize =
//                    new System.Web.Script.Serialization.JavaScriptSerializer();
//                var email = jSerialize.Deserialize<Mail>(jsonString);

//                if (email != null)
//                {
//                    string from = email.StartDate;
//                    string to = email.Year;
//                    string body = email.Errormessage;
//                    string comments = email.Comment;
//                    //try
//                    //{
//                    //    MailMessage mail = new MailMessage();
//                    //    SmtpClient SmtpServer = new SmtpClient("smtp.gmail.com");

//                    //    mail.From = new MailAddress("ikennabenedict@gmail.com");
//                    //    mail.To.Add("ejiolawale4@gmail.com");
//                    //    mail.Subject = "Test Mail";
//                    //    mail.Body = "There is a License request from first bank for 3 years.";

//                    //    SmtpServer.Port = 587;
//                    //    SmtpServer.Credentials = new System.Net.NetworkCredential("ikennabenedict@gmail.com", "chinedu1996");
//                    //    SmtpServer.EnableSsl = true;

//                    //    SmtpServer.Send(mail);
//                    //    Console.WriteLine("mail Send");
//                    //}
//                    //catch (Exception ex)
//                    //{
//                    //    Console.WriteLine(ex.ToString());
//                    //}
//                    //You can write here the code to send Email, 
//                    //see ,the Class System.Net.Mail.MailMessage on MSDN
//                    //Once the Mail is sent succefully, you can send back 
//                    //a response to the Client informing him that everything is okay !
//                    context.Response.Write(jSerialize.Serialize(
//                         new
//                         {
//                             Response = "Message Has been sent successfully"
//                         }));
//                }
//            }
//        }
//    }
//}