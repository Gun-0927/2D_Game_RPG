using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class TypeEffect : MonoBehaviour
{
    public int CharPerSecond;
    public GameObject EndCursor;
    
    string targetMsg;
    TextMeshProUGUI msgText;
    AudioSource audioSource;
    int index;
    float interval;
    public bool isAnim;

    void Awake()
    {
        msgText = GetComponent<TextMeshProUGUI>();
        audioSource = GetComponent<AudioSource>();
    }


    public void SetMsg(string msg)
    {
        if (isAnim)
        {
           msgText.text = targetMsg;
           CancelInvoke();
           EffectEnd();
        }
        else
        {
            targetMsg = msg;
            EffectStart();
        }
    }

    void EffectStart()
    {
        msgText.text = "";
        index = 0;
        EndCursor.SetActive(false);

        //start animation
        interval = 1.0f / CharPerSecond;

        isAnim = true;

        Invoke("Effecting", interval);
    }   
    void Effecting()
    {
        if(msgText.text == targetMsg)
        {
            EffectEnd();
            return;
        }
        //sound
        if (targetMsg[index] != ' ' || targetMsg[index] != '.')
        {
            audioSource.Play();

        }

        msgText.text += targetMsg[index];
        index++;

        Invoke("Effecting", interval);
    }  
    void EffectEnd()
    {
        isAnim = false;
        EndCursor.SetActive(true);
    }
}
