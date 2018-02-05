namespace OpenFramework.Helper.AudioService
{
    using System.Collections;
    public class AudioService : IAudioService<AudioType>
    {
        private AudioManager audioManager;

        public GameContext context { get; set; }
        public bool ready { get; set; }
        public IEnumerator Init()
        {
            audioManager = AudioManager.instance;
            yield return null;
        }

        public void Mute()
        {
            audioManager.Mute();
        }

        public void Pause(AudioType type)
        {
            audioManager.Pause(type);
        }

        public void Play(AudioType type, bool loop)
        {
            audioManager.Play(type, loop);
        }

        public void SetVolume(AudioType audioType, float vol)
        {
            audioManager.SetVolume(audioType, vol);
        }

        public void SetVolume(float vol)
        {
            audioManager.SetVolume(vol);
        }


        public void Stop(AudioType type)
        {
            audioManager.Stop(type);
        }

        public void StopAll()
        {
            audioManager.StopAll();
        }
        public void UnMute()
        {
            audioManager.UnMute();
        }
        public void StopService()
        {
            throw new System.NotImplementedException();
        }

        public void StartService()
        {
            throw new System.NotImplementedException();
        }
    }
}