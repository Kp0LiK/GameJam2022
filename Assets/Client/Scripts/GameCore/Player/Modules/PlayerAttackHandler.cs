using UnityEngine;
using UnityEngine.InputSystem;

namespace Client
{
    public class PlayerAttackHandler : MonoBehaviour
    {
        [SerializeField] private Transform _attackPoint;
        [SerializeField] private Projectile _projectile;

        private PlayerBehaviour _playerBehaviour;

        private void Awake()
        {
            _playerBehaviour = GetComponentInParent<PlayerBehaviour>();
        }

        private void FixedUpdate()
        {
            if (Mouse.current.press.wasReleasedThisFrame && _playerBehaviour.Energy > 0)
            {
                _playerBehaviour.Animator.SetTrigger("KobyzF");
                _playerBehaviour.Energy -= 20f;
                var newProjectile = Instantiate(_projectile, _attackPoint.position, _attackPoint.rotation);
                newProjectile.EntityData = _playerBehaviour.Data;
                newProjectile.Rigidbody.velocity = transform.TransformDirection(new Vector3(0, 0, 10f));
                _playerBehaviour.AudioSource.PlayOneShot(_playerBehaviour.AudioData.OnAttack);
            }
            else
            {
                _playerBehaviour.Animator.SetTrigger("Idle");

            }
        }
    }
}