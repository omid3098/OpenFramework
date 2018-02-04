namespace OpenFramework.Helper.AsyncService
{
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using OpenFramework;
    using OpenFramework.Helper.SaveLoadService;
    using UnityEngine;
    using UnityEngine.Assertions;

    public class AsyncService : IAsyncService
    {
        public delegate void OnAsyncServiceReadyDelegate();
        public event OnAsyncServiceReadyDelegate OnReady;
        public GameContext context { get; set; }
        private SaveLoadService _saveLoadService;
        public List<SerializedTask> unfinishedTasks { get; private set; }
        public List<Task> taskPool { get; private set; }
        public bool ready { get; set; }
        private Task currentExecutingTask;

        public IEnumerator Init()
        {
            _saveLoadService = (SaveLoadService)context.GetService<ISaveLoadService>();
            Assert.IsNotNull(_saveLoadService, "SaveLoadService is not registered. if you want to save offline tasks, register that.");
            Task.context = context;
            unfinishedTasks = new List<SerializedTask>();
            taskPool = new List<Task>();
            Debug.Log("Async service initialized");
            ready = true;
            yield return null;
        }

        /// <summary>
        /// call this method when your game is ready to update unfinished tasks
        /// </summary>
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

        public void SaveTask(Task task)
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
        public void SavedTaskDone()
        {
            var _task = unfinishedTasks[0];
            unfinishedTasks.Remove(_task);
        }
        public void Schedule(Task task)
        {
            Assert.IsTrue(ready);
            // if (task.OnComplete)
            taskPool.Add(task);
            if (currentExecutingTask == null) ExecuteFirstTask();
        }

        private void RemoveTaskFromPool(string data)
        {
            taskPool.Remove(currentExecutingTask);
            currentExecutingTask = null;
            ExecuteFirstTask();
        }

        private void ExecuteFirstTask()
        {
            if (taskPool.Count == 0) return;
            currentExecutingTask = taskPool[0];
            currentExecutingTask.OnComplete += RemoveTaskFromPool;
            currentExecutingTask.Execute();
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
