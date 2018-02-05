using UnityEngine;

namespace OpenFramework.Helper.AudioService.Database
{

    [System.Serializable]
    public class AudioDatabaseItem
    {
        public string audioName;
        public AudioType type;
        public AudioClip audioClip;
    }
}