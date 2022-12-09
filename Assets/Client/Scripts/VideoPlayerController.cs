using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Video;
public class VideoPlayerController : MonoBehaviour
{
    public static VideoPlayerController Instance
    {
        get;
        private set;
    }
    
    void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
        }
        else
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
    }
    [SerializeField] VideoPlayer videoPlayer;
    [SerializeField] GameObject againTxt;
    bool teaserDone;
    private System.Action callback; 
    // Start is called before the first frame update
    void Start()
    {
        callback = LoadMenu;
        DontDestroyOnLoad(gameObject);    
    }
    
    int countPressed;
    // Update is called once per frame
    float timer;
    void Update()
    {
        if (videoPlayer.isPlaying && Input.anyKeyDown)
        {
            countPressed++;
            if (countPressed == 1)
            {
                againTxt.SetActive(true);                
            }
            if (countPressed >= 2)
            {
                videoPlayer.Stop();
                OnMovieFinished();
            }
                   
        }
        if(countPressed == 1)
        {
            timer += Time.deltaTime;
            if (timer > 2f)
            {
                againTxt.SetActive(false);
                countPressed = 0;
                timer = 0f;
            }
        }
        if (videoPlayer.isPlaying && videoPlayer.frame + 10 >= (long) videoPlayer.frameCount)
        {
            OnMovieFinished();
        }
        Debug.Log(videoPlayer.frame + " " + videoPlayer.frameCount);
    }
    public void PlayClip(VideoClip clip, System.Action call, Camera cam)
    {
        callback = call;
        videoPlayer.targetCamera = cam;
        videoPlayer.clip = clip;

    }
    private void LoadMenu()
    {
        UnityEngine.SceneManagement.SceneManager.LoadScene("Menu");
    }
    private void OnMovieFinished()
    {
        videoPlayer.Stop();
        videoPlayer.clip = null;
        callback();
        callback = null;
        againTxt.SetActive(false);
    }

}
