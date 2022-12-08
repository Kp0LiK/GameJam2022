using UnityEngine;
using UnityEngine.Events;

namespace Client
{
    public class PlayerDetector : MonoBehaviour
    {
        public event UnityAction Entered;
        public event UnityAction DetectExited;

        public PlayerBehaviour PlayerTarget { get; private set; }
    
        private void OnTriggerEnter(Collider other)
        {
        
            if (!other.TryGetComponent(out PlayerBehaviour playerBehaviour)) return;
            PlayerTarget = playerBehaviour;
            Entered?.Invoke();
        }

        private void OnTriggerExit(Collider other)
        {
        
            if (!other.TryGetComponent(out PlayerBehaviour _)) return;
            DetectExited?.Invoke();
            PlayerTarget = null;
        }
    }   
}