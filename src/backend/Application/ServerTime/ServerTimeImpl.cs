
using System;

namespace backend.Application.ServerTime
{
    internal sealed class ServerTimeImpl : IServerTime
    {
        public DateTime Now => DateTime.Now;
    }
}