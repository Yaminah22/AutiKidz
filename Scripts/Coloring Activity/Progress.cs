using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Progress : MonoBehaviour
{
    public GameObject sprite;
    public GameObject color_script;
    ScoreManager scoreManager;
    int total_sprites;
    // Start is called before the first frame update
    int count;
    GameObject[] all_childs;
    private void Start()
    {
        scoreManager = ScoreManager.Instance;
    }
    void Update()
    {
        SpriteRenderer parent = sprite.transform.parent.gameObject.GetComponent<SpriteRenderer>();
       
        if (parent.color != Color.white)
        {
            all_childs = GameObject.FindGameObjectsWithTag(sprite.name);
            foreach(GameObject g in all_childs)
            {
                Destroy(g);
            }
            count += 1;
            call_progress();
        }
    }
    void call_progress()
    {
        if (count == 1) {
            scoreManager.progress(); }
            if (count == Mathf.Ceil(total_sprites / 4)) {
                scoreManager.progress();
            }
            if (count == Mathf.Ceil(total_sprites / 2))
                { scoreManager.progress(); }
            if (count == total_sprites || count==total_sprites-1|| count==total_sprites-2)
                { scoreManager.progress(); }
        }
    

}
