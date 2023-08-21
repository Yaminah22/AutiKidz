using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class WithoutFoxStarScript : MonoBehaviour
{
    //Creating Fade aimation for central star appearing and disappearing
    public static WithoutFoxStarScript Instance
    {
        set;
        get;
    }
    public Image starResult; //star that will fade in and fade out
    private bool isInTransition;
    private float transition;
    private bool isShowing;
    private float duration;
    public Animator fadeImageAnimator; //animator for movement across the screen of central star
    AudioManager audiomanager;
    public GameObject confetti;
    //For displaying level end animation
    public GameObject completeLevelUI;
    public GameObject topbar;
    public GameObject body;
    public LevelCompletionStatus levelComplete;
    // Logic for stars filling in star bar
    public Animator[] star;
    //For keeping track of star to be filled
    int count = 0;
    private void Start()
    {
        audiomanager = FindObjectOfType<AudioManager>();
    }
    void Awake()
    {
        Instance = this;
    }

    // Fade IN OUT BASE function
    public void Fade(bool showing, float duration)
    {
        isShowing = showing;
        isInTransition = true;
        this.duration = duration;
        transition = (isShowing) ? 0 : 1;
        StopAllCoroutines();
        StartCoroutine(animating(showing)); //A coroutine for showing animation
    }
    // Star Movement Function
    IEnumerator animating(bool showing)
    {
        if (showing == true)
        {
            
            //start fox movement

            //yield return new WaitForSeconds(0.7f);
            //wait for fade-in animation to end
            fadeImageAnimator.SetBool("isStationery", false);
            //movement animation starts
            audiomanager.Play("StarMovement");
            yield return new WaitForSeconds(0.7f);// wait for movement to end
            Fade(false, 0.01f);// fade-out animation

            ProgressStars(count);// Set stars in bar according to progress
            fadeImageAnimator.SetBool("isStationery", true);// reset star position

           
            if (count == 4)
            {
                StopAllCoroutines();
                StartCoroutine(LevelEnd());
            }
        }
    }
    // Stars Bar Function
    void ProgressStars(int progress_score)
    {
        audiomanager.Play("StarFill");
        for (int i = 0; i < progress_score; i++)
        {
            //Debug.Log(i+ " "+progress_score);
            if (star[i].GetBool("filled") != true)
                star[i].SetBool("filled", true);
        }

    }

    //function to display level complete screen
    IEnumerator LevelEnd()
    {
        yield return new WaitForSeconds(1f);
        topbar.SetActive(false);
        body.SetActive(false);
        completeLevelUI.SetActive(true);
        audiomanager.Play("lvlComplete");
        confetti.SetActive(true);
        confetti.GetComponent<ParticleSystem>().Play();
        yield return new WaitForSeconds(4f);
        levelComplete.CompleteLevel();
    }
    //Function which updates the progress
    public void progress()
    {
        Fade(true, 0.5f); //calling fade-in animation
        count += 1;
    }

    void Update()
     {

         // fade animation creation script
         if (!isInTransition) return;
         transition += (isShowing) ? Time.deltaTime * (1 / duration) : -Time.deltaTime * (1 / duration);
         starResult.color = Color.Lerp(new Color(1, 1, 1, 0), Color.white, transition);
         if (transition > 1 || transition < 0) isInTransition = false;
     }

}