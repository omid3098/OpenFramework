using System.Collections;
using System.Collections.Generic;
using OpenFramework;
using UnityEngine;

namespace OpenFramework.Sample
{
    public class Tester : MonoService
    {
        MyAudioService _audio;
        void Awake()
        {
            _audio = (MyAudioService)context.GetService<IAudioService<OpenAudio.AudioType>>();
        }
        void Update()
        {
            if (Input.GetKeyDown(KeyCode.Q))
            {
                _audio.Play(OpenAudio.AudioType.audio1, false);
            }
        }
    }
}