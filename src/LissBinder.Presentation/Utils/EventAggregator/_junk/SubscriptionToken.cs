using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Escyug.LissBinder.Models.Utils.EventBus
{
    /// <summary>
    /// A Token representing a Subscription
    /// </summary>
    public class SubscriptionToken
    {
        internal SubscriptionToken(Type eventItemType)
        {
            _uniqueTokenId = Guid.NewGuid();
            _eventItemType = eventItemType;
        }

        public Guid Token { get { return _uniqueTokenId; } }
        public Type EventItemType { get { return _eventItemType; } }

        private readonly Guid _uniqueTokenId;
        private readonly Type _eventItemType;
    }
}
