namespace OpenFramework
{// Game Service
    using System.Collections;

    public interface IGameService : IService
    {
        IEnumerator Intro();
        void Running();
        void Finished();
    }
}