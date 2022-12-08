using UnityEngine;
using Zenject;

namespace Client
{
    public class TransisionNewLevel : MonoBehaviour
    {
        private SceneLoader _sceneLoader;

        [Inject]
        public void Constructor(SceneLoader sceneLoader)
        {
            _sceneLoader = sceneLoader;
        }
        private void OnCollisionEnter(Collision other)
        {
            if (other.gameObject.TryGetComponent(out PlayerBehaviour playerBehaviour))
            {
                _sceneLoader.LoadSceneAsync("Cave");
            }
        }
    }

}