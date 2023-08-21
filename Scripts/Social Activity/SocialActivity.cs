using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SocialActivity : MonoBehaviour
{
    public static SocialActivity Instance
    {
        get;
        set;
    }
    public GameObject storyPanel;
    public GameObject quesPanel;
    public GameObject leftPartition;
    public GameObject rightPartition;
    public GameObject middlePartition;
    public Sprite mother;
    public Sprite father;
    public Sprite mosque;
    public Sprite home;
    public Sprite house2;
    public Sprite rightSideBoy;
    public Sprite leftsideBoy;
    public Button soundBtn;
    public Button replayBtn;
    AudioManager audioManager;
    public int conversationPart = 0;
    QuizManager quizManager;
    void Start()
    {
        if (Instance == null) Instance = this;
        conversationPart = 1;
        audioManager = FindObjectOfType<AudioManager>();
        quizManager = FindObjectOfType<QuizManager>();
        leftPartition.SetActive(false);
        rightPartition.SetActive(false);

        StopAllCoroutines();
        StartCoroutine(FirstDialogue());
    }
    private void Update()
    {
        if (quesPanel.activeInHierarchy == true)
        {
            soundBtn.enabled = true;
            replayBtn.enabled = true;
        }
        else
        {
            soundBtn.enabled = false;
            replayBtn.enabled = false;
        }
    }
    public IEnumerator FirstDialogue()
    {
        OpenStoryPanel();
        audioManager.Play("dialogue" + conversationPart.ToString());
        yield return new WaitForSeconds(6f);
        OpenQuesPanel();
    }
    public IEnumerator SecondDialogue()
    {
        OpenStoryPanel();
        audioManager.Play("dialogue" + conversationPart.ToString() + "a");
        yield return new WaitForSeconds(2f);
        rightPartition.GetComponent<Image>().sprite = mother;
        rightPartition.SetActive(true);
        yield return new WaitForSeconds(3f);
        audioManager.Play("dialogue" + conversationPart.ToString() + "b");
        middlePartition.GetComponent<Image>().sprite = leftsideBoy;
        leftPartition.GetComponent<Image>().sprite = father;
        leftPartition.SetActive(true);
        yield return new WaitForSeconds(3f);
        OpenQuesPanel();
    }
    public IEnumerator ThirdDialogue()
    {
        OpenStoryPanel();
        audioManager.Play("dialogue" + conversationPart.ToString());
        middlePartition.GetComponent<Image>().sprite = rightSideBoy;
        rightPartition.GetComponent<Image>().sprite = mosque;
        rightPartition.SetActive(true);
        yield return new WaitForSeconds(3f);
        leftPartition.SetActive(true);
        middlePartition.GetComponent<Image>().sprite = leftsideBoy;
        yield return new WaitForSeconds(2f);
        OpenQuesPanel();
    }
    public IEnumerator FourthDialogue()
    {
        OpenStoryPanel();
        audioManager.Play("dialogue" + conversationPart.ToString());
        middlePartition.GetComponent<Image>().sprite = rightSideBoy;
        rightPartition.GetComponent<Image>().sprite = mother;
        rightPartition.SetActive(true);
        leftPartition.GetComponent<Image>().sprite = house2;
        leftPartition.SetActive(true);
        yield return new WaitForSeconds(3f);
        OpenQuesPanel();
    }
    public void OpenStoryPanel()
    {
        quesPanel.SetActive(false);
        storyPanel.SetActive(true);
    }
    public void OpenQuesPanel()
    {
        rightPartition.SetActive(false);
        leftPartition.SetActive(false);
        storyPanel.SetActive(false);
        quesPanel.SetActive(true);
        quizManager.generateQuestion();
    }
}
