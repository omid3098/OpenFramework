namespace OpenFramework
{
    using UnityEngine;
    public class MouseInput
    {
        public delegate void MouseClick(Vector2 position);
        public delegate void MouseSwipe(swipeDirection direction);
        public delegate void MouseRelease(Vector2 position);
        public enum swipeDirection
        {
            left,
            right,
            up,
            down,
        }
    }
    public interface IInputService : IService
    {
        event MouseInput.MouseClick OnClick;
        event MouseInput.MouseSwipe OnSwipe;
        event MouseInput.MouseRelease OnRelease;
    }
}
