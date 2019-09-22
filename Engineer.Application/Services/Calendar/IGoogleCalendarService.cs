using Engineer.Domain.Entities;
using Engineer.Domain.Models.Loans;

namespace Engineer.Application.Services.Calendar
{
    public interface IGoogleCalendarService
    {
        void CreateLoanOrder(OrderDTO order);
    }
}