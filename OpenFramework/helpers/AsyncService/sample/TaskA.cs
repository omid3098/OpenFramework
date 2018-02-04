using System;
using System.Collections;
using UnityEngine;
namespace OpenFramework.Helper.AsyncService
{
    public class TaskA : GameTask
    {
        public override event TaskDelegate OnComplete;
        public override event TaskDelegate OnError;

        public override void Execute()
        {
            context.StartCoroutine(WaitSomeSecs(3f));
        }

        private IEnumerator WaitSomeSecs(float duration)
        {
            yield return new WaitForSeconds(duration);
            Debug.Log("TaskA Finished after 3 Seconds");
            if (OnComplete != null) OnComplete(null);
        }
    }
}