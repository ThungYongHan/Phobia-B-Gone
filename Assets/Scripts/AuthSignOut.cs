using System.Collections;
using System.Collections.Generic;
using Firebase;
using UnityEngine;
using Firebase.Auth;
using Firebase.Database;
using UnityEngine.UI;
using Firebase.Extensions;
using UnityEngine.SceneManagement;
using UnityEngine.Serialization;
using UnityEngine.XR.Management;

public class AuthSignOut : MonoBehaviour
{
    FirebaseAuth auth;

    public void Start()
    {
        auth = FirebaseAuth.DefaultInstance;
    }
    public void SignOutButton()
    {
        auth.SignOut();
        SceneManager.LoadScene("LogInSignUp");
    }
}
