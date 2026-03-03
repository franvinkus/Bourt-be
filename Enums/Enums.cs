namespace Bourt.Enums
{
    public enum UserRole
    {
        Admin,
        Owner, 
        Customer,
    }

    public enum BookingStatus
    {
        RequestCancel,
        Cancelled,
        Pending,
        Verifying,
        Confirmed,
        Completed,
    }
}
