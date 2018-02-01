namespace OpenFramework.Helper
{
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using OpenFramework;
    using UnityEngine;
    using UnityEngine.Assertions;

    public class AsyncService : IAsyncService
    {
        public delegate void OnAsyncServiceReadyDelegate();
        public event OnAsyncServiceReadyDelegate OnReady;
        public GameContext context { get; set; }
        private SaveLoadService _saveLoadService;
        public List<SerializedTask> unfinishedTasks { get; private set; }
        public bool ready { get; set; }

        public IEnumerator Init()
        {
            _saveLoadService = (SaveLoadService)context.GetService<ISaveLoadService>();
            Assert.IsNotNull(_saveLoadService, "Service is not registered");
            GameTask.context = context;
            unfinishedTasks = new List<SerializedTask>();
            Debug.Log("Async service initialized");
            ready = true;
            yield return null;
        }

        public void UpdateUnfinishedTasks()
        {
            unfinishedTasks = _saveLoadService.LoadAllTasks();
            if (unfinishedTasks != null && unfinishedTasks.Count != 0)
            {
                var _task = unfinishedTasks[0];
                var updateOfflineTasks = new UpdateSavedTasks(null);
                updateOfflineTasks.Execute();
                updateOfflineTasks.OnComplete += Ready;
                updateOfflineTasks.OnError += Ready;
            }
            else
            {
                Ready(null);
            }
        }

        private void Ready(string data)
        {
            if (OnReady != null) OnReady.Invoke();
        }

        public void SaveTask(GameTask task)
        {
            Debug.Log("Saving Task");
            if (unfinishedTasks == null) unfinishedTasks = new List<SerializedTask>();
            unfinishedTasks.Add(new SerializedTask()
            {
                id = task.id,
                type = task.GetType().FullName,
                data = task.GetData(),
            });
            _saveLoadService.SaveAllTasks(unfinishedTasks);
        }
        public void TaskDone()
        {
            var _task = unfinishedTasks[0];
            unfinishedTasks.Remove(_task);
        }
        public void StopService()
        {
            throw new NotImplementedException();
        }
        public void StartService()
        {
        }
    }
}

/*
CheckToken(
    OnValid     +=  GetProfile(
                        OnComplete  +=  GetData()
                        OnError     +=  GetProfile()
                    )   
    OnNoToken   +=  Register(
                        OnComplete  +=  InitializeProfile(
                                        Oncomplete += 
                                    )
                        OnError     +=  ErroFaildToLoadGame()
                    )
    OnExpired   +=  GetNewToken()
)

*/
