using System;
using UnityEngine;

namespace Client
{
    [SelectionBase]
    public class PlayerBehaviour : MonoBehaviour, IDamageable
    {
        [SerializeField] private EntityData data;
        [SerializeField] private float _maxEnergy;

        private int _health;
        private float _energy;
        public EntityData Data => data;


        public int Health
        {
            get => _health;
            set
            {
                HealthChanged?.Invoke(value);
                _health = value;
                if (_health >= data.Health)
                {
                    _health = (int) data.Health;
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

        public void ApplyDamage(float damage)
        {
            if (data.Health <= 0)
            {
                data.Health = 0;
                data.isDied = true;
            }

            if (data.isDied)
            {
              //todo deathState
            }
            else
            {
                data.Health -= damage;
            }

            UpdateHealth();
        }

        private void UpdateHealth() => HealthChanged?.Invoke(Mathf.RoundToInt( data.Health));
    }
}