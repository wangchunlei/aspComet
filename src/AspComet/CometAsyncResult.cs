using System;
using System.Collections.Generic;
using System.Threading;
using System.Web;

namespace AspComet
{
    public class CometAsyncResult : IAsyncResult
    {
        private readonly HttpContextBase httpContext;
        private readonly AsyncCallback callback;
        private readonly object asyncState;

        public CometAsyncResult(HttpContextBase httpContext, AsyncCallback callback, object asyncState)
        {
            this.httpContext = httpContext;
            this.callback = callback;
            this.asyncState = asyncState;
        }

        public bool IsCompleted { get; private set; }

        public WaitHandle AsyncWaitHandle
        {
            get { throw new NotSupportedException("Not required for COMET implementation"); }
        }

        public object AsyncState
        {
            get { return asyncState; }
        }

        public bool CompletedSynchronously
        {
            get { return false; }
        }

        public HttpContextBase HttpContext
        {
            get { return this.httpContext; }
        }

        public IEnumerable<Message> ResponseMessages { get; set; }

        public void Complete()
        {
            this.IsCompleted = true;
            this.callback(this);
        }
    }
}