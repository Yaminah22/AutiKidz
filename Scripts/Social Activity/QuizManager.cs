using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;


public class QuizManager : MonoBehaviour
{
    public List<QuestionAnswers> QnA;
    public GameObject[] options;
    public int currentQuestion;
    public TextMeshProUGUI questionText;
    public Image urduQuestionText;
    AudioManager audioManager;
    public Button option1;
    public Button option2;
    WithoutFoxStarScript starScript;
    SocialActivity dialogues;
    public GameConfig config;
    string language;
    private void Start()
    {
        language = config.GetLanguage();
        starScript = FindObjectOfType<WithoutFoxStarScript>();
        audioManager = FindObjectOfType<AudioManager>();
        dialogues = FindObjectOfType<SocialActivity>();
        
    }

    public void SoundBtn()
    {
        audioManager.Play(QnA[currentQuestion].counter.ToString() + "-Ques");
    }
    public void ReplayDialogue()
    {
        StopAllCoroutines();
        switch (dialogues.conversationPart)
        {
            case 1:
                StartCoroutine(dialogues.FirstDialogue());
                break;
            case 2:
                StartCoroutine(dialogues.SecondDialogue());
                break;
            case 3:
                StartCoroutine(dialogues.ThirdDialogue());
                break;
            case 4:
                StartCoroutine(dialogues.FourthDialogue());
                break;
        }
    }
    public IEnumerator correct()
    {
        starScript.progress();
        option1.enabled = false;
        option2.enabled = false;
        dialogues.conversationPart += 1;
        yield return new WaitForSeconds(2f);
        ReplayDialogue();
    }
    void SetAnswers()
    {
        option1.enabled = true;
        option2.enabled = true;
        for (int i = 0; i < options.Length; i++)
        {

            options[i].GetComponent<Image>().color = options[i].GetComponent<AnswerScript>().startColor;
            options[i].GetComponent<AnswerScript>().isCorrect = false;
            options[i].transform.GetChild(0).GetComponent<Image>().sprite = QnA[currentQuestion].Answers[i];
            if (QnA[currentQuestion].CorrectAnswer == i + 1)
            {
                options[i].GetComponent<AnswerScript>().isCorrect = true;
            }
        }
    }

    public void generateQuestion()
    {
        if (QnA.Count > 0)
        {
            currentQuestion = dialogues.conversationPart-1;
            audioManager.Play(QnA[currentQuestion].counter.ToString()+"-Ques");
            if (language == "English")
            {
                questionText.text = QnA[currentQuestion].Question;
            }
            else if (language == "Urdu")
            {
                urduQuestionText.sprite = QnA[currentQuestion].UrduQuestion;
            }
            SetAnswers();
        }
        else
        {
            Debug.Log("GameOver");
        }
        

    }
}
