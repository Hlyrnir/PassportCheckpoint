namespace PassportCheckpoint.Interface
{
    public interface IPassport
    {
        string EmailAddress { get; init; }
        DateTimeOffset ExpiredAt { get; init; }
        Guid HolderId { get; init; }
        bool IsAuthority { get; init; }
        bool IsEnabled { get; init; }
        IEnumerable<IPassportVisa> Visa { get; init; }
    }
}
