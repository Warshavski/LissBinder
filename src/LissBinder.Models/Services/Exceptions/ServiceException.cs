using System;

namespace Escyug.LissBinder.Models.Services.Exceptions
{
    /// <summary>
    /// Exception thrown when http service exceptions has occurs
    /// </summary>
    [Serializable]
    public class ServiceException : Exception
    {
        public ServiceException(string message)
            : base(message)
        {
        }
    }
}
