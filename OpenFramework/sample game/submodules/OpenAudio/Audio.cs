using System;
using OpenAudio.Database;
using UnityEngine;

namespace OpenAudio
{
    public class Audio
    //  : IAudio
    {
        AudioSource audioSource;

        GameObject _gameObject;
        AudioDatabaseItem _audioDatabaseItem;
        public AudioDatabaseItem audioDatabaseItem
        {
            get
            {
                return _audioDatabaseItem;
            }
            private set
            {
                _audioDatabaseItem = value;
                audioSource.clip = value.audioClip;
            }
        }

        public Audio(AudioDatabaseItem _audioDatabaseItem)
        {
            if (_gameObject == null)
            {
                _gameObject = new GameObject("audioSource");
                GameObject.DontDestroyOnLoad(_gameObject);
                // _gameObject.transform.SetParent(OpenAudio.AudioManager.instance.transform, false);
                audioSource = _gameObject.AddComponent<AudioSource>();
            }
            audioDatabaseItem = _audioDatabaseItem;
        }

        /// <summary>
        /// Set Audio volume
        /// </summary>
        /// <param name="volume">between 0 and 1</param>
        public Audio SetVolume(float vol)
        {
            audioSource.volume = vol;
            return this;
        }


        public Audio Play()
        {
            audioSource.Play();
            return this;
        }

        public Audio Loop(bool loop)
        {
            audioSource.loop = loop;
            return this;
        }

        public Audio Stop()
        {
            audioSource.Stop();
            return this;
        }

        public bool isplaying()
        {
            return audioSource.isPlaying;
        }

        public Audio SetParent(Transform parent)
        {
            _gameObject.transform.SetParent(parent, false);
            return this;
        }

        public void Pause()
        {
            audioSource.Pause();
        }

        public void Mute()
        {
            audioSource.mute = true;
        }

        public void UnMute()
        {
            audioSource.mute = false;
        }
    }
}