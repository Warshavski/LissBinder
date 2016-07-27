using System;

namespace Escyug.LissBinder.Models.Utils.EventAggregator
{
    public interface IEventAggregator
    {
        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="TEventMessage"></typeparam>
        /// <param name="message"></param>
        void Publish<TEventMessage>(TEventMessage message)
            where TEventMessage : IEventMessage;


        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="TEventMessage"></typeparam>
        /// <param name="action"></param>
        /// <returns></returns>
        ISubscription<TEventMessage> Subscribe<TEventMessage>(Action<TEventMessage> action)
            where TEventMessage : IEventMessage;


        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="TEventMessage"></typeparam>
        /// <param name="subscription"></param>
        void Unsubscribe<TEventMessage>(ISubscription<TEventMessage> subscription)
           where TEventMessage : IEventMessage;
    }
}
