using System;

namespace Escyug.LissBinder.Presentation.Utils.EventAggregator
{
    public class Subscription<TEventMessage> : ISubscription<TEventMessage>
        where TEventMessage : IEventMessage
    {

        #region ISubscription members


        public Action<TEventMessage> Action { get; private set; }

        public IEventAggregator EventAggregator { get; private set; }

        
        #endregion ISubscription members
        //---------------------------------------------------------------------


        public Subscription(IEventAggregator eventAggregator, Action<TEventMessage> action)
        {
            if (eventAggregator == null)
            {
                throw new ArgumentNullException("eventAggregator");
            }

            if (action == null)
            {
                throw new ArgumentNullException("action");
            }

            EventAggregator = eventAggregator;
            Action = action;
        }


        #region IDisposable members

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        protected virtual void Dispose(bool disposing)
        {
            if (disposing)
            {
                EventAggregator.Unsubscribe(this);
            }
        }

        #endregion IDisposable members
        //---------------------------------------------------------------------
    }
}
