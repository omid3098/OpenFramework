using System.Collections.Generic;
using UnityEngine;

namespace OpenFramework.Helper.AudioService.Database
{
    [CreateAssetMenu(menuName = "Audio Database")]
    public class AudioDatabase : ScriptableObject
    {
        public string ResourcePath;

        public List<AudioDatabaseItem> audioDBItems = new List<AudioDatabaseItem>();
    }
}