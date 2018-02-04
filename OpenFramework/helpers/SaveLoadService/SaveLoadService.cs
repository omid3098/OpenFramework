namespace OpenFramework.Helper
{
    using System.Collections;
    using System.Collections.Generic;
    using UnityEngine;
    using OpenFramework;
    using System;
    using UnityEngine.Assertions;
    using OpenFramework.Helper.AsyncService;

    public class SaveLoadService : ISaveLoadService
    {
        private const string TaskKey = "ASYNCSERVICE_TASKS";

        public GameContext context { get; set; }

        public bool ready { get; set; }

        // ======================================================================== \\

        public IEnumerator Init()
        {
            ready = true;
            yield return null;
        }

        [Serializable]
        public class ListHolder<T>
        {
            [SerializeField]
            public List<T> list;
        }

        public void SaveAllTasks(List<SerializedTask> tasks)
        {
            ListHolder<SerializedTask> holder = new ListHolder<SerializedTask>();
            holder.list = tasks;
            var data = JsonUtility.ToJson(holder);
            Debug.Log(data);
            PlayerPrefs.SetString(TaskKey, data);
        }

        public List<SerializedTask> LoadAllTasks()
        {
            ListHolder<SerializedTask> holder = new ListHolder<SerializedTask>();
            var data = PlayerPrefs.GetString(TaskKey, "");
            if (string.IsNullOrEmpty(data))
            {
                return holder.list;
            }
            else
            {
                holder = JsonUtility.FromJson<ListHolder<SerializedTask>>(data);
                Debug.Log(holder.list);
                return holder.list;
            }
        }

        public void StartService()
        {
        }

        public void StopService()
        {
            throw new NotImplementedException();
        }
    }

}