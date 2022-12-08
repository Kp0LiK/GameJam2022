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

        private PlayerBehaviour _playerBehaviour;
        private float maxHealth = 100f;
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
            Debug.Log(health);
            _fillHealthImage.DOFillAmount(health/maxHealth, 0.5f);
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
                //todo HealSystem
            }
        }
    }   
}