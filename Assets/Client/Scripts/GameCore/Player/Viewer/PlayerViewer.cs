using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

public class HealthViewer : MonoBehaviour
{
    [SerializeField] private Slider _healthViewer;
    [SerializeField] private Image _fillImage;

    private PlayerBehaviour _playerBehaviour;

    [Inject]
    public void Constructor(PlayerBehaviour playerBehaviour)
    {
        _playerBehaviour = playerBehaviour;
    }

    private void OnEnable()
    {
        _playerBehaviour.HealthChanged += OnHealthChanged;
    }

    private void OnHealthChanged(int obj)
    {
        throw new NotImplementedException();
    }

    private void OnDisable()
    {
        
    }
}
