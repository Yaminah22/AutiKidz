
using UnityEngine;

public class Color_script : MonoBehaviour
{public LayerMask Raycast_layer;
    public Camera Cam;
    public Color[] colorList;
    public Color curColor;
    public int colorCount;
    string spritename;
    AudioManager audiomanager;
    GameObject [] Instances;
    public GameObject Star;

    private void Start()
    {
        audiomanager = FindObjectOfType<AudioManager>();
        audiomanager.Play("ColorTheObject");
    }
    public void SoundButton()
    {
        audiomanager.Play("ColorTheObject");
    }
    void Update()
    {
        curColor = colorList[colorCount];
        
        var ray = Cam.ScreenToWorldPoint(Input.mousePosition);
        RaycastHit2D hit = Physics2D.Raycast(ray, -Vector2.up);
        //GameObject hand = GameObject.Find("clicking");
       


        if (hit.collider != null)
        {
            Instances = GameObject.FindGameObjectsWithTag(hit.collider.name);
            foreach (GameObject g in Instances) {
                SpriteRenderer sp = g.GetComponent<SpriteRenderer>();
                curColor.a = 1f;
                sp.color = curColor;
            }
        }
            
               
                
            }


    public void paint(int colorCode) {
        colorCount = colorCode;
        if (colorCode == 1) {
            audiomanager.Play("Red");
        }
        if (colorCode == 2)
        {
            audiomanager.Play("Blue");
        }
        if (colorCode == 3)
        {
            audiomanager.Play("Yellow");
        }
    }
}
