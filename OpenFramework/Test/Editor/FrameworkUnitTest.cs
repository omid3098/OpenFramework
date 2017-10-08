using UnityEngine;
using UnityEditor;
using UnityEngine.TestTools;
using NUnit.Framework;
using System.Collections;
using OpenFramework;

public class FrameworkUnitTest
{
    // A UnityTest behaves like a coroutine in PlayMode
    // and allows you to yield null to skip a frame in EditMode
    [UnityTest]
    public IEnumerator Game_Context_Exists_in_The_Scene()
    {
        // Use the Assert class to test conditions.
        // yield to skip a frame
        var gameContext = GameObject.FindObjectOfType<GameContext>();
        Assert.IsNotNull(gameContext);
        yield return null;
    }

    [UnityTest]
    public IEnumerator Game_Services_Are_Registered()
    {
        var gameContext = GameObject.FindObjectOfType<GameContext>();
        gameContext.SetupGameContext();
        // yield return null;
        Debug.Log(gameContext.serviceCount + " services were registered");
        Assert.IsTrue(gameContext.serviceCount != 0);
        yield return null;
    }
}
