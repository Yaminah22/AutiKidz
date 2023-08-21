using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
public class OnWorldLoad : MonoBehaviour
{ 
    [SerializeField]
    private GameState beginnerWorldState;
    [SerializeField]
    private GameState hardWorldState;
    SaveManager saveManager;
    public Slider loadSlider;
    
    public void Awake()
    {
        saveManager = FindObjectOfType<SaveManager>();
        //Debug.Log("save Manager found!");
        
    }
    public void Start()
    {
        //Debug.Log("start of loading");
        StartCoroutine(CheckForAllObjectsBeingLoaded());
    }
    public IEnumerator CheckForAllObjectsBeingLoaded()
    {
        while(!saveManager.startCalledFlag)
        {
            yield return null;
        }
        //Debug.Log("Now start of save manager has been called");
        StartCoroutine(saveManager.LoadUserData());
        StartCoroutine(saveManager.LoadPlayerLocation());
        while(!saveManager.positonCoroutineLoadFlag && !saveManager.dataCoroutineLoadFlag)
        {
            yield return null;
        }
        StartCoroutine(CallLoadAsynchronously());
    }
   public IEnumerator CallLoadAsynchronously()
    {
        yield return new WaitForSeconds(0.2f);
        if (beginnerWorldState.GetCurrent() == true)
        {
            StartCoroutine(LoadAsynchronously("Beginner World"));
            //Debug.Log("Beginner World going to load asynchronously");
        }
        else if (hardWorldState.GetCurrent() == true)
        {
            StartCoroutine(LoadAsynchronously("Hard World"));
            //Debug.Log("Hard World going to load asynchronously");
        }
        else Debug.Log("An error in OnWorldLoad");

    }
    public IEnumerator LoadAsynchronously(string sceneName)
    {
        
        AsyncOperation operation = SceneManager.LoadSceneAsync(sceneName);

        while (!operation.isDone)
        {
            float progress = Mathf.Clamp01(operation.progress / 0.9f);
            loadSlider.value = progress;
            yield return null;
        }
        Debug.Log("Load Complete!");
    }
}

