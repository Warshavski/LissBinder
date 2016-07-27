using Escyug.LissBinder.Models;
using Escyug.LissBinder.Presentation.Utils.EventAggregator;

namespace Escyug.LissBinder.Presentation.Messages
{
    internal class BindingMessage : IEventMessage
    {
        public Binding Binding { get; private set; }

        public BindingMessage(Binding binding)
        {
            Binding = binding;
        }
    }
}
