using OpenFramework.Helper.AudioService;
using UnityEngine;

public class AudioSampleUsage : MonoBehaviour
{
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Q))
        {
            AudioManager.instance.Play(OpenFramework.Helper.AudioService.AudioType.audio1, true);
        }
        if (Input.GetKeyDown(KeyCode.W))
        {
            AudioManager.instance.Play(OpenFramework.Helper.AudioService.AudioType.audio2);
        }
    }
}
