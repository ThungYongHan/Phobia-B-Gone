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
public class FirebaseUserManager : MonoBehaviour
{
    DatabaseReference reference;

    //public VrModeController scriptB;
    public string message;
    public Text signuperrormessage;
    public Text loginerrormessage;

    [SerializeField] private InputField loginemail;
    [SerializeField] private InputField loginpassword;
    [SerializeField] private InputField signuppassword;
    [SerializeField] private InputField signuppasswordconfirm;
    [SerializeField] private InputField signupemail;
    [SerializeField] private InputField signupusername;
    
    FirebaseAuth auth;

    public Canvas LogInCanvas, SignUpCanvas;
    void Start()
    {
        reference = FirebaseDatabase.DefaultInstance.RootReference;

        auth = FirebaseAuth.DefaultInstance;
        signuperrormessage = GameObject.Find("signuperrormessage").GetComponent<UnityEngine.UI.Text>();
        loginerrormessage = GameObject.Find("loginerrormessage").GetComponent<UnityEngine.UI.Text>();
        //XRGeneralSettings.Instance.Manager.InitializeLoader();
        //XRGeneralSettings.Instance.Manager.DeinitializeLoader();
        SignUpCanvas = GameObject.Find("SignUpCanvas").GetComponent<Canvas>();
        SignUpCanvas.enabled = false;
        LogInCanvas = GameObject.Find("LogInCanvas").GetComponent<Canvas>();
    }
    
    UserDetails user = new UserDetails();
    
    public void switchToSignUp()
    {
        SignUpCanvas.enabled = true;
        LogInCanvas.enabled = false;
    }
    
    public void switchToLogIn()
    {
        SignUpCanvas.enabled = false;
        LogInCanvas.enabled = true;
    }

    public void signup()
    {
        string checktext = signupusername.text;
        if (signuppassword.text != signuppasswordconfirm.text)
        {
            signuperrormessage.text = "Passwords do not match!";
        }
        /*if (!string.IsNullOrEmpty(checktext))
        {
            signuperrormessage.text = "Invalid username!";
        }*/
        else
        {
            auth.CreateUserWithEmailAndPasswordAsync(signupemail.text, signuppassword.text).ContinueWithOnMainThread(signuptask =>
            {
                if (signuptask.IsCanceled)
                {
                    Debug.LogError("CreateUserWithEmailAndPasswordAsync was canceled.");
                    return;
                }

                if (signuptask.IsFaulted)
                {
                    Debug.LogError("CreateUserWithEmailAndPasswordAsync encountered an error:" + signuptask.Exception);
                    if (signuptask.Exception != null)
                    {
                        message = "Login failed";
                        FirebaseException firebaseEx = signuptask.Exception.GetBaseException() as FirebaseException;
                        AuthError errorCode = (AuthError)firebaseEx.ErrorCode;
                        switch (errorCode)
                        {
                            case AuthError.MissingEmail:
                                signuperrormessage.text  = "Missing Email";
                                break;
                            case AuthError.MissingPassword:
                                signuperrormessage.text  = "Missing Password";
                                break;
                            case AuthError.InvalidEmail:
                                signuperrormessage.text  = "Invalid Email";
                                break;
                            case AuthError.EmailAlreadyInUse:
                                signuperrormessage.text  = "Email is already in use!";
                                break;
                            /*case AuthError.UserNotFound:
                                errormessage.text  = "Account does not exist";
                                break;*/
                        }
                        return;
                    }
                }
                
                
                FirebaseUser newuser = signuptask.Result;
                Debug.LogFormat("Firebase user is created successfully :{0} ({1})", newuser.DisplayName, newuser.UserId);
                signuperrormessage.text = "The user has signed up successfully";
                
                user.Username = signupusername.text;
                user.Password = signuppassword.text;
                user.Email = signupemail.text;
                string json = JsonUtility.ToJson(user);
                reference.Child("User").Child(newuser.UserId).SetRawJsonValueAsync(json).ContinueWithOnMainThread(task => 
                {
                    if (task.IsCompleted)
                    {
                        // reference.Child("Sessions").SetRawJsonValueAsync(json);
                        Debug.Log("Successfully added new user data to Firebase");
                        switchToLogIn();
                    }
                    else
                    {
                        Debug.Log("Unsuccessful");
                    }
                });
            });
        }
    }

    public void signin()
    {
        //string message = "login failed";
        auth.SignInWithEmailAndPasswordAsync(loginemail.text, loginpassword.text).ContinueWithOnMainThread(logintask =>
        {
            if (logintask.IsCanceled)
            {
                Debug.Log("SignInWithEmailAndPasswordAsync was canceled.");
                return;
            }

            if (logintask.IsFaulted)
            {
                Debug.Log("SignInWithEmailAndPasswordAsync encountered an error:" + logintask.Exception);
                if (logintask.Exception != null)
                {
                    message = "Login failed";
                    FirebaseException firebaseEx = logintask.Exception.GetBaseException() as FirebaseException;
                    AuthError errorCode = (AuthError)firebaseEx.ErrorCode;
                    /*if (errorCode == AuthError.MissingEmail)
                    {
                        errormessage.text = "MissingEmail";
                    }
                    if (errorCode == AuthError.MissingPassword)
                    {
                        //message = "MissingPassword";
                        errormessage.text = "MissingPassword";
                        // errormessage.SetActive(false);
                    }
                    if (errorCode == AuthError.WrongPassword)
                    {
                        errormessage.text = "WrongPassword";
                    }
                    if (errorCode == AuthError.InvalidEmail)
                    {
                        errormessage.text = "InvalidEmail";
                    }
                    if (errorCode == AuthError.UserNotFound)
                    {
                        errormessage.text = "UserNotFound";
                    }*/
                    switch (errorCode)
                    {
                        case AuthError.MissingEmail:
                            loginerrormessage.text  = "Missing Email";
                            break;
                        case AuthError.MissingPassword:
                            loginerrormessage.text  = "Missing Password";
                            break;
                        case AuthError.WrongPassword:
                            loginerrormessage.text = "Wrong Password";
                            break;
                        case AuthError.InvalidEmail:
                            loginerrormessage.text  = "Invalid Email";
                            break;
                        /*case AuthError.UserNotFound:
                            errormessage.text  = "Account does not exist";
                            break;*/
                    }

                    return;
                }
                /*FirebaseException firebaseEx = task.Exception.GetBaseException() as FirebaseException;
                AuthError errorCode = (AuthError)firebaseEx.ErrorCode;
                string message = "Login Failed";
                switch (errorCode)
                {
                    case AuthError.MissingEmail:
                        message = "Missing Email";
                        break;
                    case AuthError.MissingPassword:
                        message = "Missing Password";
                        break;
                    case AuthError.WrongPassword:
                        message= "Wrong Password";
                        break;
                    case AuthError.InvalidEmail:
                        message = "Invalid Email";
                        break;
                    case AuthError.UserNotFound:
                        message = "Account does not exist";
                        break;
                }*/
            }
            //Debug.Log(message);
            //errormessage.text = message;
            FirebaseUser returnuser = logintask.Result;
            Debug.LogFormat("Firebase user is logged in  successfully :{0} ({1})", returnuser.DisplayName, returnuser.UserId);
            loginerrormessage.text = "The user is logged in successfully";
            SceneManager.LoadScene("2dphobiaselectmenu");
        });
    }
}
