namespace OpenFramework.Sample
{
    using System.Collections.Generic;
    using UnityEngine;

    public interface IAudioService<TEnum> : IService
    {
        void Play(TEnum type, bool loop);
        void Stop(TEnum type);
        void StopAll();
        void SetVolume(TEnum audioType, float vol);
        void SetVolume(float vol);
        void Mute();
        void UnMute();
    }
}