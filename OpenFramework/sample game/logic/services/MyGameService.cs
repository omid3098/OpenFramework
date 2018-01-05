using System.Collections;
using System.Collections.Generic;
using OpenFramework;
using UnityEngine;

public class MyGameService : IGameService, IUpdatable
{
    private MyAudioService _audioService;
    private MyParticleService _particleService;

    public bool intro { get; private set; }
    public GameContext context { get; set; }
    public bool ready { get; set; }

    public IEnumerator Init()
    {
        intro = false;
        _audioService = (MyAudioService)context.GetService<IAudioService<OpenAudio.AudioType>>();
        _particleService = (MyParticleService)context.GetService<IParticleService>();
        ready = true;
        yield return 0;
    }


    public void Intro()
    {
        Debug.Log("4- for example we came to game service and played some audio. you can load another level etc..");
        _audioService.Play(OpenAudio.AudioType.audio1, false);
        _particleService.Play();
        intro = true;
    }
    public void Running()
    {
    }
    public void Finished()
    {
    }

    public void IUpdate()
    {
        Debug.Log("5- You dont need to use MonoBehaviour for having Update anymore. if your service inherit from IUpdatable interface, IUpdate is your new Update method.");
        if (!context.ready) return;
        Running();
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
