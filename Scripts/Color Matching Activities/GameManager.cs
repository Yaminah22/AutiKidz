using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.EventSystems;
using System.Linq;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public Question[] questions;
    private static List<Question> unansweredQuestions;
    private Question currentQuestion;
    public TMP_Text engQuestionTxt;
    public Image urduQuestionTxt;
    public Button option1;
    public Button option2;
    public Button option3;
    private string answer;
    private float timeBetweenQuestions = 5f;
    public Animator fox;
    public GameObject picture;
    AudioManager audioManager;
    ScoreManager star;

    public GameConfig config;
    string language;
    void Start()
    {
        language = config.GetLanguage();
        star = ScoreManager.Instance; 
        audioManager = FindObjectOfType<AudioManager>();
        if(unansweredQuestions== null || unansweredQuestions.Count == 0)
        {
            unansweredQuestions = questions.ToList<Question>();
        }
        SetCurrentRandomQuestion();

        option1.onClick.AddListener(showResult);
        option2.onClick.AddListener(showResult);
        option3.onClick.AddListener(showResult);
    }

    void SetCurrentRandomQuestion()
    {
        int randomQuestionIndex = Random.Range(0, unansweredQuestions.Count);
        if (unansweredQuestions == null || unansweredQuestions.Count == 0)
        {
            return;
        }
        currentQuestion = unansweredQuestions[randomQuestionIndex];

        if (language == "English")
        {
            engQuestionTxt.text = currentQuestion.question;
        }
        else if (language == "Urdu")
        {
            urduQuestionTxt.sprite = currentQuestion.urduQuestion;
        }
        
        Scene currentScene = SceneManager.GetActiveScene();

        // Retrieve the name of this scene.
        string sceneName = currentScene.name;

        if (sceneName == "colorMatchingLevel2")
        {
            picture.GetComponent<Image>().sprite = currentQuestion.picture;
        }
        audioManager.Play(currentQuestion.voice);
        answer = currentQuestion.correctAnswer;
        Color color1;
        Color color2;
        Color color3;
        ColorUtility.TryParseHtmlString(currentQuestion.colorButton1, out color1);
        ColorUtility.TryParseHtmlString(currentQuestion.colorButton2, out color2);
        ColorUtility.TryParseHtmlString(currentQuestion.colorButton3, out color3);

        option1.image.color = color1;
        option2.image.color = color2;
        option3.image.color = color3;

    }
    public void SoundButton()
    {
        audioManager.Play(currentQuestion.voice);
    }
    IEnumerator TransitionToNextQuestion()
    {
        unansweredQuestions.Remove(currentQuestion);
        yield return new WaitForSeconds(timeBetweenQuestions);
        option1.enabled = true;
        option2.enabled = true;
        option3.enabled = true;
        SetCurrentRandomQuestion();
    }

    IEnumerator Reset()
    {
        yield return new WaitForSeconds(timeBetweenQuestions);
    }

    IEnumerator wrongOption()
    {
        yield return new WaitForSeconds(timeBetweenQuestions);
        fox.SetBool("wrongOption", false);
    }

    IEnumerator wrongVoice()
    {
        yield return new WaitForSeconds(1f);
        audioManager.Play("wrong");
    }

    IEnumerator correctVoice()
    {
        DisableOptions();
        yield return new WaitForSeconds(1f);
        audioManager.Play(currentQuestion.answer);
    }

    public void DisableOptions() {
        option1.enabled = false;
        option2.enabled = false;
        option3.enabled = false;
    }
    public void showResult()
    {
        if (EventSystem.current.currentSelectedGameObject.name == answer)
        {
            audioManager.Play("correct");
            StopAllCoroutines();
            StartCoroutine(correctVoice());
            star.progress();
            StartCoroutine(TransitionToNextQuestion());
        }
        else
        {
            fox.SetBool("wrongOption", true);
            StopAllCoroutines();
            StartCoroutine(wrongOption());
            audioManager.Play("incorrect");
            StartCoroutine(wrongVoice());
            StartCoroutine(Reset());
        }
    }
}
