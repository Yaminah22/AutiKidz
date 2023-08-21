using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using Firebase;
using Firebase.Auth;
using Firebase.Database;
using UnityEngine.SceneManagement;
using TMPro;

public class FirebaseAuthManager : MonoBehaviour
{
    public static FirebaseAuthManager Instance { get; private set; }

    // Firebase variable
    [Header("Firebase")]
    public DependencyStatus dependencyStatus;
    public FirebaseAuth auth;
    public FirebaseUser user;
    public DatabaseReference DBreference;
    public TMP_Text text;
    public GameObject popUpPanel;
    // Login Variables
    [Space]
    [Header("Login")]
    public InputField emailLoginField;
    public InputField passwordLoginField;

    // Registration Variables
    [Space]
    [Header("Registration")]
    public InputField nameRegisterField;
    public InputField emailRegisterField;
    public InputField passwordRegisterField;
    public InputField confirmPasswordRegisterField;

    private void Start()
    {
        StartCoroutine(CheckAndFixDependenciesAsync());
    }

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }

        Instance = this;
        DontDestroyOnLoad(gameObject);
    }
    private IEnumerator CheckAndFixDependenciesAsync()
    {
        var dependencyTask = FirebaseApp.CheckAndFixDependenciesAsync();

        yield return new WaitUntil(() => dependencyTask.IsCompleted);

        dependencyStatus = dependencyTask.Result;

        if (dependencyStatus == DependencyStatus.Available)
        {
            InitializeFirebase();
            yield return new WaitForEndOfFrame();
            StartCoroutine(CheckForAutoLogin());
        }
        else
        {
            Debug.LogError("Could not resolve all firebase dependencies: " + dependencyStatus);
        }
    }

    void InitializeFirebase()
    {
        //Set the default instance object
        auth = FirebaseAuth.DefaultInstance;
        DBreference = FirebaseDatabase.DefaultInstance.RootReference;
        auth.StateChanged += AuthStateChanged;
        AuthStateChanged(this, null);
    }

    // Track state changes of the auth object.
    void AuthStateChanged(object sender, System.EventArgs eventArgs)
    {
        if (auth.CurrentUser != user)
        {
            bool signedIn = user != auth.CurrentUser && auth.CurrentUser != null;

            if (!signedIn)
            {
                Debug.Log("Signed out " + user.UserId);
                SceneManager.LoadScene("FirebaseLogin");
            }

            user = auth.CurrentUser;

            if (signedIn)
            {
                Debug.Log("Signed in " + user.UserId);
                References.ReferencedUser = user;
                SceneManager.LoadScene("LoadMainMenu");
            }
        }
    }

    private IEnumerator CheckForAutoLogin()
    {
        
        if (user != null)
        {
            var reloadUsertask = user.ReloadAsync();
            yield return new WaitUntil(() => reloadUsertask.IsCompleted);
            AutoLogin();
        }
        else
        {
            UIManager.Instance.OpenLoginPanel();
        }
    }

    private void AutoLogin()
    {
        if (user != null)
        {
            References.ReferencedUser = user;
            SceneManager.LoadScene("LoadMainMenu");
        }
    }

    public void Login()
    {
        if (emailLoginField.text == "")
        {
            StartCoroutine(PopUpNotification("Please enter a valid email!"));
        }
        else if (passwordLoginField.text == "")
        {
            StartCoroutine(PopUpNotification("Please enter your password!"));
        }
        else StartCoroutine(LoginAsync(emailLoginField.text, passwordLoginField.text));
    }

    public IEnumerator LoginAsync(string email, string password)
    {
        var loginTask = auth.SignInWithEmailAndPasswordAsync(email, password);

        yield return new WaitUntil(() => loginTask.IsCompleted);

        if (loginTask.Exception != null)
        {
            Debug.LogError(loginTask.Exception);

            FirebaseException firebaseException = loginTask.Exception.GetBaseException() as FirebaseException;
            AuthError authError = (AuthError)firebaseException.ErrorCode;


            string failedMessage = "Login Failed ";

            switch (authError)
            {
                case AuthError.InvalidEmail:
                    failedMessage += "Invalid Email!";
                    break;
                case AuthError.WrongPassword:
                    failedMessage += "Wrong Password!";
                    break;
                case AuthError.UserNotFound:
                    failedMessage = "This user does not exist!";
                    UIManager.Instance.OpenRegistrationPanel();
                    break;
                default:
                    failedMessage = "Login Failed!";
                    break;
            }

            //Debug.Log(failedMessage);
            StartCoroutine(PopUpNotification(failedMessage));

        }
        else
        {
            user = loginTask.Result.User;

            Debug.LogFormat("{0} You Are Successfully Logged In", user.DisplayName);

            References.ReferencedUser = user;
            UnityEngine.SceneManagement.SceneManager.LoadScene("LoadMainMenu");
        }
    }
    public IEnumerator PopUpNotification(string failedMessage)
    {
        popUpPanel.SetActive(true);
        text.text = failedMessage;
        yield return new WaitForSeconds(3f);
        popUpPanel.SetActive(false);
    }
    public void Register()
    {
        if(nameRegisterField.text=="")
        {
            StartCoroutine(PopUpNotification("Kindly enter a user name!"));
        }
        else if(emailRegisterField.text=="")
        {
            StartCoroutine(PopUpNotification("Kindly enter an email!"));
        }
        else if (passwordRegisterField.text == "")
        {
            StartCoroutine(PopUpNotification("Kindly enter a password!"));
        }
        else if (confirmPasswordRegisterField.text == "")
        {
            StartCoroutine(PopUpNotification("Kindly confirm your password!"));
        }
        else if(passwordRegisterField.text != confirmPasswordRegisterField.text)
        {
            StartCoroutine(PopUpNotification("Your password does not match!"));
        }
        else StartCoroutine(RegisterAsync(nameRegisterField.text, emailRegisterField.text, passwordRegisterField.text, confirmPasswordRegisterField.text));
    }

    private IEnumerator RegisterAsync(string name, string email, string password, string confirmPassword)
    {
            var registerTask = auth.CreateUserWithEmailAndPasswordAsync(email, password);

            yield return new WaitUntil(() => registerTask.IsCompleted);

            if (registerTask.Exception != null)
            {
                Debug.LogError(registerTask.Exception);

                FirebaseException firebaseException = registerTask.Exception.GetBaseException() as FirebaseException;
                AuthError authError = (AuthError)firebaseException.ErrorCode;

                string failedMessage = "Registration Failed ";
                switch (authError)
                {
                    case AuthError.InvalidEmail:
                        failedMessage += "Invalid Email!";
                        break;
                    case AuthError.WrongPassword:
                        failedMessage += "Wrong Password!";
                        break;
                    case AuthError.MissingEmail:
                        failedMessage += "Please enter a valid email!";
                        break;
                    case AuthError.MissingPassword:
                        failedMessage += "Please enter a password!";
                        break;
                    default:
                        failedMessage = "Registration Failed!";
                        break;
                }

                //Debug.Log(failedMessage);
                StartCoroutine(PopUpNotification(failedMessage));
            }
            else
            {
                // Get The User After Registration Success
                user = registerTask.Result.User;

                UserProfile userProfile = new UserProfile { DisplayName = name };

                var updateProfileTask = user.UpdateUserProfileAsync(userProfile);

                yield return new WaitUntil(() => updateProfileTask.IsCompleted);

                if (updateProfileTask.Exception != null)
                {
                    // Delete the user if user update failed
                    user.DeleteAsync();

                    Debug.LogError(updateProfileTask.Exception);

                    FirebaseException firebaseException = updateProfileTask.Exception.GetBaseException() as FirebaseException;
                    AuthError authError = (AuthError)firebaseException.ErrorCode;


                    string failedMessage = "Profile update Failed! Becuase ";
                    switch (authError)
                    {
                        case AuthError.InvalidEmail:
                            failedMessage += "Email is invalid";
                            break;
                        case AuthError.WrongPassword:
                            failedMessage += "Wrong Password";
                            break;
                        case AuthError.MissingEmail:
                            failedMessage += "Email is missing";
                            break;
                        case AuthError.MissingPassword:
                            failedMessage += "Password is missing";
                            break;
                        default:
                            failedMessage = "Profile update Failed";
                            break;
                    }

                    Debug.Log(failedMessage);
                }
                else
                {
                    Debug.Log("Registration Sucessful Welcome " + user.DisplayName);
                    ///StartCoroutine(PopUpNotification("Registration Sucessful Welcome " + user.DisplayName));
                    //UIManager.Instance.OpenLoginPanel();
                }
            }
        }
    }

