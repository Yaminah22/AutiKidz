using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using Firebase.Database;
using Firebase.Auth;
using TMPro;
using System;

public class SaveManager : MonoBehaviour
{
    [SerializeField]
    private GameState beginnerGameState;
    [SerializeField]
    private GameState hardGameState;
    [SerializeField]
    private GameConfig gameConfig;
    [SerializeField]
    private Levels matching;
    [SerializeField]
    private Levels coloring;
    [SerializeField]
    private Levels memoryRetention;
    [SerializeField]
    private Levels sorting;
    [SerializeField]
    private Levels puzzle;
    [SerializeField]
    private Levels social;
    [SerializeField]
    private Levels tracing;
    public bool positonCoroutineLoadFlag;
    public bool dataCoroutineLoadFlag;
    public bool startCalledFlag;
    public bool loadLanguageFlag;
    /* public OnWorldLoad onWorldLoad;*/


    //Firebase variables
    [Header("Firebase")]
    private DatabaseReference DBreference;
    private FirebaseAuth auth;


    void Start()
    {
        // Get the root reference location of the database.
        DBreference = FirebaseDatabase.DefaultInstance.RootReference;
        auth = FirebaseAuth.DefaultInstance;
        startCalledFlag = true;
        /*ShowMessage();
        StartCoroutine(LoadUserData());*/
       
    }


    public void SignOut()
    {
        auth.SignOut();
        Debug.Log("User signed out successfully.");
        Application.Quit();
    }
    public void SavePosition(Vector3 transform, string name)
    {
        StartCoroutine(UpadatePlayerTransform(transform, name));
    }
    public void SaveLanguage(string name)
    {
        StartCoroutine(UpdateLanguageConfig(name));

    }
    public IEnumerator UpdateLanguageConfig(string languageName)
    {
        var DBTask = DBreference.Child("users").Child(References.ReferencedUser.UserId).Child("language").SetValueAsync(languageName);

        yield return new WaitUntil(predicate: () => DBTask.IsCompleted);

        if (DBTask.Exception != null)
        {
            Debug.LogWarning(message: $"Failed to register task with {DBTask.Exception}");
        }
    }
    public void SaveData(string name, int lvlID)
    {
        /*StartCoroutine(AddChildNametoDB());
        StartCoroutine(AddEmailtoDB());*/
        StartCoroutine(AddDatetoDB());
        StartCoroutine(UpdateActivityLevel(name, lvlID));
    }
    public IEnumerator UpadatePlayerTransform(Vector3 transform, string name)
    {
        var DBTask1 = DBreference.Child("users").Child(References.ReferencedUser.UserId).Child("progress").Child("Transform").SetValueAsync(transform.ToString());
        var DBTask2= DBreference.Child("users").Child(References.ReferencedUser.UserId).Child("progress").Child("World Name").SetValueAsync(name);

        yield return new WaitUntil(predicate: () => DBTask1.IsCompleted);
        yield return new WaitUntil(predicate: () => DBTask2.IsCompleted);
        if (DBTask1.Exception != null)
        {
            Debug.LogWarning(message: $"Failed to register task with {DBTask1.Exception}");
        }
        else if (DBTask2.Exception != null)
        {
            Debug.LogWarning(message: $"Failed to register task with {DBTask2.Exception}");
        }
        else
        {
            Debug.Log("Successfully Saved!");
        }
    }
    public IEnumerator AddEmailtoDB()
    {
        //Set the currently logged in user email in the database
        var DBTask = DBreference.Child("users").Child(References.ReferencedUser.UserId).Child("email").SetValueAsync(References.ReferencedUser.Email);

        yield return new WaitUntil(predicate: () => DBTask.IsCompleted);

        if (DBTask.Exception != null)
        {
            Debug.LogWarning(message: $"Failed to register task with {DBTask.Exception}");
        }
    }
    public IEnumerator AddLanguageToDB()
    {
        var DBTask = DBreference.Child("users").Child(References.ReferencedUser.UserId).Child("language").SetValueAsync(gameConfig.GetLanguage());

        yield return new WaitUntil(predicate: () => DBTask.IsCompleted);

        if (DBTask.Exception != null)
        {
            Debug.LogWarning(message: $"Failed to register task with {DBTask.Exception}");
        }
    }
    public IEnumerator AddDatetoDB()
    {
        //Set the currently logged in user date in the database
        var DBTask = DBreference.Child("users").Child(References.ReferencedUser.UserId).Child("date").SetValueAsync(DateTime.Now.ToString("MM/dd/yyyy"));

        yield return new WaitUntil(predicate: () => DBTask.IsCompleted);

        if (DBTask.Exception != null)
        {
            Debug.LogWarning(message: $"Failed to register task with {DBTask.Exception}");
        }
    }

