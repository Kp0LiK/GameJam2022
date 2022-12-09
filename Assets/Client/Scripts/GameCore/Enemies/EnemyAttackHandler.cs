using System;
using System.Collections;
using UnityEngine;

namespace Client
{
    public class EnemyAttackHandler : MonoBehaviour
    {
        private SpiderBehaviour _spiderBehaviour;
        private Coroutine _attackRoutine;


        private void Awake()
        {
            _spiderBehaviour = GetComponentInParent<SpiderBehaviour>();
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
            StopCoroutine(_attackRoutine);
        }

        private IEnumerator AttackRoutine()
        {
            while (!ReferenceEquals(_spiderBehaviour.Target, null))
            {
                yield return new WaitForSeconds(1.5f);
                _spiderBehaviour.Target.ApplyDamage(_spiderBehaviour.Data.Damage);
            }
        }
    }
}