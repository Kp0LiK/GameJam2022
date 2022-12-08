using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.Events;

namespace Client
{
    public class SpiderBehaviour : MonoBehaviour
    {
        [SerializeField] private EntityData _data;
        [SerializeField] private AudioSource _audio;
        [SerializeField] private PlayerBehaviour _player;

        private Animator _animator;
        private PlayerDetector _playerDetector;
        private AttackTrigger _attackDetector;
        private static readonly int Walk = Animator.StringToHash("Walk");
        private static readonly int IsAttack = Animator.StringToHash("isAttack");
        private static readonly int IsDead = Animator.StringToHash("isDead");

        public EntityData Data => _data;

        public event UnityAction<float> HealthChanged;

        private void Awake()
        {
            _animator = GetComponent<Animator>();
            _playerDetector = GetComponentInChildren<PlayerDetector>();
            _attackDetector = GetComponentInChildren<AttackTrigger>();
        }

        private void OnEnable()
        {
            _playerDetector.Entered += OnEntered;
            _playerDetector.DetectExited += OnDetectExited;

            _attackDetector.Entered += OnSpiderAttackDetect;
            _attackDetector.DetectExited += OnAttackDetectExited;
        }

        private void OnDisable()
        {
            _playerDetector.Entered -= OnEntered;
            _playerDetector.DetectExited -= OnDetectExited;

            _attackDetector.Entered -= OnSpiderAttackDetect;
            _attackDetector.DetectExited -= OnAttackDetectExited;
        }

        private void OnEntered()
        {
            _animator.SetFloat(Walk, 1);
        }

        private void OnDetectExited()
        {
            _animator.SetFloat(Walk, 0);
        }

        private async void OnSpiderAttackDetect()
        {
            _animator.SetBool(IsAttack, true);
            while (true)
            {
                await Task.Delay(1500);

                if (_data.isDied)
                    return;

                if (!ReferenceEquals(_player, null))
                {
                    _player.ApplyDamage(_data.Damage);
                }
                else
                {
                    return;
                }
            }
        }

        private void OnAttackDetectExited()
        {
            _animator.SetBool(IsAttack, false);
        }

        public void ApplyDamage(float damage)
        {
            _data.Health -= damage;

            if (_data.Health <= 0)
            {
                _data.Health = 0;
                _data.isDied = true;
            }

            if (_data.isDied)
            {
                _animator.SetBool(IsDead, true);
            }
            HealthChanged?.Invoke(_data.Health);
        }
    }
}