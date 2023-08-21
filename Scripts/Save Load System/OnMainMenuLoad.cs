using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
public class OnMainMenuLoad : MonoBehaviour
{
    LanguageCheckFromDatabase languageCheckFromDatabase;
    public Slider loadSlider;
    // Start is called before the first frame update
  
    private void Start()
    {
        languageCheckFromDatabase = FindObjectOfType<LanguageCheckFromDatabase>();
        StartCoroutine(CheckIfLanguageHasBeenLoaded());
    }
    
    IEnumerator CheckIfLanguageHasBeenLoaded()
    {
        int slider = 10;
        while (!languageCheckFromDatabase.languageCheckCompleteFlag)
        {

            loadSlider.value = slider + 4f;
            yield return null;
        }
        SceneManager.LoadScene("MainMenu");
    }
    
}
