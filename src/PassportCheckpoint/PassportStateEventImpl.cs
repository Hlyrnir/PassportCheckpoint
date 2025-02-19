using System;
using System.Threading.Tasks;

namespace PassportCheckpoint
{
    public abstract class PassportStateEventImpl
    {
        public event EventHandler<PassportStateEventArgs>? PassportStateChanged;

        public void NotifyPassportStateChanged(Task<PassportState> tskPassportState)
        {
            if (tskPassportState is null)
                return;

            OnPassportStateChangedEvent(new PassportStateEventArgs { PassportState = tskPassportState });
        }

        private void OnPassportStateChangedEvent(PassportStateEventArgs evntPassportState)
        {
            EventHandler<PassportStateEventArgs>? evntHandler = PassportStateChanged;

            if (evntHandler is not null)
                evntHandler(this, evntPassportState);
        }
    }
}
