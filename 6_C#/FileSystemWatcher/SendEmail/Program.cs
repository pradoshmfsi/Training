using System;
using System.Configuration;
using System.Net;
using System.Net.Mail;

class Program
{
    static void Main(string[] args)
    {
        MailMessage mailMessage = new MailMessage();
        mailMessage.From = new MailAddress(GetValue("smtpFrom"));
        mailMessage.To.Add(GetValue("smtpTo"));
        mailMessage.Subject = "Subject";
        mailMessage.IsBodyHtml = true;
        mailMessage.Body = "<h1 style='color:red;background-color:black'>Great offer</h1>";
        //mailMessage.Body = "This is test email";

        Attachment attachment = new Attachment(GetValue("smtpAttachment"));
        mailMessage.Attachments.Add(attachment);


        SmtpClient smtpClient = new SmtpClient();
        smtpClient.Host = GetValue("smtpHost");
        smtpClient.Port = Int32.Parse(GetValue("smtpPort"));
        smtpClient.UseDefaultCredentials = false;
        smtpClient.Credentials = new NetworkCredential(GetValue("smtpUsername"), GetValue("smtpPassword"));
        smtpClient.EnableSsl = true;

        try
        {
            smtpClient.Send(mailMessage);
            Console.WriteLine("Email Sent Successfully.");
            Console.ReadLine();
        }
        catch (Exception ex)
        {
            Console.WriteLine("Error: " + ex.Message);
            Console.ReadLine();
        }
    }
    public static string GetValue(string field)
    {
        return @ConfigurationManager.AppSettings.Get(field);
    }
}