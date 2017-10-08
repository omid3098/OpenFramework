using System.Collections.Generic;
using OpenAudio.Database;
using UnityEngine;
using UnityEngine.Assertions;

namespace OpenAudio
{

    public class AudioManager
    {
        #region Properties

        #endregion

        #region Fields
        GameObject audioListener;
        List<Audio> audioSourcePool;
        List<AudioDatabase> allDataBases;

        // List<AudioClip> audioClips;

        #endregion

        #region Methods

        public void Init()
        {
            LoadService();
        }

        private void LoadService()
        {
            audioListener = GameObject.FindObjectOfType<AudioListener>().gameObject;
            if (audioListener == null)
            {
                audioListener = new GameObject("audio-listener");
                audioListener.AddComponent<AudioListener>();
                GameObject.DontDestroyOnLoad(audioListener);
            }

            allDataBases = new List<AudioDatabase>();
            audioSourcePool = new List<Audio>();
            AudioDatabase[] collection = Resources.LoadAll<AudioDatabase>("");
            allDataBases.AddRange(collection);
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
                var _audioDatabaseItem = database.audioDBItems.Find(x => x.type == audioType);
                if (_audioDatabaseItem != null)
                {
                    audioDatabaseItem = _audioDatabaseItem;
                    db = database;
                    break;
                }
            }
            Assert.IsNotNull(audioDatabaseItem);
            Assert.IsNotNull(db);

            if (audioDatabaseItem != null && audioDatabaseItem.audioClip == null)
            {
                string clipPath = db.ResourcePath + "/" + audioDatabaseItem.audioName;
                var clip = Resources.Load<AudioClip>(clipPath);
                Debug.Log("___ Loading audio clip at path: " + clipPath);
                Assert.IsNotNull(clip, "could not find audioClip at: " + clipPath);
                audioDatabaseItem.audioClip = clip;
            }

            if (audio == null)
            {
                audio = new Audio(audioDatabaseItem);
                audioSourcePool.Add(audio);
            }
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