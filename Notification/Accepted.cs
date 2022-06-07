namespace Notification
{
    [Flags]
    public enum Accepted
    {
        Rejected = 0b001,
        Kitchen = 0b010,
        Booking = 0b100,
        Confirmed = Kitchen | Booking,
        BookingRejected = Kitchen | Rejected,
        KitchenRejected = Rejected | Booking
    }
}
