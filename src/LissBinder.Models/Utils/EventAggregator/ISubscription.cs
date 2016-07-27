using System;

namespace Escyug.LissBinder.Models.Utils.EventAggregator
{
    public interface ISubscription<TEventMessage> : IDisposable
        where TEventMessage : IEventMessage
    {
        /// <summary>
        /// 
        /// </summary>
        Action<TEventMessage> Action { get; }

        /// <summary>
        /// 
        /// </summary>
        IEventAggregator EventAggregator { get; }
    }
}
