using System;
using System.Collections;
using OpenFramework;

public class MyGameContext : GameContext
{
    public override event OnReadyDelegate OnReady;

    void Awake()
    {
        SetupGameContext();
    }

    protected override void OnReadyCallback()
    {
        var gameService = (MyGameService)GetService<IGameService>();
        StartCoroutine(gameService.Intro());
        if (OnReady != null) OnReady.Invoke();
    }

    public override void SetupGameContext()
    {
        Register<MyGameService>();
        Register<MyAudioService>();
        Register<MyParticleService>();
        Init(this);
    }
}