using PassportCheckpoint.Interface;

namespace PassportCheckpoint
{
    public sealed class PassportState
    {
        private readonly IPassport? ppPassport;

        public event EventHandler<EventArgs>? StateChanged;

        public void NotifyStateChanged()
        {
            OnStateChangedEvent(new EventArgs());
        }

        private void OnStateChangedEvent(EventArgs argEvent)
        {
            EventHandler<EventArgs>? evntHandler = StateChanged;

            if (evntHandler is not null)
                evntHandler(this, argEvent);
        }

        private PassportState(IPassport? ppPassport = null)
        {
            this.ppPassport = ppPassport;
        }

        public bool IsAuthenticated
        {
            get
            {
                if (ppPassport is null)
                    return false;

                return true;
            }
        }

        public bool IsAuthority
        {
            get
            {
                if (ppPassport is null)
                    return false;

                return ppPassport.IsAuthority;
            }
        }

        public Guid PassportHolderId
        {
            get
            {
                if (ppPassport is null)
                    return Guid.Empty;

                return ppPassport.HolderId;
            }
        }

        public bool RequiredVisaExists(IPassportVisa ppRequiredPassportVisa)
        {
            if (ppPassport is null)
                return false;

            foreach (IPassportVisa ppVisa in ppPassport.Visa)
            {
                if (ppVisa.Name == ppRequiredPassportVisa.Name && ppVisa.Level == ppRequiredPassportVisa.Level)
                    return true;
            }

            return false;
        }

        public static PassportState AsAnonymous()
        {
            return new PassportState();
        }

        public static PassportState Initialize(IPassport ppPassport)
        {
            if (ppPassport.IsEnabled == false)
                return new PassportState();

            return new PassportState(ppPassport);
        }
    }
}
