using System.Collections.Generic;
using UnityEngine;
namespace OpenFramework
{
    public class InjectBehaviour : MonoBehaviour
    {
        private static GameContext context;
        private void Awake()
        {
            if (context == null) context = FindObjectOfType<GameContext>();
            foreach (var item in GetComponents<MonoBehaviour>())
                if (item != this)
                    context.Inject(item);
        }
    }
}