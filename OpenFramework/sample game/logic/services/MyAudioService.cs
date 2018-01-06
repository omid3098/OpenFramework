using System.Collections;
using System.Collections.Generic;
using OpenAudio;
using OpenAudio.Database;
using OpenFramework;
using OpenFramework.Helper;
using UnityEngine;
namespace OpenFramework.Sample
{
    public class MyAudioService : IAudioService<OpenAudio.AudioType>
    {
        public GameContext context { get; set; }

        private AudioManager audioManager;

        public bool ready { get; set; }

        public IEnumerator Init()
        {
            audioManager = new AudioManager();
            audioManager.Init();
            ready = true;
            yield return 0;
        }
        public void Play(OpenAudio.AudioType audioType, bool loop = false)
        {
            audioManager.Play(audioType, loop);
        }

        public void StopAll()
        {
            audioManager.StopAll();
        }

        public void Stop(OpenAudio.AudioType audioType)
        {
            audioManager.Stop(audioType);
        }
        public void Pause(OpenAudio.AudioType audioType)
        {
            audioManager.Pause(audioType);
        }

        public void SetVolume(OpenAudio.AudioType audioType, float vol)
        {
            audioManager.SetVolume(audioType, vol);
        }
        public void SetVolume(float vol)
        {
            audioManager.SetVolume(vol);
        }

        public void Mute()
        {
            audioManager.Mute();
        }

        public void UnMute()
        {
            audioManager.UnMute();
        }

        public void StartService()
        {
            throw new System.NotImplementedException();
        }

        public void StopService()
        {
            throw new System.NotImplementedException();
        }
    }
}