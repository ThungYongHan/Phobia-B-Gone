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
    
    public Text signuperrormessage;
    public Text loginerrormessage;
    public GameObject LogInErrorMessagePanel;
    public GameObject SignUpErrorMessagePanel; 
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
        LogInErrorMessagePanel.SetActive(false);
        SignUpErrorMessagePanel.SetActive(false);
        auth = FirebaseAuth.DefaultInstance;
        SignUpCanvas.enabled = false;
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
        if (string.IsNullOrWhiteSpace(signupemail.text))
        {
            SignUpErrorMessagePanel.SetActive(true);
            signuperrormessage.text = "Please input an email address!";
        }
        else if (string.IsNullOrWhiteSpace(signupusername.text))
        {
            SignUpErrorMessagePanel.SetActive(true);
            signuperrormessage.text = "Please input a username!";
        }
        else if (signuppassword.text != signuppasswordconfirm.text)
        {
            SignUpErrorMessagePanel.SetActive(true);
            signuperrormessage.text = "Passwords do not match!";
        }
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
                        SignUpErrorMessagePanel.SetActive(true);
                        FirebaseException firebaseEx = signuptask.Exception.GetBaseException() as FirebaseException;
                        
                        AuthError errorCode = (AuthError)firebaseEx.ErrorCode;
                        switch (errorCode)
                        {
                            case AuthError.MissingEmail:
                                signuperrormessage.text  = "Missing Email";
                                break;
                            case AuthError.InvalidEmail:
                                signuperrormessage.text  = "Invalid Email";
                                break;
                            case AuthError.MissingPassword:
                                signuperrormessage.text  = "Missing Password";
                                break;
                            case AuthError.WeakPassword:
                                signuperrormessage.text  = "Chosen Password is too weak!";
                                break;
                            case AuthError.EmailAlreadyInUse:
                                signuperrormessage.text  = "Email is already in use!";
                                break;
                        }
                        return;
                    }
                }
                
                FirebaseUser newuser = signuptask.Result;
                Debug.LogFormat("Firebase user is created successfully :{0} ({1})", newuser.DisplayName, newuser.UserId);
                user.Username = signupusername.text;
                user.Email = signupemail.text;
                string json = JsonUtility.ToJson(user);
                reference.Child("User").Child(newuser.UserId).SetRawJsonValueAsync(json).ContinueWithOnMainThread(task => 
                {
                    if (task.IsCompleted)
                    {
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
                    LogInErrorMessagePanel.SetActive(true);
                    FirebaseException firebaseEx = logintask.Exception.GetBaseException() as FirebaseException;
                    AuthError errorCode = (AuthError)firebaseEx.ErrorCode;
                    if (string.IsNullOrWhiteSpace(loginemail.text) && string.IsNullOrWhiteSpace(loginpassword.text))
                    {
                        loginerrormessage.text  = "Missing Email and Password!";
                        return;
                    }
                    
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
                        case AuthError.UserNotFound:
                            loginerrormessage.text  = "Account does not exist";
                            break;
                    }
                    return;
                }
            }
            FirebaseUser returnuser = logintask.Result;
            Debug.LogFormat("Firebase user is logged in  successfully :{0} ({1})", returnuser.DisplayName, returnuser.UserId);
            // loginerrormessage.text = "The user is logged in successfully";
            SceneManager.LoadScene("PhobiaSelectMenu");
        });
    }

    public void turnOffLoginErrorPanel()
    {
        LogInErrorMessagePanel.SetActive(false);
    }
    
    public void turnOffSignUpErrorPanel()
    {
        SignUpErrorMessagePanel.SetActive(false);
    }
}
