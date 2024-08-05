using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public GameObject talkPanel;
    public TalkManager talkManager;
    public QuestManager questManager;
    public TextMeshProUGUI talkText;
    public Image portraitImg;
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

        talkPanel.SetActive(isAction);
    }
    void Talk(int id, bool isNpc)
    {
        //set talk data
        int questTalkIndex = questManager.GetQuestTalkIndex(id);
        string talkData = talkManager.GetTalk(id+ questTalkIndex , talkIndex);

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
            talkText.text = talkData.Split(":")[0];
            portraitImg.sprite = talkManager.GetPortrait(id, int.Parse(talkData.Split(":")[1]));
            portraitImg.color = new Color(1, 1, 1, 1);
        }
        else
        {
            talkText.text = talkData;
            portraitImg.color = new Color(1, 1, 1, 0);
        }
        isAction = true;
        talkIndex++;

    }
}
