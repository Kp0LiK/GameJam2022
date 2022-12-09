using System;
using UnityEngine;

namespace Client
{
    [SelectionBase]
    public class PlayerBehaviour : MonoBehaviour, IDamageable
    {
        [SerializeField] private EntityData data;
        [SerializeField] private float _maxEnergy;
        [SerializeField] private EnemyAudioData audioData;


        private int _health;
        private float _energy;

        private Animator _animator;
        
        private AudioSource _audioSource;
        public EntityData Data => data;

        public Animator Animator => _animator;
        public AudioSource AudioSource => _audioSource;

        public EnemyAudioData AudioData => audioData;


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

        private void Awake()
        {
            _animator = GetComponent<Animator>();
            _audioSource = GetComponent<AudioSource>();
        }

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
                _audioSource.PlayOneShot(audioData.OnDetect);

            }

            UpdateHealth();
        }

        private void UpdateHealth() => HealthChanged?.Invoke(Mathf.RoundToInt( data.Health));
    }
    
    [Serializable]
    public class PlayerAudioData
    {
        public AudioClip OnAttack;
        public AudioClip OnDetect;
        public AudioClip OnUnDetect;
        public AudioClip OnMove;
        public AudioClip OnDie;
    }
}