using System.Collections.Generic;
using UnityEngine;
namespace OpenFramework
{
    public class InjectBehaviour : MonoBehaviour
    {
        private static GameContext context;
        private bool injected = false;
        private void Awake()
        {
            Inject();
        }
        public void Inject()
        {
            if (injected) return;
            injected = true;
            if (context == null) context = FindObjectOfType<GameContext>();
            foreach (var item in GetComponents<MonoBehaviour>())
                if (item != this)
                {
                    Debug.Log("Injecting for " + gameObject.name);
                    context.Inject(item);
                }
        }
    }
}