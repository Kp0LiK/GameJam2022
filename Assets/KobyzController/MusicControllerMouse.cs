using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MusicControllerMouse : MonoBehaviour
{
    public Image scaleImg;
    AudioSource audio;
    [SerializeField] AudioSource complete;
    public MusicActionM shield;
    public MusicActionM atack;
    public MusicActionM atack2;
    public MusicActionTypeM shieldType;
    public MusicActionTypeM atackType;

    [SerializeField] ParticleSystem particle1;
    [SerializeField] ParticleSystem particle2;
    [SerializeField] Animator anim;
    float kobyzForwardTime = 0.708f;
    float kobyzBackTime = 0.667f;
    // Start is called before the first frame update
    void Start()
    {
        audio = GetComponent<AudioSource>();
    }

    bool buttonPressed;
    float holdTimer;
    [SerializeField] float threshhold = 0.3f;
    int index;
    MusicActionM currentAction;
    MusicActionTypeM currentActionType;
    // Update is called once per frame
    void Update()
    {
        
        if (!buttonPressed && Input.GetKeyDown(KeyCode.E))
        {
            buttonPressed = true;
            index = 0;
            holdTimer = 0f; 
            scaleImg.fillAmount = 0f;
            //currentAction = atack;
            currentActionType = atackType;
            anim.SetTrigger("KobyzReady");
        }
        if (buttonPressed && Input.GetKeyUp(KeyCode.E))
        {
            buttonPressed = false;
            anim.SetTrigger("Idle");
        }
        if (!buttonPressed && Input.GetKeyDown(KeyCode.Q))
        {
            buttonPressed = true;
            index = 0;
            holdTimer = 0f;
            scaleImg.fillAmount = 0f;
            //currentAction = shield;
            currentActionType = shieldType;
            anim.SetTrigger("KobyzReady");
        }
        if (buttonPressed && Input.GetKeyUp(KeyCode.Q))
        {
            buttonPressed = false;
            anim.SetTrigger("Idle");
        }
        if (buttonPressed && index == 0)
        {
            foreach(MusicActionM action in currentActionType.actions)
            {
                if (Input.GetMouseButtonDown(action.notes[index].key))
                {
                    currentAction = action;
                    audio.clip = currentAction.clip;
                    audio.Play();
                    particle1.Play(false);
                    anim.speed = kobyzForwardTime / currentAction.notes[index].duration;
                    anim.SetTrigger("KobyzF");
                    break;
                }
            }
        }
        if (buttonPressed && currentAction != null)
        {
            if (buttonPressed && Input.GetMouseButton(currentAction.notes[index].key))
            {
                holdTimer += Time.deltaTime;
                scaleImg.fillAmount = holdTimer / currentAction.notes[index].duration;
                if (holdTimer > currentAction.notes[index].duration)
                {
                    if (index == currentAction.notes.Length - 1)
                        CastSuccess();
                    else
                        CastFail();
                }

            }
            if (buttonPressed && Input.GetMouseButtonUp(currentAction.notes[index].key))
            {
                if (Mathf.Abs(holdTimer - currentAction.notes[index].duration) < threshhold)
                {
                    index++;
                    holdTimer = 0f;
                    scaleImg.fillAmount = 0f;
                    Debug.Log("Success!!!");
                    if (index >= currentAction.notes.Length)
                    {
                        CastSuccess();
                    }
                    else
                    {
                        anim.speed = (index % 2 > 0 ? kobyzBackTime : kobyzForwardTime) / currentAction.notes[index].duration;
                        anim.SetTrigger(index % 2 > 0 ? "KobyzB" : "KobyzF");
                    }
                }
                else
                {
                    CastFail();
                }
            }
        } 
    }
    private void CastSuccess()
    {
        complete.Play();
        particle2.Play(true);
        ClearValues();
    }
    private void CastFail()
    {
        Debug.Log("Fail");
        audio.Stop();
        ClearValues();       
    }
    private void ClearValues()
    {
        particle1.Stop(false);
        buttonPressed = false;
        anim.SetTrigger("Idle");
        anim.speed = 1f;
        scaleImg.fillAmount = 0f;
    }
    
    [System.Serializable]
    public class NoteM {
        public int key;
        public float duration;
    }
    [System.Serializable]
    public class MusicActionM
    {
        public AudioClip clip;
        public NoteM[] notes;
    }
    [System.Serializable]
    public class MusicActionTypeM
    {
        public MusicActionM[] actions;
    }
}
