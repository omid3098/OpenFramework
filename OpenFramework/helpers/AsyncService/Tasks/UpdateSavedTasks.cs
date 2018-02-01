using System;
using System.Collections.Generic;
using UnityEngine;

namespace Gambeet
{
    public class UpdateSavedTasks : GameTask
    {
        public UpdateSavedTasks(string _data) : base(_data)
        {
        }

        public override event TaskDelegate OnComplete;
        public override event TaskDelegate OnError;
        public override void Execute()
        {
            Debug.Log("____ UpdateOfflineTasks: ____");
            if (_asyncService.unfinishedTasks != null && _asyncService.unfinishedTasks.Count == 0)
            {
                if (OnComplete != null) OnComplete.Invoke("");
                return;
            }
            var task = _asyncService.unfinishedTasks[0];
            GameTask gameTask = ConvertSerializedTaskToGameTask(task);
            gameTask.id = task.id;
            gameTask.Execute();
            gameTask.OnComplete += MoveNext;
            gameTask.OnError += RetryTask;
        }

        private GameTask ConvertSerializedTaskToGameTask(SerializedTask task)
        {
            Type type = Type.GetType(task.type); //target type
            Debug.Log("type: " + type + " - data: " + task.data);
            object instanceObject = Activator.CreateInstance(type, new object[] { task.data }); // an instance of target type
            GameTask gameTask = (GameTask)instanceObject;
            // if (task.type == "UpdateProfileTask")
            // {
            //     return new UpdateProfileTask(task.data);
            // }
            // Debug.LogError("task type is not implemented ty");
            return gameTask;
        }

        private void RetryTask(string data)
        {
            // we dont count for retry because eac task retries itself
            if (OnError != null) OnError.Invoke(data);
        }

        private void MoveNext(string data)
        {
            _asyncService.TaskDone();
            Execute();
        }
    }
}
