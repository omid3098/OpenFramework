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
        Debug.Log("GameService");
        intro = false;
        _audioService = (MyAudioService)context.GetService<IAudioService<OpenAudio.AudioType>>();
        _particleService = (MyParticleService)context.GetService<IParticleService>();
        ready = true;
        yield return 0;
    }


    public IEnumerator Intro()
    {
        _audioService.Play(OpenAudio.AudioType.audio1, false);
        _particleService.Play();
        intro = true;
        yield return 0;
    }
    public void Running()
    {
    }
    public void Finished()
    {
        Debug.Log("Finished!!!");
    }
    public void IUpdate()
    {
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
