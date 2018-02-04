using System;
using UnityEngine.Assertions;

namespace OpenFramework.Helper.AsyncService
{
    public abstract class GameTask
    {
        public delegate void TaskDelegate(string data);
        public bool running;
        public string data { get; protected set; }
        public abstract event TaskDelegate OnComplete;
        public abstract event TaskDelegate OnError;
        public string id;

        public static GameContext context;
        protected AsyncService _asyncService;
        public GameTask(string _data = null)
        {
            Assert.IsNotNull(context, "Context is not set. make sure AsyncService is registered in your main context!");
            data = _data;
            id = Guid.NewGuid().ToString();
            _asyncService = (AsyncService)context.GetService<IAsyncService>();
        }
        public abstract void Execute();
        public void Schedule()
        {
            _asyncService.Schedule(this);
        }
        
        public string GetData()
        {
            return data;
        }
    }
}