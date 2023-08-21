using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Audio : MonoBehaviour
{
    bool produce_tracing_sound;
    public AudioClip clip_complete;
    bool produce_on_complete_sound;
    public bool tracing_two;
    public GameObject ball;
    AudioManager audiomanager;
    private void Start()
    {
        audiomanager = FindObjectOfType<AudioManager>();
    }
    void Update()

    {
        //Debug.Log(tracing_two);
        produce_tracing_sound = gameObject.GetComponent<Tracing>().Tracing_begin;
        produce_on_complete_sound = gameObject.GetComponent<Tracing>().tracing_complete;

        if (produce_tracing_sound)
        {
           audiomanager.Play("Tracing");

        }
       
        else if (produce_on_complete_sound && tracing_two!=true) {
            audiomanager.Play("OneBall");
            ball.GetComponent<Waypoints>().waypointIndex = 0;
        }
        else if (produce_on_complete_sound && tracing_two==true)
        {audiomanager.Play("TwoBalls");
        }
    }
    public void tracefollowing() {
        audiomanager.Play("TraceFollowing");
    }
}
