namespace OpenFramework
{
    using System.Collections.Generic;
    using UnityEngine;

    public interface IAudioService<TEnum> : IService
    {
        void Play(TEnum type, bool loop);
        void Stop(TEnum type);
    }
}