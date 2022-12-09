using Tools.Scene;
using UnityEngine;
using Zenject;

namespace Client
{
    public class TransitionNewLevel : MonoBehaviour
    {
        [SerializeField, Scene] private string _scene;
        private SceneLoader _sceneLoader;

        [Inject]
        public void Constructor(SceneLoader sceneLoader)
        {
            _sceneLoader = sceneLoader;
        }

        private void OnTriggerEnter(Collider other)
        {
            if (other.TryGetComponent(out PlayerBehaviour playerBehaviour))
            {
                Debug.Log($"{playerBehaviour.name} Here!");
                _sceneLoader.LoadSceneAsync(_scene);
            }
        }
    }

}