    public IEnumerator AddChildNametoDB()
    {
        //Set the currently logged in user username in the database
        var DBTask = DBreference.Child("users").Child(References.ReferencedUser.UserId).Child("child's name").SetValueAsync(References.ReferencedUser.DisplayName);

        yield return new WaitUntil(predicate: () => DBTask.IsCompleted);

        if (DBTask.Exception != null)
        {
            Debug.LogWarning(message: $"Failed to register task with {DBTask.Exception}");
        }
    }
    public IEnumerator AddProgressFields()
    {
        var DBTask1 = DBreference.Child("users").Child(References.ReferencedUser.UserId).Child("progress").Child("Color Matching").SetValueAsync(0);
        var DBTask2 = DBreference.Child("users").Child(References.ReferencedUser.UserId).Child("progress").Child("Coloring").SetValueAsync(0);
        var DBTask3 = DBreference.Child("users").Child(References.ReferencedUser.UserId).Child("progress").Child("Sorting").SetValueAsync(0);
        var DBTask4 = DBreference.Child("users").Child(References.ReferencedUser.UserId).Child("progress").Child("Puzzle").SetValueAsync(0);
        var DBTask5 = DBreference.Child("users").Child(References.ReferencedUser.UserId).Child("progress").Child("Memory Retention").SetValueAsync(0);
        var DBTask6 = DBreference.Child("users").Child(References.ReferencedUser.UserId).Child("progress").Child("Social Activity").SetValueAsync(0);
        var DBTask7 = DBreference.Child("users").Child(References.ReferencedUser.UserId).Child("progress").Child("Tracing Activity").SetValueAsync(0);
        var DBTask8 = DBreference.Child("users").Child(References.ReferencedUser.UserId).Child("progress").Child("Transform").SetValueAsync("");
        var DBTask9 = DBreference.Child("users").Child(References.ReferencedUser.UserId).Child("progress").Child("World Name").SetValueAsync("Beginner");

        yield return new WaitUntil(predicate: () => DBTask1.IsCompleted);

        if (DBTask1.Exception != null)
        {
            Debug.LogWarning(message: $"Failed to register task with {DBTask1.Exception}");
        }
        yield return new WaitUntil(predicate: () => DBTask2.IsCompleted);

        if (DBTask2.Exception != null)
        {
            Debug.LogWarning(message: $"Failed to register task with {DBTask2.Exception}");
        }
        yield return new WaitUntil(predicate: () => DBTask3.IsCompleted);

        if (DBTask3.Exception != null)
        {
            Debug.LogWarning(message: $"Failed to register task with {DBTask3.Exception}");
        }
        yield return new WaitUntil(predicate: () => DBTask4.IsCompleted);

        if (DBTask4.Exception != null)
        {
            Debug.LogWarning(message: $"Failed to register task with {DBTask4.Exception}");
        }
        yield return new WaitUntil(predicate: () => DBTask5.IsCompleted);

        if (DBTask5.Exception != null)
        {
            Debug.LogWarning(message: $"Failed to register task with {DBTask5.Exception}");
        }
        yield return new WaitUntil(predicate: () => DBTask6.IsCompleted);

        if (DBTask6.Exception != null)
        {
            Debug.LogWarning(message: $"Failed to register task with {DBTask6.Exception}");
        }
        yield return new WaitUntil(predicate: () => DBTask7.IsCompleted);

        if (DBTask7.Exception != null)
        {
            Debug.LogWarning(message: $"Failed to register task with {DBTask7.Exception}");
        }
        yield return new WaitUntil(predicate: () => DBTask8.IsCompleted);

        if (DBTask8.Exception != null)
        {
            Debug.LogWarning(message: $"Failed to register task with {DBTask8.Exception}");
        }
        yield return new WaitUntil(predicate: () => DBTask9.IsCompleted);

        if (DBTask9.Exception != null)
        {
            Debug.LogWarning(message: $"Failed to register task with {DBTask9.Exception}");
        }
    }
    private IEnumerator UpdateActivityLevel(string activityName, int level)
    {
        //Set the currently logged in user activity level
        var DBTask = DBreference.Child("users").Child(References.ReferencedUser.UserId).Child("progress").Child(activityName).SetValueAsync(level);

        yield return new WaitUntil(predicate: () => DBTask.IsCompleted);

        if (DBTask.Exception != null)
        {
            Debug.LogWarning(message: $"Failed to register task with {DBTask.Exception}");
        }
    }

