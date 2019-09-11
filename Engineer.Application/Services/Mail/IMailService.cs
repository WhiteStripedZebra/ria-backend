using System.Threading.Tasks;
using Engineer.Domain.Entities;

namespace Engineer.Application.Services.Mail
{
    public interface IMailService
    {
        Task SendNewOrderMailAsync(string to, Order order);
        Task SendMailAsync(string to, string subject, string message, string displayName = "Rådet for Ingeniørstuderende Aarhus");
    }
}