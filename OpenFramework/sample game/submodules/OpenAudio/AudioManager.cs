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
        GameObject audioListenerGo;
        List<Audio> audioPool;
        List<AudioDatabase> allAudioDataBases;

        // List<AudioClip> audioClips;

        #endregion

        #region Methods

        public void Init()
        {
            audioListenerGo = null;
            LoadService();
        }

        private void LoadService()
        {
            AudioListener audioListener = GameObject.FindObjectOfType<AudioListener>();
            if (audioListener != null) audioListenerGo = audioListener.gameObject;
            if (audioListenerGo == null)
            {
                audioListenerGo = new GameObject("audio-listener");
                audioListenerGo.AddComponent<AudioListener>();
                GameObject.DontDestroyOnLoad(audioListenerGo);
            }
            Assert.IsNotNull(audioListenerGo);
            allAudioDataBases = new List<AudioDatabase>();
            audioPool = new List<Audio>();
            AudioDatabase[] collection = Resources.LoadAll<AudioDatabase>("");
            Assert.IsNotNull(collection);
            allAudioDataBases.AddRange(collection);
            foreach (var audioDatabase in allAudioDataBases)
            {
                foreach (var audioItem in audioDatabase.audioDBItems)
                {
                    audioItem.audioClip = null;
                }
            }
        }

        public void Play(AudioType audioType, bool loop = false)
        {
            Audio audio = null;
            foreach (Audio audioItem in audioPool)
            {
                if (!audioItem.isplaying())
                {
                    if (audioItem.audioDatabaseItem.type == audioType)
                    {
                        audioItem.Play().Loop(loop);
                        return;
                    }
                    // else audio = audioItem;
                }
            }

            // Find audioDatabaseItem in all databases and set the resource path and set clip if it is not set;
            AudioDatabaseItem audioDatabaseItem = null;
            string audioPath = "";
            foreach (var audioDatabase in allAudioDataBases)
            {
                var _audioDatabaseItem = audioDatabase.audioDBItems.Find(x => x.type == audioType);
                if (_audioDatabaseItem != null)
                {
                    audioDatabaseItem = _audioDatabaseItem;
                    audioPath = audioDatabase.ResourcePath + "/" + audioDatabaseItem.audioName;
                    break;
                }
            }
            Assert.IsNotNull(audioDatabaseItem);
            Assert.IsFalse(string.IsNullOrEmpty(audioPath));

            if (audioDatabaseItem.audioClip == null)
            {
                var clip = Resources.Load<AudioClip>(audioPath);
                Assert.IsNotNull(clip, "could not find audioClip at: " + audioPath);
                audioDatabaseItem.audioClip = clip;
            }

            if (audio == null)
            {
                audio = new Audio(audioDatabaseItem);
                audioPool.Add(audio);
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
            foreach (var audio in audioPool)
            {
                if (audio.isplaying()) audio.Stop();
            }
        }

        public void Stop(AudioType audioType)
        {
            foreach (var audio in audioPool)
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
            foreach (var audio in audioPool)
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
            foreach (var _audio in audioPool)
            {
                if (_audio.audioDatabaseItem.type == audioType)
                {
                    _audio.SetVolume(vol);
                }
            }
        }
        public void SetVolume(float vol)
        {
            foreach (var _audio in audioPool)
            {
                if (_audio.isplaying())
                {
                    _audio.SetVolume(vol);
                }
            }
        }

        public void Mute()
        {
            foreach (var _audio in audioPool)
            {
                _audio.Mute();
            }
        }

        public void UnMute()
        {
            foreach (var _audio in audioPool)
            {
                _audio.UnMute();
            }
        }
        #endregion
    }
}