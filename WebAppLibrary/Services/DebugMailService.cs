using System;
using System.Diagnostics;

namespace WebAppLibrary.Services
{
    public class DebugMailService : IMailService
    {
        public bool SendMail(string to, string from, string subject, string body)
        {
            //Emulates true email response by now :)
            Debug.WriteLine($"Sending email: To: {to}, Subject: {subject}");
            return true;
        }
    }
}
