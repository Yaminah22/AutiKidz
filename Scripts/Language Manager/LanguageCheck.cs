using UnityEngine;
using System.Collections;

public class LanguageCheck : MonoBehaviour
{
    [SerializeField]
    private GameConfig configuration;
    private GameObject[] EnglishObjects;
    private GameObject[] UrduObjects;
    public bool languageCheckCompleteFlag;
  
    private void Awake()
    {
        EnglishObjects = GameObject.FindGameObjectsWithTag("English");
        UrduObjects = GameObject.FindGameObjectsWithTag("Urdu");      
        CheckCurrentlySetLanguage();

    }
    public void CheckCurrentlySetLanguage()
    {
        if (configuration.GetLanguage() == "English" )
        {
            foreach (GameObject obj in UrduObjects)
            {
                obj.SetActive(false);
            }
            foreach (GameObject obj in EnglishObjects)
            {
                obj.SetActive(true);
            }

        }

        if (configuration.GetLanguage() == "Urdu")
        {
            foreach (GameObject obj in EnglishObjects)
            {
                obj.SetActive(false);
            }
            foreach (GameObject obj in UrduObjects)
            {
                obj.SetActive(true);
            }
        }
        languageCheckCompleteFlag = true;

    }
}
