using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class LanguageCheckFromDatabase : MonoBehaviour
{
    [SerializeField]
    private GameConfig configuration;
    SaveManager saveManager;

    public bool languageCheckCompleteFlag;
    
    void Start()
    {
        saveManager = FindObjectOfType<SaveManager>();
        StartCoroutine(LoadCurrentLanguage());
    }
    public IEnumerator LoadCurrentLanguage()
    {
        while (!saveManager.startCalledFlag)
        {
            yield return null;
        }
        StartCoroutine(saveManager.LoadLanguage());
        while (!saveManager.loadLanguageFlag) yield return null;
        languageCheckCompleteFlag = true;
    }
   
}
