using System;
using System.Runtime.Serialization;

namespace AmfValor.AmfMoney.PortalApi.Services
{
    [Serializable]
    internal class TradingBookNotFoundException : ApplicationException
    {
        public TradingBookNotFoundException()
        {
        }

        public TradingBookNotFoundException(string message) : base(message)
        {
        }

        public TradingBookNotFoundException(string message, Exception innerException) : base(message, innerException)
        {
        }

        protected TradingBookNotFoundException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }
}