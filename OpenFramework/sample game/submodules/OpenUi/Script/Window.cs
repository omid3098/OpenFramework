using System;
using System.Collections.Generic;
using OpenUi;
using UnityEngine;

namespace OpenUi
{
    public class Window<T, TMod> : ViewBase
        where T : struct, IConvertible
        where TMod : struct, IConvertible
    {
        #region Fields
        public T windowType;
        public List<Modal<TMod>> modalList { get; private set; }
        #endregion

        #region Methods
        protected override void Awake()
        {
            base.Awake();
            modalList = new List<Modal<TMod>>();
        }
        public void Hide(Action OnComplete)
        {
            if (hideTransition != null) hideTransition.Play(_onPlayCallback: OnComplete);
            else Debug.LogError("There is no hideTransition component on this window.");
        }

        public void Show(Action OnComplete)
        {
            if (showTransition != null) showTransition.Play(_onPlayCallback: OnComplete);
            else Debug.LogError("There is no showTransition component on this window.");
        }

        public void AddModal(Modal<TMod> modal)
        {
            modalList.Add(modal);
        }

        public Modal<TMod> GetModal(TMod modalType)
        {
            var modal = modalList.Find(x => EqualityComparer<TMod>.Default.Equals(x.modalType, modalType));
            return modal;
        }

        internal void RemoveModal(Modal<TMod> modal)
        {
            modalList.Remove(modal);
        }
        #endregion
    }
}