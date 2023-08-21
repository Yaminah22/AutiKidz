using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class EndGame : MonoBehaviour
{
    public GameObject confetti1;
    public GameObject confetti2;
    AudioManager audioManager;
    // Start is called before the first frame update
    void Start()
    {
        audioManager = FindObjectOfType<AudioManager>();
        StartCoroutine(PlayConfetti());
    }
    IEnumerator PlayConfetti()
    {
        yield return new WaitForSeconds(0.1f);
        confetti1.GetComponent<ParticleSystem>().Play();
        confetti2.GetComponent<ParticleSystem>().Play();
        audioManager.Play("congrats");
        audioManager.Play("bg");
    }
    public void OpenMainMenu()
    {
        SceneManager.LoadScene("MainMenu");
    }
    public void Exit()
    {
        Application.Quit();
    }
    // Update is called once per frame
    
}
