namespace PassportCheckpoint.Interface
{
    public interface IPassportStateProvider
    {
        public event EventHandler<PassportStateEventArgs> PassportStateChanged;

        void NotifyPassportStateChanged(Task<PassportState> tskPassportState);
        Task<bool> LogInAsync(IPassportCredential bwpCredential, CancellationToken tknCancellation);
        Task<bool> LogOutAsync(CancellationToken tknCancellation);

        Task RefreshAsync(CancellationToken tknCancellation);
    }
}
