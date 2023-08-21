using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class playscript : MonoBehaviour
{
    AudioManager audiomanager;
    public string scene;
    // Start is called before the first frame update
    public void Start()
    {
        audiomanager = FindObjectOfType<AudioManager>();
        audiomanager.Play("bg");
        
    }
    public void changescene() {
        SceneManager.LoadScene(scene);
    }
}
