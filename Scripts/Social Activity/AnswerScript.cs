using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AnswerScript : MonoBehaviour
{
    public bool isCorrect = false;
    public QuizManager quizManager;
    public Color startColor;
    AudioManager audioManager;
    private void Start()
    {
        startColor = GetComponent<Image>().color;
        audioManager = FindObjectOfType<AudioManager>();
    }


    public void Answer()
    {
        if (isCorrect)
        {
            GetComponent<Image>().color = Color.green;
            audioManager.Play("correctOption");
            audioManager.Play("correct_eng");
            StopAllCoroutines();
            StartCoroutine(quizManager.correct());
           
        }
        else
        {
            audioManager.Play("negative");
            audioManager.Play("wrongOption");
            StartCoroutine(colorChange());
        }
    }
    IEnumerator colorChange()
    {
        GetComponent<Image>().color = Color.red;
        yield return new WaitForSeconds(1f);
        GetComponent<Image>().color = startColor;
    }
   /* IEnumerator waitForNext(string s)
    {
        yield return new WaitForSeconds(1f);
        audioManager.Play(s);
    }*/
}
