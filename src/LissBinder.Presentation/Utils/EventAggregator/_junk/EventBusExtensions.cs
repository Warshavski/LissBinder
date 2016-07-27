using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Escyug.LissBinder.Models.Utils.EventBus
{
    public static class EventBusExtensions
    {
        public static void Publish(this EventBase eventBase, IEventBus eventBus)
        {
            eventBus.Publish(eventBase);
        }

        public static void PublishAsync(this EventBase eventBase, IEventBus eventBus)
        {
            eventBus.PublishAsync(eventBase);
        }

        public static void PublishAsync(this EventBase eventBase, IEventBus eventBus, AsyncCallback asyncCallback)
        {
            eventBus.PublishAsync(eventBase, asyncCallback);
        }

        public static void Unsubscribe(this SubscriptionToken token, IEventBus eventBus)
        {
            eventBus.Unsubscribe(token);
        }
    }
}
