using System.Collections.Generic;
using OpenFramework.Helper.AudioService.Database;
using UnityEngine;

namespace OpenFramework.Helper.AudioService
{

    public class AudioManager : MonoBehaviour
    {
        #region Properties
        private static AudioManager _instance;
        public static AudioManager instance
        {
            get
            {
                if (_instance == null)
                {
                    _instance = GameObject.FindObjectOfType<AudioManager>();
                    if (_instance == null)
                    {
                        var t = new GameObject("audio-manager");
                        _instance = t.AddComponent<AudioManager>();
                    }
                }
                return _instance;
            }
        }

        #endregion

        #region Fields

        // List<AudioDatabaseItem> audioItemList;
        List<Audio> audioSourcePool;
        [SerializeField] List<AudioDatabase> allDataBases;

        // List<AudioClip> audioClips;

        #endregion

        #region Methods

        void Awake()
        {
            GameObject.DontDestroyOnLoad(this);
            LoadService();
        }

        private void LoadService()
        {
            if (GameObject.FindObjectOfType<AudioListener>() == null)
            {
                gameObject.AddComponent<AudioListener>();
            }

            allDataBases = new List<AudioDatabase>();
            audioSourcePool = new List<Audio>();
            // audioClips = new List<AudioClip>();

            allDataBases.AddRange(Resources.LoadAll<AudioDatabase>(""));
            // foreach (var audioDB in allDataBases)
            // {
            //     audioItemList.AddRange(audioDB.audioDBItems);
            // }
        }

        public void Play(AudioType audioType, bool loop = false)
        {
            Audio audio = null;
            foreach (Audio item in audioSourcePool)
            {
                if (!item.isplaying())
                {
                    if (item.audioDatabaseItem.type == audioType)
                    {
                        item.Play().Loop(loop);
                        return;
                    }
                    audio = item;
                }
            }

            // Find clip database item and set clip if it is not set;
            AudioDatabaseItem audioDatabaseItem = null;
            AudioDatabase db = null;
            foreach (var database in allDataBases)
            {
                var tmp = database.audioDBItems.Find(x => x.type == audioType);
                if (tmp != null)
                {
                    audioDatabaseItem = tmp;
                    db = database;
                    break;
                }
            }

            if (audioDatabaseItem != null && audioDatabaseItem.audioClip == null)
            {
                string clipPath = db.ResourcePath + "/" + audioDatabaseItem.audioName;
                var clip = Resources.Load<AudioClip>(clipPath);
                if (clip != null) audioDatabaseItem.audioClip = clip;
                else Debug.Log("could not find audioClip at: " + clipPath);
            }

            if (audio == null)
            {

                audio = new Audio(audioDatabaseItem);
                audioSourcePool.Add(audio);
            }
            audio.Init(audioDatabaseItem);
            audio.Play().Loop(loop);
        }

        // private AudioDatabaseItem GetAudioDatabaseItem(AudioType audioType)
        // {
        //     AudioDatabaseItem t = null;
        //     foreach (var db in allDataBases)
        //     {
        //         t = db.audioDBItems.Find(x => x.type == audioType);
        //     }
        //     return t;
        // }

        public void StopAll()
        {
            foreach (var audio in audioSourcePool)
            {
                if (audio.isplaying()) audio.Stop();
            }
        }

        public void Stop(AudioType audioType)
        {
            foreach (var audio in audioSourcePool)
            {
                if (audio.audioDatabaseItem.type == audioType)
                {
                    audio.Stop();
                    break;
                }
            }
        }
        public void Pause(AudioType audioType)
        {
            foreach (var audio in audioSourcePool)
            {
                if (audio.audioDatabaseItem.type == audioType)
                {
                    audio.Pause();
                    break;
                }
            }
        }

        public void SetVolume(AudioType audioType, float vol)
        {
            foreach (var _audio in audioSourcePool)
            {
                if (_audio.audioDatabaseItem.type == audioType)
                {
                    _audio.SetVolume(vol);
                }
            }
        }
        public void SetVolume(float vol)
        {
            foreach (var _audio in audioSourcePool)
            {
                if (_audio.isplaying())
                {
                    _audio.SetVolume(vol);
                }
            }
        }

        public void Mute()
        {
            foreach (var _audio in audioSourcePool)
            {
                _audio.Mute();
            }
        }

        public void UnMute()
        {
            foreach (var _audio in audioSourcePool)
            {
                _audio.UnMute();
            }
        }
        #endregion
    }
}