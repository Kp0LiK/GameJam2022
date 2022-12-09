using System;
using System.Threading.Tasks;
using Cysharp.Threading.Tasks;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.Events;

namespace Client
{
    public enum EnemyState
    {
        Idle,
        Follow,
        Attack,
        Die
    }

    public class SpiderBehaviour : MonoBehaviour, IDamageable
    {
        [SerializeField] private EntityData _data;
        [SerializeField] private AudioSource _audio;
        [SerializeField] private int _attackDelayInSecond;
        [SerializeField] private float _attackDistance;
        [SerializeField] private EnemyAudioData audioData;
        
        private PlayerBehaviour _target;
        public EntityData Data => _data;

        public PlayerBehaviour Target => _target;

        public event UnityAction<float> HealthChanged;
        public EnemyState State { get; private set; }

        private Animator _animator;
        private PlayerDetector _playerDetector;


        private NavMeshAgent _meshAgent;
        private Rigidbody _rigidbody;
        private AudioSource _audioSource;


        private static readonly int Walk = Animator.StringToHash("Walk");
        private static readonly int IsAttack = Animator.StringToHash("isAttack");
        private static readonly int IsDead = Animator.StringToHash("isDead");


        private void Awake()
        {
            _rigidbody = GetComponent<Rigidbody>();
            _meshAgent = GetComponent<NavMeshAgent>();
            _animator = GetComponentInChildren<Animator>();
            _playerDetector = GetComponentInChildren<PlayerDetector>();
            _audioSource = GetComponent<AudioSource>();
        }

        private void Start()
        {
            State = EnemyState.Idle;
            _meshAgent.stoppingDistance = _attackDistance;
        }


        private void OnEnable()
        {
            _playerDetector.Entered += OnEntered;
            _playerDetector.DetectExited += OnDetectExit;
        }

        private void OnDisable()
        {
            _playerDetector.Entered -= OnEntered;
            _playerDetector.DetectExited -= OnDetectExit;
        }

        private void FixedUpdate()
        {
            switch (State)
            {
                case EnemyState.Idle:
                    break;
                case EnemyState.Follow:
                    FollowToTarget();
                    break;
                case EnemyState.Attack:
                    break;
                case EnemyState.Die:
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }
        }

        private void OnEntered(PlayerBehaviour playerBehaviour)
        {
            _audioSource.PlayOneShot(audioData.OnDetect);
            
            State = EnemyState.Follow;
            _meshAgent.isStopped = false;
            _target = playerBehaviour;
            _animator.SetFloat(Walk, 1);
        }

        private void OnDetectExit(PlayerBehaviour playerBehaviour)
        {
            _audioSource.PlayOneShot(audioData.OnUnDetect);

            State = EnemyState.Idle;
            _meshAgent.isStopped = true;
            _target = null;
            _animator.SetFloat(Walk, 0);
        }

        private async void Attack()
        {
            _animator.SetTrigger(IsAttack);
            
            if (State == EnemyState.Idle)
            {
                _animator.SetFloat(Walk, 0);
                return;
            }

            
            await Task.Yield();
            if (!ReferenceEquals(_target, null))
            {
                await UniTask.Delay(1500);
                _audioSource.PlayOneShot(audioData.OnAttack);
                _target.ApplyDamage(_data.Damage);

                if (Vector3.Distance(transform.position, _target.transform.position) > _attackDistance)
                {
                    _animator.SetFloat(Walk, 1);
                    State = EnemyState.Follow;
                }
            }
        }

        private void FollowToTarget()
        {
            if (ReferenceEquals(_target, null))
            {
                State = EnemyState.Idle;
                return;
            }

            if (_meshAgent.isOnNavMesh)
            {
                var direction = _target.transform.position;
                _meshAgent.SetDestination(direction);

                var qTo = Quaternion.LookRotation(direction);
                qTo = Quaternion.Slerp(transform.rotation, qTo, 30.0f * Time.deltaTime);
                _rigidbody.MoveRotation(qTo);
            }

            if (Vector3.Distance(transform.position, _target.transform.position) < _attackDistance)
            {
                State = EnemyState.Attack;
            }
            else
            {
                State = EnemyState.Follow;
            }

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
                State = EnemyState.Die;
                _animator.SetBool(IsDead, true);
            }

            HealthChanged?.Invoke(_data.Health);
        }
    }

    [Serializable]
    public class EnemyAudioData
    {
        public AudioClip OnAttack;
        public AudioClip OnDetect;
        public AudioClip OnUnDetect;
        public AudioClip OnMove;
        public AudioClip OnDie;
    }
}