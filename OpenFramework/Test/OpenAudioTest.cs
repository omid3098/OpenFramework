// using System.Collections;
// using OpenAudio;
// using UnityEngine;
// using UnityEngine.Assertions;
// using UnityEngine.TestTools;

// namespace OpenFramework
// {
//     public class OpenAudioTest
//     {
//         [UnityTest]
//         public IEnumerator Audio_Service_New_Instance()
//         {
//             var audioManager = new AudioManager();
//             audioManager.Init();
//             Assert.IsNotNull(audioManager);
//             audioManager.Play(OpenAudio.AudioType.button_click);
//             yield return new WaitForSeconds(1f);
//             audioManager.Play(OpenAudio.AudioType.island_swipe);
//             yield return new WaitForSeconds(1f);
//             audioManager.Play(OpenAudio.AudioType.button_click);
//             yield return null;
//         }
//     }
// }