    public IEnumerator LoadPlayerLocation()
    {
        var DBTask = DBreference.Child("users").Child(References.ReferencedUser.UserId).GetValueAsync();

        yield return new WaitUntil(predicate: () => DBTask.IsCompleted);

        if (DBTask.Exception != null)
        {
            Debug.LogWarning(message: $"Failed to register task with {DBTask.Exception}");
        }
        else if (DBTask.Result.Child("progress").Child("Transform").Value.ToString() == "")
        {
            beginnerGameState.SetCurrent(true);
            beginnerGameState.UpdateRespawnLocation(new Vector3(52.43f, 0,11.6f));
        }
        else
        {
            DataSnapshot snapshot = DBTask.Result;
            string position= snapshot.Child("progress").Child("Transform").Value.ToString();
            string world = snapshot.Child("progress").Child("World Name").Value.ToString();
            Vector3 vector3 = StringToVector3(position);
            Debug.Log(world + " " + vector3);
            if (world == "Beginner")
            {
                beginnerGameState.SetCurrent(true);
                hardGameState.SetCurrent(false);
                beginnerGameState.UpdateRespawnLocation(vector3);
                /*StartCoroutine(onWorldLoad.LoadAsynchronously("Beginner World"));*/
                Debug.Log(vector3 + " " + world);
            }
            else if (world == "Hard")
            {
                hardGameState.SetCurrent(true);
                beginnerGameState.SetCurrent(false);
                hardGameState.UpdateRespawnLocation(vector3);
                /*StartCoroutine(onWorldLoad.LoadAsynchronously("Hard World"));*/
                Debug.Log(vector3 + " " + world);
            }
            else
            {
                Debug.Log(world + " " + vector3);
            }
        }
        positonCoroutineLoadFlag = true;
    }
    public static Vector3 StringToVector3(string sVector)
    {
        // Remove the parentheses
        if (sVector.StartsWith("(") && sVector.EndsWith(")"))
        {
            sVector = sVector.Substring(1, sVector.Length - 2);
        }

        // split the items
        string[] sArray = sVector.Split(',');

        // store as a Vector3
        Vector3 result = new Vector3(
            float.Parse(sArray[0]),
            float.Parse(sArray[1]),
            float.Parse(sArray[2]));

        return result;
    }
    public IEnumerator LoadUserData()
    {
        //Get the currently logged in user data
        var DBTask = DBreference.Child("users").Child(References.ReferencedUser.UserId).GetValueAsync();

        yield return new WaitUntil(predicate: () => DBTask.IsCompleted);

        if (DBTask.Exception != null)
        {
            Debug.LogWarning(message: $"Failed to register task with {DBTask.Exception}");
        }
        else
        {
            //Data has been retrieved
            DataSnapshot snapshot = DBTask.Result;
            for(int i=0;i<Convert.ToInt32(snapshot.Child("progress").Child("Color Matching").Value); i++)
            {
                matching.lvls[i].isCompleted = true;
            }
            for (int i = 0; i < Convert.ToInt32(snapshot.Child("progress").Child("Coloring").Value); i++)
            {
                coloring.lvls[i].isCompleted = true;
            }
            for (int i = 0; i < Convert.ToInt32(snapshot.Child("progress").Child("Memory Retention").Value); i++)
            {
                memoryRetention.lvls[i].isCompleted = true;
            }
            for (int i = 0; i < Convert.ToInt32(snapshot.Child("progress").Child("Puzzle").Value); i++)
            {
                puzzle.lvls[i].isCompleted = true;
            }
            for (int i = 0; i < Convert.ToInt32(snapshot.Child("progress").Child("Social Activity").Value); i++)
            {
                social.lvls[i].isCompleted = true;
            }
            for (int i = 0; i < Convert.ToInt32(snapshot.Child("progress").Child("Sorting").Value); i++)
            {
                sorting.lvls[i].isCompleted = true;
            }
            for (int i = 0; i < Convert.ToInt32(snapshot.Child("progress").Child("Tracing Activity").Value); i++)
            {
                tracing.lvls[i].isCompleted = true;
            }
        }
        dataCoroutineLoadFlag = true;
    }
    public IEnumerator LoadLanguage()
    {
        var DBTask = DBreference.Child("users").Child(References.ReferencedUser.UserId).GetValueAsync();
        Debug.Log("language is loaded");
        yield return new WaitUntil(predicate: () => DBTask.IsCompleted);

        if (DBTask.Exception != null)
        {
            Debug.LogWarning(message: $"Failed to register task with {DBTask.Exception}");
        }
        else if (DBTask.Result.HasChild("language"))
        {
            DataSnapshot snapshot = DBTask.Result;
            gameConfig.SetLanguage(snapshot.Child("language").Value.ToString());
            Debug.Log(snapshot.Child("language").Value.ToString());
        }
       /* else if (DBTask.Result.Child("language").Value.ToString() == "")
        {
            gameConfig.SetLanguage("English");
            Debug.Log("English");
        }*/
        else
        {
            gameConfig.SetLanguage("English");
            Debug.Log("English");
        }
        loadLanguageFlag = true;
    }
}
