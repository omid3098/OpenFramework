using System.Collections;
using OpenFramework;
using UnityEngine;

public class MyParticleService : IParticleService
{
    public GameContext context { get; set; }
    public bool ready { get; set; }

    public IEnumerator Init()
    {
        ready = true;
        yield return 0;
    }
    public void Play()
    {
        Debug.Log("If we had some particles, here we could play them! but we don't have any :(");
    }

    public void StartService()
    {
        throw new System.NotImplementedException();
    }

    public void Stop()
    {
    }

    public void StopService()
    {
        throw new System.NotImplementedException();
    }
}
