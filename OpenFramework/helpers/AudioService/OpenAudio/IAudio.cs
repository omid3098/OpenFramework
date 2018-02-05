namespace OpenFramework.Helper.AudioService
{
    public interface IAudio
    {
        void Play();
        void Stop();
        void SetVolume(float vol);
        bool isplaying();
    }
}