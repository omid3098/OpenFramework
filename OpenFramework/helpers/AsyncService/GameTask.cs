using System;
using OpenFramework;
using UnityEngine.Assertions;

namespace OpenFramework.Helper
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
            Assert.IsNotNull(context);
            data = _data;
            id = Guid.NewGuid().ToString();
            _asyncService = (AsyncService)context.GetService<IAsyncService>();
        }
        public abstract void Execute();

        public string GetData()
        {
            return data;
        }
    }
}