using System.Collections;
using UnityEngine;
/// <summary>
/// how to use:
/// var task = new SampleTask();
/// task.OnComplete += (data)=>{ Debug.Log("task finished successfully" + data)}
/// task.OnError += (error_message)=>{ Debug.Log("error!"+ error_message)}
/// task.Execute();
/// </summary>
namespace OpenFramework.Helper.AsyncService
{
    public class SampleTask : Task
    {
        public override event TaskDelegate OnComplete;
        public override event TaskDelegate OnError;
        public int maxTryCount = 5;
        public override void Execute()
        {
            Debug.Log("task executed waiting for completion");
            context.StartCoroutine(WaitCoroutine());
        }

        private IEnumerator WaitCoroutine()
        {
            yield return new WaitForSeconds(3f);
            if (OnComplete != null) OnComplete.Invoke("data");
            context.StartCoroutine(SampleErrorCoroutine());
        }

        private IEnumerator SampleErrorCoroutine()
        {
            yield return new WaitForSeconds(2f);
            if (OnError != null) OnError.Invoke("error message");
        }
    }
}