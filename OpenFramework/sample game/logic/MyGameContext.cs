using System;
using System.Collections;
using OpenFramework;
using OpenFramework.Helper;
using UnityEngine;
namespace OpenFramework.Sample
{
    public class MyGameContext : GameContext
    {
        public override event OnReadyDelegate OnReady;

        protected override void OnReadyCallback()
        {
            Debug.Log("3- OnReadyCallback will call after all services are successfully initialized. so we can go anywere we want!");
            var gameService = (MyGameService)GetService<IGameService>();
            gameService.StartGame();
            if (OnReady != null) OnReady.Invoke();
        }

        public override void SetupGameContext()
        {
            Debug.Log("1- First we register all Services");
            Register<MyGameService>();
            Register<MyAudioService>();

            Debug.Log("2- Then we initialize all services and pass this context to all of them.");
            Init(this);
        }
    }
}