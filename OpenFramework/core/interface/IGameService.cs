namespace OpenFramework
{// Game Service
    using System.Collections;

    public interface IGameService : IService
    {
        void Intro();
        void Running();
        void Finished();
    }
}