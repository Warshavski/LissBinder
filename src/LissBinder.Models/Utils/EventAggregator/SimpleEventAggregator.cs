using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace Escyug.LissBinder.Models.Utils.EventAggregator
{
    public class SimpleEventAggregator : IEventAggregator
    {
        private IDictionary<Type, IList> _subscriptions;

        public SimpleEventAggregator()
        {
            _subscriptions = new Dictionary<Type, IList>();
        }


        #region IEventAggregator members
        

        public void Publish<TEventMessage>(TEventMessage message)
            where TEventMessage : IEventMessage
        {
            if (message == null)
            {
                throw new ArgumentNullException("event message");
            }

            Type messageType = typeof(TEventMessage);
            if (_subscriptions.ContainsKey(messageType))
            {
                var messageSubscriptions =
                    _subscriptions[messageType].Cast<ISubscription<TEventMessage>>();

                foreach (var subscription in messageSubscriptions)
                {
                    subscription.Action(message);
                }
            }

        }

        public ISubscription<TEventMessage> Subscribe<TEventMessage>(Action<TEventMessage> action) 
            where TEventMessage : IEventMessage
        {
            Type messageType = typeof(TEventMessage);
            var messageSubscription = new Subscription<TEventMessage>(this, action);

            if (_subscriptions.ContainsKey(messageType))
            {
                _subscriptions[messageType].Add(messageSubscription);
            }
            else
            {
                _subscriptions.Add(messageType, 
                    new List<ISubscription<TEventMessage>> { messageSubscription });
            }

            return messageSubscription;
        }

        public void Unsubscribe<TEventMessage>(ISubscription<TEventMessage> subscription) 
            where TEventMessage : IEventMessage
        {
            Type messageType = typeof(TEventMessage);
            if (_subscriptions.ContainsKey(messageType))
            {
                _subscriptions.Remove(messageType);
            }
        }


        #endregion IEventAggregator members
        //---------------------------------------------------------------------

    }
}
