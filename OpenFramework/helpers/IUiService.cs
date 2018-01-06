namespace OpenFramework.Helper
{
    using System;
    using OpenUi;
    using UnityEngine;

    public interface IUiService<T, T1> : IService
        where T : struct, IConvertible
        where T1 : struct, IConvertible
    {
        Canvas canvas { get; set; }
        void ChangeWindow(T type, Action OnComplete);
        Modal<T1> ShowModal(T1 type, Action OnComplete);
        Modal<T1> HideModal(T1 type, Action OnComplete);
    }
}