namespace OpenFramework.Helper.AudioService
{
    using System;
    using System.Collections.Generic;
    using UnityEngine;

    public interface IAudioService<TEnum> : IService where TEnum : struct, IComparable
    {
        void Play(TEnum type, bool loop);
        void Stop(TEnum type);
        void Pause(TEnum type);
        void StopAll();
        void SetVolume(TEnum audioType, float vol);
        void SetVolume(float vol);
        void Mute();
        void UnMute();
    }
}