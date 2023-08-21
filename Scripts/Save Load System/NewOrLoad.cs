using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class NewOrLoad : MonoBehaviour
{
    SaveManager saveManager;
    private void Start()
    {
        saveManager = FindObjectOfType<SaveManager>();
    }
    public void NewGame()
    {
        StartCoroutine(StartNewGame());
    }
    public IEnumerator StartNewGame()
    {
        while (!saveManager.startCalledFlag)
        {
            yield return null;
        }
        StartCoroutine(saveManager.AddChildNametoDB());
        StartCoroutine(saveManager.AddEmailtoDB());
        //StartCoroutine(saveManager.AddDatetoDB());
        StartCoroutine(saveManager.AddLanguageToDB());
        StartCoroutine(saveManager.AddProgressFields());
        SceneManager.LoadScene("LoadFromMainMenu");
    }
    public void LoadProgress()
    {
       /* StartCoroutine(saveManager.LoadPlayerLocation());
        StartCoroutine(saveManager.LoadUserData());
*/
        SceneManager.LoadScene("LoadFromMainMenu");
    }
}
