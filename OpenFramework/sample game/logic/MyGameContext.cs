using System;
using System.Collections;
using OpenFramework;
using UnityEngine;

public class MyGameContext : GameContext
{
    public override event OnReadyDelegate OnReady;

    protected override void OnReadyCallback()
    {
        Debug.Log("3- OnReadyCallback will call after all services are successfully initialized. so we can go anywere we want!");
        var gameService = (MyGameService)GetService<IGameService>();
        gameService.Intro();
        if (OnReady != null) OnReady.Invoke();
    }

    public override void SetupGameContext()
    {
        Debug.Log("1- First we register all Services");
        Register<MyGameService>();
        Register<MyAudioService>();
        Register<MyParticleService>();

        Debug.Log("2- Then we initialize all services and pass this context to all of them.");        
        Init(this);
    }
}