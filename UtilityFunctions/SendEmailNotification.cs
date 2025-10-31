using System;
using System.Net;
using System.Net.Mail;

namespace SF_Automation.UtilityFunctions
{
    public class SendEmailNotification
    {
        //To send email notification using gmail     
        public void SendEmailWithTestExecutionReport()
        {
            //Setting up Gmail SMTP
            MailMessage mail = new MailMessage();
            SmtpClient client = new SmtpClient("smtp.gmail.com");
            mail.From = new MailAddress("sfstandard1.user@gmail.com");
            mail.To.Add("SGoyal0427@hl.com");
            mail.CC.Add("sonika.goyal@Technossus.com");
            mail.Subject = "Test Execution Report";
            mail.Body = "Please find the attached reports.";
            mail.Attachments.Add(new Attachment(@"C:\Users\YRamann\source\repos\SF_Automation\Reports\index.html"));
            client.Port = 443;
            client.UseDefaultCredentials = false;
            client.Credentials = new System.Net.NetworkCredential("sfstandard1.user", "testing@123");
            client.EnableSsl = true;

            try
            {
                client.Send(mail);
            }
            catch (System.Exception e)
            {
                Console.WriteLine(e.Message);
            }
        }

        //To outlook send email notification
        public void SendOutlookEmailWithTestExecutionReport(string Subject)
        {
            try
            {
                MailMessage mail = new MailMessage();
                SmtpClient client = new SmtpClient("mail.HL.com");
                mail.From = new MailAddress("SGoyal0427@HL.com");
                //mail.To.Add(new MailAddress("GMcCoy@HL.com"));
                //mail.To.Add(new MailAddress("DChavez@HL.com"));
                //mail.To.Add(new MailAddress("TTurnbull@HL.com"));
                mail.To.Add(new MailAddress("SGoyal0427@HL.com"));                ;
                //mail.CC.Add("SGoyal0427@HL.com");
                mail.Subject = Subject;
                mail.Body = "Please find the attached reports.";
                mail.Attachments.Add(new Attachment(@"C:\Users\YRamann\source\repos\SF_Automation\Reports\index.html"));
                client.Port = 25;
                client.EnableSsl = true;
                client.UseDefaultCredentials = false;
                client.Credentials = new NetworkCredential("SGoyal0427", "Angeles!2021");
                client.Send(mail);
            }
            catch (System.Exception e)
            {
                Console.WriteLine(e.Message);
            }
        }
    }
}


