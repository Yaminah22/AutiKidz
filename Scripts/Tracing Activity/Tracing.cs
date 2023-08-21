using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class Tracing : MonoBehaviour
{

    public LayerMask Raycast_layer;
    public Camera Cam;
    string Sprite_name;
    int Sprite_number;
    public bool Tracing_begin;
    public int total_sprites;
    public bool tracing_complete;
   AudioManager audiomanager;
    GameObject tracing;

    private void Start()
    {
        audiomanager = FindObjectOfType<AudioManager>();
        Tracing_begin = false;
        tracing_complete = false;
        audiomanager.Play("TraceFollowing");
    }
    //Sound button functionality to be implemented in every activity
    public void SoundButton()
    {
        audiomanager.Play("TraceFollowing");
    }

    void Update()
    {
        tracing = GameObject.Find("tracing");
        total_sprites = tracing.transform.childCount - 1;
        Tracing_begin = false;
        tracing_complete = false;
        var ray = Cam.ScreenToWorldPoint(Input.mousePosition);
        RaycastHit2D hit = Physics2D.Raycast(ray, -Vector2.up);
        GameObject hand = GameObject.Find("clicking");

        if (hit.collider != null)
        {
            SpriteRenderer sp = hit.collider.gameObject.GetComponent<SpriteRenderer>();
            Sprite_name = hit.collider.name;
            if (Sprite_name == "1" && sp.color !=Color.yellow)
            {
                sp.color = Color.yellow;
                Tracing_begin = true;
                Destroy(hand);

            }
            else if (sp.color == Color.yellow) {
                return;
            }

            else
            {
                Sprite_number = Convert.ToInt32(hit.collider.name);

                for (int i = (Sprite_number - 1); i > 0; i--)
                {
                    SpriteRenderer sp2 = GameObject.Find(i.ToString()).gameObject.GetComponent<SpriteRenderer>();
                    //bug.Log(sp2.color);
                    if (sp2.color != Color.yellow)
                    {
                        break;
                    }
                    else {
                        if (sp.name != total_sprites.ToString())
                        {
                            Tracing_begin = true;
                        }
                        else { tracing_complete = true; }
                        sp.color = Color.yellow;
                    }
                }
            }
        }

    }
}
