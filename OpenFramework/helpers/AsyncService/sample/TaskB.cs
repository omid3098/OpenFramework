using System.Collections;
using UnityEngine;
namespace OpenFramework.Helper.AsyncService
{
    public class TaskB : GameTask
    {
        public override event TaskDelegate OnComplete;
        public override event TaskDelegate OnError;

        public override void Execute()
        {
            context.StartCoroutine(WaitSomeSecs(4f));
        }

        private IEnumerator WaitSomeSecs(float duration)
        {
            yield return new WaitForSeconds(duration);
            Debug.Log("TaskB Finished after 4 Seconds");
            if (OnComplete != null) OnComplete(null);
        }
    }
}