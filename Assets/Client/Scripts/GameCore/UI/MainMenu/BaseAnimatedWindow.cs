using System;
using UnityEngine;

namespace Client
{
    public abstract class BaseAnimatedWindow : MonoBehaviour
    {
        private BaseWindow _animation;


        protected BaseAnimatedWindow(BaseWindow animation)
        {
            _animation = animation;
        }

        public void Open()
        {
            OnOpen();
        }

        protected abstract void OnOpen();
    }
}