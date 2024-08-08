using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public Animator talkPanel;
    public TalkManager talkManager;
    public QuestManager questManager;
    public TypeEffect talkText;
    public Image portraitImg;
    public Animator portraitAnim;
    public Sprite prevPortrait;
    public GameObject scanObject;
    public bool isAction;
    public int talkIndex;

   void Start()
    {
        Debug.Log(questManager.CheckQuest());   
    }

    public void Action(GameObject scanObj)
    {

        isAction = true;
        scanObject = scanObj;
        ObjectData objDate = scanObject.GetComponent<ObjectData>();

        Talk(objDate.id, objDate.isNpc);

        talkPanel.SetBool("isShow",isAction);
    }
    void Talk(int id, bool isNpc)
    {
        int questTalkIndex = 0;
        string talkData = "";
        //set talk data
        if (talkText.isAnim)
        {
            talkText.SetMsg("");
            return;
        }
        else
        {
             questTalkIndex = questManager.GetQuestTalkIndex(id);
             talkData = talkManager.GetTalk(id+ questTalkIndex , talkIndex);
        }

        //end talk
        if (talkData == null)
        {
            isAction = false;
            talkIndex = 0;
            Debug.Log( questManager.CheckQuest(id));
            return;
        }

        //continue talk
        if (isNpc)
        {
            talkText.SetMsg(talkData.Split(":")[0]);
            portraitImg.sprite = talkManager.GetPortrait(id, int.Parse(talkData.Split(":")[1]));
            portraitImg.color = new Color(1, 1, 1, 1);
            //Animation Portrait
            if(prevPortrait != portraitImg.sprite)
            {
                portraitAnim.SetTrigger("doEffect");
                prevPortrait = portraitImg.sprite;
            }
        }
        else
        {
            talkText.SetMsg(talkData);
            portraitImg.color = new Color(1, 1, 1, 0);
        }
        isAction = true;
        talkIndex++;

    }
}
