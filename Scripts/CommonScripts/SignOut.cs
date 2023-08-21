using UnityEngine;
using Firebase.Auth;
public class SignOutExample : MonoBehaviour
{
    private FirebaseAuth auth;

    private void Start()
    {
        // Initialize Firebase Auth
        auth = FirebaseAuth.DefaultInstance;
    }

    public void SignOut()
    {
        auth.SignOut();
        Debug.Log("User signed out successfully.");
    }
}
