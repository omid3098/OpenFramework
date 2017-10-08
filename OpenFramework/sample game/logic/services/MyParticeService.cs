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
