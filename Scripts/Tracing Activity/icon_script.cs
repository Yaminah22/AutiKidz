using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class icon_script : MonoBehaviour
{
    // Start is called before the first frame update
    public GameObject star;
    public GameObject FilledStar;
    void Update()
    {
        SpriteRenderer star_parent_sprite = star.transform.parent.gameObject.GetComponent<SpriteRenderer>();
        
        if (star_parent_sprite.color == Color.yellow) {
            FilledStar.GetComponent<ScoreManager>().progress();
            Destroy(star);
        }
    }
}
