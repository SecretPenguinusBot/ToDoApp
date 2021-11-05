
using System;

namespace backend.Application.ServerTime
{
    public interface IServerTime
    {
        public DateTime Now { get; }
    }
}