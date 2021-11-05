

namespace backend.Application
{
    using System;
    using ServerTime;
    public sealed class ApplicationContext
    {
        private readonly IServerTime _time;
        public ApplicationContext(IServerTime serverTime)
        {
            _time = serverTime;
        }
        
        private DateTime _started;
        public DateTime Started => _started;
        public DateTime Now => _time.Now;
        public TimeSpan Uptime => Now - Started;
        public string ApplicationName => "ToDoWebApi";
        public string Version => "v0.0.0.1";
        public string Description => "Web api for to-do application";
        public void Start() => _started = _time.Now;

    }
}