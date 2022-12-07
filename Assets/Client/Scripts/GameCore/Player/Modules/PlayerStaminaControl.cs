﻿using System.Collections;
using UnityEngine;

namespace Client
{
    [RequireComponent(typeof(PlayerBehaviour))]
    public class PlayerStaminaControl : MonoBehaviour
    {
        [SerializeField] private float _restoreDelay;
        [SerializeField] private float _multiplayer;
        
        private PlayerBehaviour _player;

        private Coroutine _restoreRoutine;
        private WaitForSeconds _delay;

        private WaitForEndOfFrame _forEndOfFrame;

        private void Awake()
        {
            _forEndOfFrame = new WaitForEndOfFrame();
            _delay = new WaitForSeconds(_restoreDelay);
            _player = GetComponent<PlayerBehaviour>();
        }

        private void OnEnable()
        {
            _player.EnergyChanged += OnEnergyChange;
        }

        private void OnDisable()
        {
            _player.EnergyChanged -= OnEnergyChange;
        }

        private void OnEnergyChange(float value)
        {
            if (value < 100)
                _restoreRoutine = StartCoroutine(Restore(value));
            else
            {
                if (ReferenceEquals(_restoreRoutine, null))
                    return;

                StopCoroutine(_restoreRoutine);
                _restoreRoutine = null;
            }
        }

        private IEnumerator Restore(float currentValue)
        {
            if (currentValue >= 100)
            {
                StopCoroutine(_restoreRoutine);
                yield return _forEndOfFrame;
            }
            
            yield return _delay;
            _player.Energy += _multiplayer;
        }
    }
}