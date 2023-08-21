using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using TMPro;

public class LoadingScript : MonoBehaviour
{
   
    public string PortalSceneName;
    public Slider loadSlider;
    public TMP_Text progressPercent;
    public GameState hardWorldGameState;
    public void Start()
    {
        hardWorldGameState.UpdateRespawnLocation(new Vector3(52.43f, 0, 6.8f));
        StartCoroutine(LoadAsynchronously());
        
    }
    IEnumerator LoadAsynchronously()
    {
        yield return new WaitForSeconds(0.5f);
        AsyncOperation operation = SceneManager.LoadSceneAsync(PortalSceneName.ToString());

        while (!operation.isDone)
        {
            float progress = Mathf.Clamp01(operation.progress / 0.9f);
            loadSlider.value = progress;
            /*progressPercent.text = progress * 100f + "%";*/
            yield return null;
        }

    }
}

