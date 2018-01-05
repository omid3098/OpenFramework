namespace OpenFramework
{
    using System.Collections;

    public interface IService
    {
        GameContext context { get; set; }
        bool ready { get; set; }
        IEnumerator Init();
        void StartService();
        void StopService();
    }
}