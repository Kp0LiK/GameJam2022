using UnityEngine;
using UnityEngine.Events;

namespace Client
{
    public class PlayerDetector : MonoBehaviour
    {
        public event UnityAction<PlayerBehaviour> Entered;
        public event UnityAction<PlayerBehaviour> DetectExited;
        
        private void OnTriggerEnter(Collider other)
        {
        
            if (!other.TryGetComponent(out PlayerBehaviour playerBehaviour)) return;
            Entered?.Invoke(playerBehaviour);
        }

        private void OnTriggerExit(Collider other)
        {
        
            if (!other.TryGetComponent(out PlayerBehaviour player)) return;
            DetectExited?.Invoke(player);
        }
    }   
}