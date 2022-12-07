using System;
using System.Threading.Tasks;
using UnityEngine;

namespace Client
{
    [SelectionBase]
    public class PlayerBehaviour : MonoBehaviour, IDamageable
    {
        [SerializeField] private EntityConfig _config;
        [SerializeField] private float _maxEnergy;

        private int _health;
        private float _energy;
        public EntityConfig Config => _config;


        public int Health
        {
            get => _health;
            set
            {
                HealthChanged?.Invoke(value);
                _health = value;
                if (_health >= _config.Health)
                {
                    _health = (int) _config.Health;
                }
            }
        }

        public float Energy
        {
            get => _energy;
            set
            {
                EnergyChanged?.Invoke(value);
                _energy = value;
                if (_energy >= _maxEnergy)
                {
                    _energy = _maxEnergy;
                }
            }
        }

        public event Action<int> HealthChanged;
        public event Action<float> EnergyChanged;

        private void Start()
        {
            _health = 100;
            _energy = 100;
        }

        private void Update()
        {
            HealEnergy();

            Debug.Log(_energy);
        }

        public async void HealEnergy()
        {
            while (Math.Abs(_energy - 100) > 0.1f)
            {
                await Task.Delay(6000);
                Energy += 0.2f;
            }
        }

        public void ApplyDamage(float damage)
        {
            if (_config.Health <= 0)
            {
                _config.Health = 0;
                _config.isDied = true;
            }

            _config.Health -= damage;

            UpdateHealth();
        }

        private void UpdateHealth() => HealthChanged?.Invoke((int) _config.Health);
        
    }
}