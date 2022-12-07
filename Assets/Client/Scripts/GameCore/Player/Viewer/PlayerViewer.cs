using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

namespace Client
{
    public class PlayerViewer : MonoBehaviour
    {
        [SerializeField] private Slider _healthViewer;
        [SerializeField] private Slider _energyViewer;
        [SerializeField] private Image _fillHealthImage;
        [SerializeField] private Image _fillEnergyImage;

        private PlayerBehaviour _playerBehaviour;

        [Inject]
        public void Constructor(PlayerBehaviour playerBehaviour)
        {
            _playerBehaviour = playerBehaviour;
        }

        private void OnEnable()
        {
            _playerBehaviour.HealthChanged += OnHealthChanged;
            _playerBehaviour.EnergyChanged += OnEnergyChanged;
        }

        private void OnDisable()
        {
            _playerBehaviour.HealthChanged -= OnHealthChanged;
            _playerBehaviour.EnergyChanged -= OnEnergyChanged;
        }

        private void OnHealthChanged(int health)
        {
            _healthViewer.DOValue(health, 0.5f);
            if (health <= 0)
            {
                _fillHealthImage.DOFade(0, 0.5f);
            }
        }

        private void OnEnergyChanged(float energy)
        {
            _energyViewer.DOValue(energy, 0.5f);
            if (energy <= 0)
            {
                _fillEnergyImage.DOFade(0, 0.5f);
            }
        }
    }   
}