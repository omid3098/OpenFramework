namespace OpenFramework
{
    using System.Collections.Generic;
    using UnityEngine;
    public interface IPrefabContainer
    {
        List<GameObject> prefabs { get; set; }
    }
}