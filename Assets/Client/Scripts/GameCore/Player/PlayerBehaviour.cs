using System;
using UnityEngine;

namespace Client
{
    public class PlayerBehaviour : MonoBehaviour
    {
        [SerializeField] private int _maxHealth;
        [SerializeField] private float _maxEnergy;

        private int _health;
        private float _energy;

        public int Health
        {
            get => _health;
            set
            {
                HealthChanged?.Invoke(value);
                _health = value;
                if (_health >= _maxHealth)
                {
                    _health = _maxHealth;
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
    }
}

