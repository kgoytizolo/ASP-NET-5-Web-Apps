
namespace WebAppLibrary.Services
{
    public interface IMailService
    {
        //To implement email service contract - client side calls the service
        bool SendMail(string to, string from, string subject, string body);
    }
}
