using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

namespace Client
{
    public class MainMenu : MonoBehaviour
    {
        [SerializeField] private Transform _logo;
    
        [SerializeField] private MenuButtonSegment _start;
        [SerializeField] private MenuButtonSegment _settings;
        [SerializeField] private MenuButtonSegment _exit;

        [SerializeField] private Image _selector;

        [SerializeField] private AudioClip _selectSound;
        [SerializeField] UnityEngine.Video.VideoClip videoClip;
        private AudioSource _audioSource;
        private SceneLoader _sceneLoader;

        [Inject]
        public void Constructor(SceneLoader sceneLoader)
        {
            _sceneLoader = sceneLoader;
        }

        private void Awake()
        {
            _audioSource = GetComponent<AudioSource>();
        }

        private void Start()
        {
            _audioSource.clip = _selectSound;
            _selector.DOFade(0, 0);
            AnimateLogo();
        }

        private void OnEnable()
        {
            _start.ButtonPressed += OnStartButtonPressed;
            _start.ButtonMouseEnter += OnButtonSelectEnter;
            _start.ButtonMouseExit += OnButtonSelectExit;

            _settings.ButtonMouseEnter += OnButtonSelectEnter;
            _settings.ButtonMouseExit += OnButtonSelectExit;

            _exit.ButtonMouseEnter += OnButtonSelectEnter;
            _exit.ButtonMouseExit += OnButtonSelectExit;
        }

        private void OnDisable()
        {
            _start.ButtonPressed -= OnStartButtonPressed;
            _start.ButtonMouseEnter -= OnButtonSelectEnter;
            _start.ButtonMouseExit -= OnButtonSelectExit;

            _settings.ButtonMouseEnter -= OnButtonSelectEnter;
            _settings.ButtonMouseExit -= OnButtonSelectExit;

            _exit.ButtonMouseEnter -= OnButtonSelectEnter;
            _exit.ButtonMouseExit -= OnButtonSelectExit;
        }

        private void OnStartButtonPressed()
        {
            transform.parent.gameObject.SetActive(false);
            VideoPlayerController.Instance.PlayClip(videoClip, VideoFinished, Camera.main);
            
        }
        private void VideoFinished()
        {
            _sceneLoader.LoadSceneAsync("Game");
        }
        private void OnButtonSelectEnter(Transform context)
        {
            _selector.transform.DOMove(context.transform.position, 0.2f).OnStart(() =>
            {
                context.DOScale(new Vector3(1.2f, 1.2f, 1f), 0.3f)
                    .SetEase(Ease.InOutBounce);

                _audioSource.PlayOneShot(_selectSound);

                _selector.DOFade(0.25f, 0.5f);
            });
        }

        private void OnButtonSelectExit(Transform context)
        {
            _selector.DOFade(0, 0.2f).OnStart(() =>
            {
                context.DOScale(new Vector3(1f, 1f, 1f), 0.3f)
                    .SetEase(Ease.InOutBounce);
            });
        }

        private void AnimateLogo()
        {
            _logo.DOScale(new Vector3(1.2f, 1.2f, 1f), 3f).SetLoops(-1, LoopType.Yoyo);
        }
    }
}