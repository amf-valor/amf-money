using AmfValor.AmfMoney.PortalApi.Data;
using System;

namespace AmfValor.AmfMoney.PortalApi.Services
{
    public abstract class Service : IDisposable
    {
        protected readonly AmfMoneyContext _context;

        protected Service(AmfMoneyContext context)
        {
            _context = context;
        }

        #region IDisposable Support
        private bool disposedValue = false; // To detect redundant calls

        protected virtual void Dispose(bool disposing)
        {
            if (!disposedValue)
            {
                if (disposing)
                {
                    _context.Dispose();
                }

                disposedValue = true;
            }
        }
        public void Dispose()
        {
            Dispose(true);
        }
        #endregion
    }
}
