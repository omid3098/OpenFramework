namespace OpenFramework
{
    using System.Collections.Generic;
    using UnityEngine;

    public abstract class PrefabContainer : MonoBehaviour, IPrefabContainer
    {
        public abstract List<GameObject> prefabs { get; set; }
    }
}