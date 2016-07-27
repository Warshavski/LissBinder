using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Escyug.LissBinder.Models.Utils.EventBus
{
    public interface ISubscription
    {
        /// <summary>
        /// Token returned to the subscriber
        /// </summary>
        SubscriptionToken SubscriptionToken { get; }

        /// <summary>
        /// Publish to the subscriber
        /// </summary>
        /// <param name="eventBase"></param>
        void Publish(EventBase eventBase);
    }
}
