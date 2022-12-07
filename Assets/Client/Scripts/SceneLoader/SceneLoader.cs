using System;
using Cysharp.Threading.Tasks;
using DG.Tweening;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class SceneLoader : MonoBehaviour
{
    [SerializeField] private Image _image;

    private void Awake()
    {
        _image.DOFade(0, 0f);
    }

    public async void LoadSceneAsync(string scene)
    {
        await _image.DOFade(1, 0.5f).AsyncWaitForCompletion();
        await SceneManager.LoadSceneAsync(scene);
        await _image.DOFade(0, 0.5f).AsyncWaitForCompletion();
    }
}