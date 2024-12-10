namespace PassportCheckpoint.Interface
{
    public interface IPassportCredential
    {
        string Credential { get; set; }
        string Signature { get; set; }
    }
}
