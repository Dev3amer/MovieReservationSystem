using MovieReservationSystem.Data.Entities;

namespace MovieReservationSystem.Service.Abstracts
{
    public interface IPaymentService
    {
        Task<Reservation?> CreateOrUpdatePaymentIntent(int reservationId);
        Task UpdatePaymentIntentToSucceededOrFailed(string paymentIntentId, bool isSucceeded);
    }
}
