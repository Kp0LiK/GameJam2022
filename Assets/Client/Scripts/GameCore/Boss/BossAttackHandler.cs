using System;
using System.Collections;
using UnityEngine;

namespace Client
{
    public class BossAttackHandler : MonoBehaviour
    {
        private BossBehaviour  _bossBehaviour;
        private Coroutine _attackRoutine;
        
        public EnemyState State { get; private set; }


        private void Awake()
        {
            _bossBehaviour = GetComponentInParent<BossBehaviour>();
        }

        private void OnTriggerEnter(Collider other)
        {
            if (other.TryGetComponent(out PlayerBehaviour playerBehaviour))
            {
                _attackRoutine = StartCoroutine(AttackRoutine());
            }
        }

        private void OnTriggerExit(Collider other)
        {
            _bossBehaviour.Animator.SetBool("isMagick", true);
            StopCoroutine(_attackRoutine);
        }

        private IEnumerator AttackRoutine()
        {
            while (!ReferenceEquals(_bossBehaviour.Target, null))
            {
                yield return new WaitForSeconds(1.5f);
                _bossBehaviour.Animator.SetTrigger("isAttack");
                _bossBehaviour.Target.ApplyDamage(_bossBehaviour.Data.Damage);
            }
        }
    }
}