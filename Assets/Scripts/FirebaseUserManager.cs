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
    private DatabaseReference _reference;
    private FirebaseAuth _auth;
    
    public Canvas logInCanvas, signUpCanvas;
    public Text signuperrormessage, loginerrormessage;
    public GameObject logInErrorMessagePanel, signUpErrorMessagePanel;
    public InputField loginemail, loginpassword, signuppassword, 
        signuppasswordconfirm, signupemail, signupusername;

    void Start()
    {
        // get the root reference location of the firebase database
        _reference = FirebaseDatabase.DefaultInstance.RootReference;
        _auth = FirebaseAuth.DefaultInstance;
        // deactivate error message panel for both login and signup on start
        logInErrorMessagePanel.SetActive(false);
        signUpErrorMessagePanel.SetActive(false);
        // disable signUpCanvas on start 
        signUpCanvas.enabled = false;
    }
    
    // create object of UserDetails class to be converted to JSON format for Firebase 
    UserDetails user = new UserDetails();
    
    // user clicks sign up page button
    public void SwitchToSignUp()
    {
        signUpCanvas.enabled = true;
        logInCanvas.enabled = false;
    }
    
    // user clicks log in page button
    public void SwitchToLogIn()
    {
        signUpCanvas.enabled = false;
        logInCanvas.enabled = true;
    }

    // user clicks sign up button
    public void SignUp()
    {
        // if email is empty or has white-space characters only
        if (string.IsNullOrWhiteSpace(signupemail.text))
        {
            // active error message panel for sign up page
            signUpErrorMessagePanel.SetActive(true);
            // display error message 
            signuperrormessage.text = "Please input an email address!";
        }
        // if username is empty or has white-space characters only
        else if (string.IsNullOrWhiteSpace(signupusername.text))
        {
            signUpErrorMessagePanel.SetActive(true);
            signuperrormessage.text = "Please input a username!";
        }
        // if password does not match password confirmation
        else if (signuppassword.text != signuppasswordconfirm.text)
        {
            signUpErrorMessagePanel.SetActive(true);
            signuperrormessage.text = "Passwords do not match!";
        }
        else
        {   
            // create a new account with entered email and password
            _auth.CreateUserWithEmailAndPasswordAsync(signupemail.text, signuppassword.text)
                .ContinueWithOnMainThread(signuptask =>
            {
                // if task is cancelled 
                if (signuptask.IsCanceled)
                {
                    signuperrormessage.text = "Sign Up failed";
                    return;
                }
                //if task failed
                if (signuptask.IsFaulted)
                {
                    signuperrormessage.text = "Sign Up failed";
                    // if exception is found for task
                    if (signuptask.Exception != null)
                    {
                        signUpErrorMessagePanel.SetActive(true);
                        // initialize FirebaseException object with reference to task exception root cause
                        FirebaseException firebaseEx = signuptask.Exception.GetBaseException() as FirebaseException;
                        // get authentication error code of FirebaseException object
                        AuthError errorCode = (AuthError)firebaseEx.ErrorCode;
                        switch (errorCode)
                        {
                            case AuthError.InvalidEmail:
                                signuperrormessage.text  = "Invalid Email";
                                break;
                            case AuthError.EmailAlreadyInUse:
                                signuperrormessage.text  = "Email Is Already In Use By Another Account";
                                break;
                            case AuthError.MissingPassword:
                                signuperrormessage.text  = "Missing Password";
                                break;
                            case AuthError.WeakPassword:
                                signuperrormessage.text  = "Chosen Password Is Too Weak";
                                break;
                        }
                        return;
                    }
                }
                // get result value from task
                FirebaseUser newuser = signuptask.Result;
                // store input username into Username variable of user object
                user.Username = signupusername.text;
                // store input email into Email variable of user object
                user.Email = signupemail.text;
                // convert user object variables into json format and store as string
                string json = JsonUtility.ToJson(user);
                // save json string of user object variables to the specified path
                _reference.Child("User").Child(newuser.UserId).SetRawJsonValueAsync(json).ContinueWithOnMainThread(task => 
                {
                    if (task.IsCompleted)
                    {
                        signuperrormessage.text = "User Has Signed Up Successfully";
                        SwitchToLogIn();
                    }
                    else
                    {
                        signuperrormessage.text = "Sign Up Failed";
                    }
                });
            });
        }
    }
    
    public void LogIn()
    {
        // log in user with entered email and password
        _auth.SignInWithEmailAndPasswordAsync(loginemail.text, loginpassword.text)
            .ContinueWithOnMainThread(logintask =>
        {
            // if task is cancelled 
            if (logintask.IsCanceled)
            {
                loginerrormessage.text  = "Log In Failed";
                return;
            }

            //if task failed
            if (logintask.IsFaulted)
            {
                loginerrormessage.text  = "Log In Failed";      
                // if exception is found for task
                if (logintask.Exception != null)
                {
                    logInErrorMessagePanel.SetActive(true);
                    // initialize FirebaseException object with reference to task exception root cause
                    FirebaseException firebaseEx = logintask.Exception.GetBaseException() as FirebaseException;
                    // get authentication error code of FirebaseException object
                    AuthError errorCode = (AuthError)firebaseEx.ErrorCode;
                    // if email and password are empty or have white-space characters only
                    if (string.IsNullOrWhiteSpace(loginemail.text) && string.IsNullOrWhiteSpace(loginpassword.text))
                    {
                        loginerrormessage.text  = "Missing Email and Password!";
                        return;
                    }
                    // display error message if authentication error code matches AuthError case
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
                            loginerrormessage.text  = "Account Does Not exist";
                            break;
                    }
                    return;
                }
            }
            loginerrormessage.text  = "User Logged In Successfully";
            // load phobia selection menu
            SceneManager.LoadScene("PhobiaSelectMenu");
        });
    }

    public void turnOffLoginErrorPanel()
    {
        logInErrorMessagePanel.SetActive(false);
    }
    
    public void turnOffSignUpErrorPanel()
    {
        signUpErrorMessagePanel.SetActive(false);
    }
}

//Debug.LogError("CreateUserWithEmailAndPasswordAsync encountered an error:" + signuptask.Exception);
//Debug.LogFormat("Firebase user is created successfully :{0} ({1})", newuser.DisplayName, newuser.UserId);
//Debug.LogFormat("Firebase user is logged in  successfully :{0} ({1})", returnuser.DisplayName, returnuser.UserId);

// FirebaseUser returnuser = logintask.Result;
// Debug.LogFormat("Firebase user is logged in  successfully :{0} ({1})", returnuser.DisplayName, returnuser.UserId);