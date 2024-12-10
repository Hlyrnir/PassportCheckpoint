namespace PassportCheckpoint
{
    public sealed class PassportStateEventArgs : EventArgs
    {
        public required Task<PassportState> PassportState { get; init; }
    }
}