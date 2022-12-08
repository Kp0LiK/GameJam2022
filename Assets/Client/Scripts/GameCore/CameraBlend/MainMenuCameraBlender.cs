using System.Threading.Tasks;
using Cinemachine;
using UnityEngine;

namespace Client
{
    public class MainMenuCameraBlender : MonoBehaviour
    {
        [SerializeField] private CinemachineVirtualCamera _firstFrame;
        [SerializeField] private CinemachineVirtualCamera _secondFrame;

        private void Awake()
        {
            Restore();
            
            _firstFrame.gameObject.SetActive(true);
        }

        private async void Start()
        {
            await Task.Delay(500);
            _firstFrame.gameObject.SetActive(false);
            _secondFrame.gameObject.SetActive(true);
        }

        private void Restore()
        {
            _firstFrame.gameObject.SetActive(false);
            _secondFrame.gameObject.SetActive(false);
        }
    }
}