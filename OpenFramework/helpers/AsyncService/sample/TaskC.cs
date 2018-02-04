using System.Collections;
using UnityEngine;
namespace OpenFramework.Helper.AsyncService
{
    public class TaskC : Task
    {
        public override event TaskDelegate OnComplete;
        public override event TaskDelegate OnError;

        public override void Execute()
        {
            context.StartCoroutine(WaitSomeSecs(1f));
        }

        private IEnumerator WaitSomeSecs(float duration)
        {
            yield return new WaitForSeconds(duration);
            Debug.Log("TaskC Finished after 1 Seconds");
            if (OnComplete != null) OnComplete(null);
        }
    }
}