using System.Collections;
using System.Collections.Generic;
using Client;
using UnityEngine;
using UnityEngine.InputSystem;

public class BossParticleAttack : MonoBehaviour
{
    [SerializeField] private Transform _attackPoint;
    [SerializeField] private Projectile _projectile;

    private BossBehaviour _bossBehaviour;

    private void Awake()
    {
        _bossBehaviour = GetComponentInParent<BossBehaviour>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.TryGetComponent(out PlayerBehaviour playerBehaviour))
        {
            _bossBehaviour.Animator.SetBool("isMagick", true);
            var newProjectile = Instantiate(_projectile, _attackPoint.position, _attackPoint.rotation);
            newProjectile.EntityData = _bossBehaviour.Data;
            newProjectile.Rigidbody.velocity = transform.TransformDirection(new Vector3(0, 0, 10f));
        }
        else
        {
            _bossBehaviour.Animator.SetTrigger("Idle");
        }
    }
}