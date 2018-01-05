using System.Collections;
using System.Collections.Generic;
using OpenFramework;
using UnityEngine;
namespace OpenFramework.Sample
{
    public class MyGameService : IGameService, IUpdatable
    {
        private MyAudioService _audioService;

        public bool intro { get; private set; }
        public GameContext context { get; set; }
        public bool ready { get; set; }

        public IEnumerator Init()
        {
            _audioService = (MyAudioService)context.GetService<IAudioService<OpenAudio.AudioType>>();
            ready = true;
            yield return 0;
        }


        public void StartGame()
        {
            Debug.Log("4- for example we came to game service and played some audio. you can load another level etc..");
            _audioService.Play(OpenAudio.AudioType.audio1, false);
        }

        public void IUpdate()
        {
            if (!context.ready) return;
            Debug.Log("5- You dont need to use MonoBehaviour for having Update anymore. if your service inherit from IUpdatable interface, IUpdate is your new Update method.");
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