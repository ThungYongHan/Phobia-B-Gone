using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Firebase.Database;
using Firebase.Auth;
using Firebase.Extensions;
using TMPro;
using UnityEngine.SceneManagement;

public class SpiderFormSubmitScript : MonoBehaviour
{
    private FirebaseAuth _auth;
    private FirebaseUser _user;
    private DatabaseReference _reference;
    // for questionnaire data saving auto-increment
    private int _incrementNum;
    // fsq question sliders
    public Slider Q1, Q2, Q3, Q4, Q5, Q6, Q7, Q8, Q9, Q10, Q11, Q12, Q13, Q14, Q15, Q16, Q17, Q18;
    public TextMeshProUGUI FSQInstructions;
    
    void Start()
    {
        FirebaseDatabase.DefaultInstance.SetPersistenceEnabled(false);
        _auth = FirebaseAuth.DefaultInstance;
        _reference = FirebaseDatabase.DefaultInstance.RootReference;
        _user = _auth.CurrentUser;
        // count existing fsq data of the currently logged-in user
        CountPatientSpiderData();
    }
    
    // create object of Questionnaire class to be converted to JSON format for Firebase 
    Questionnaire questionnaire = new Questionnaire();

    private void CountPatientSpiderData()
    {
        // get snapshot of all data at specified path
        _reference.Child("Questionnaires").Child(_user.UserId).Child("Spider").GetValueAsync().ContinueWithOnMainThread(task =>
        {
            if (task.IsCompleted)
            {
                // get result value from task
                DataSnapshot snapshot = task.Result;
                // if number of children in the snapshot is equal to 0 (no existing data)
                if (snapshot.ChildrenCount == 0)
                {
                    // the first fsq submitted by currently logged-in user will have '1' as its parent  
                    _incrementNum = 1;
                }
                // if fsq data exists for currently logged-in user
                else
                {
                    // get the number of children in the snapshot
                    _incrementNum = (int)snapshot.ChildrenCount;
                    // increment by 1 so the next fsq submission will not overwrite existing data
                    // and will be added as the next node with incrementing parent in the specified path
                    _incrementNum += 1;
                }
            }
            else
            {
                FSQInstructions.text = "Existing FSQ Data Checking Failed";
            }
        });
    }
    
    // user clicks submit for fsq in FirstSpiderEval
    public void submitFirstFSQ()
    {
        if (_incrementNum == 1)
        {
            // add up slider value and store as string
            string sumNum = (Q1.value + Q2.value + Q3.value + Q4.value + Q5.value + Q6.value + Q7.value +
                             Q8.value + Q9.value + Q10.value + Q11.value + Q12.value + Q13.value + Q14.value +
                             Q15.value + Q16.value + Q17.value + Q18.value).ToString();

            // get current date and time and store as string
            string currentdatetime = DateTime.Now.ToString("yyyy-MM-ddTHH:mm:ss");
            // store current date and time into AnswerDateTime variable of questionnaire object
            questionnaire.AnswerDateTime = currentdatetime;
            // store total FSQ slider value into AnswerScore variable of questionnaire object
            questionnaire.AnswerScore = sumNum;
            // convert questionnaire object variables into json format and store as string
            string json = JsonUtility.ToJson(questionnaire);

            // save json string of questionnaire object variables to the specified path
            _reference.Child("Questionnaires").Child(_user.UserId).Child("Spider").Child(_incrementNum.ToString())
                .SetRawJsonValueAsync(json).ContinueWith(task =>
                {
                    if (task.IsCompleted)
                    {
                        FSQInstructions.text = "Submitted FSQ Data Saving Is Successful";
                    }
                    else
                    {
                        FSQInstructions.text = "Submitted FSQ Data Saving Failed";
                    }
                });
            // load arachnophobia exposure therapy menu
            SceneManager.LoadScene("SpiderPhobiaMenu");
        }
        else
        {
            SceneManager.LoadScene("SpiderPhobiaMenu");
        }
    }

    public void submitTreatmentFSQ()
    {
        string sumNum = (Q1.value + Q2.value + Q3.value + Q4.value + Q5.value + Q6.value + Q7.value +
                         Q8.value + Q9.value + Q10.value + Q11.value + Q12.value + Q13.value + Q14.value +
                         Q15.value + Q16.value + Q17.value + Q18.value).ToString();
        
        string currentdatetime = DateTime.Now.ToString("yyyy-MM-ddTHH:mm:ss");
        questionnaire.AnswerDateTime = currentdatetime;
        questionnaire.AnswerScore = sumNum;
        string json = JsonUtility.ToJson(questionnaire);
        
        // save FSQ answer data with auto-incrementing parent
        _reference.Child("Questionnaires").Child(_user.UserId).Child("Spider").Child(_incrementNum.ToString())
            .SetRawJsonValueAsync(json).ContinueWith(task => 
        {
            if (task.IsCompleted)
            {
                FSQInstructions.text = "Submitted FSQ Data Saving Is Successful";
            }
            else
            {
                FSQInstructions.text = "Submitted FSQ Data Saving Failed";
            }
        });
        // load arachnophobia treatment progress viewing scene
        SceneManager.LoadScene("TreatmentProgressSpider");
    }
    
    public void quitApp()
    {
        Application.Quit();
        SceneManager.LoadScene("SignOutTreatmentProgressSpider");
    }
    
    public void backFSQ()
    {
        SceneManager.LoadScene("TreatmentProgressSpider");
    }
    
    public void signOutSubmit()
    {
        string sumNum = (Q1.value + Q2.value + Q3.value + Q4.value + Q5.value + Q6.value + Q7.value +
                         Q8.value + Q9.value + Q10.value + Q11.value + Q12.value + Q13.value + Q14.value +
                         Q15.value + Q16.value + Q17.value + Q18.value).ToString();
        
        string currentdatetime = DateTime.Now.ToString("yyyy-MM-ddTHH:mm:ss");
        questionnaire.AnswerDateTime = currentdatetime;
        questionnaire.AnswerScore = sumNum;
        string json = JsonUtility.ToJson(questionnaire);
        
        Debug.Log(currentdatetime);
        var user = _auth.CurrentUser;
        _reference.Child("Questionnaires").Child(user.UserId).Child("Spider").Child(_incrementNum.ToString()).SetRawJsonValueAsync(json).ContinueWith(task => 
        {
            if (task.IsCompleted)
            {
                Debug.Log("Successfully added data to Firebase"); 
            }
            else
            {
                Debug.Log("Unsuccessful");
            }
        });
        SceneManager.LoadScene("SignOutTreatmentProgressSpider");
    }
}

// InitializeFirebase();
// void InitializeFirebase() {
//     Debug.Log("Setting up Firebase Auth");
//     _auth = Firebase.Auth.FirebaseAuth.DefaultInstance;
//     _auth.StateChanged += AuthStateChanged;
//     AuthStateChanged(this, null);
// }
//
// void AuthStateChanged(object sender, System.EventArgs eventArgs) {
//     if (_auth.CurrentUser != _user) {
//         bool signedIn = _user != _auth.CurrentUser && _auth.CurrentUser != null;
//         if (!signedIn && _user != null) {
//             Debug.Log("Signed out " + _user.UserId);
//         }
//         _user = _auth.CurrentUser;
//         if (signedIn) {
//             Debug.Log("Signed in " + _user.UserId);
//         }
//     }
// }
//
// void OnDestroy() {
//     _auth.StateChanged -= AuthStateChanged;
//     _auth = null;
// }
/*
Debug.Log(Q1.value + Q2.value + Q3.value + Q4.value + Q5.value + Q6.value + Q7.value + 
          Q8.value + Q9.value + Q10.value + Q11.value + Q12.value + Q13.value + Q14.value + 
          Q15.value + Q16.value + Q17.value + Q18.value);
          */
/*float phobiaNum = Q1.value + Q2.value + Q3.value + Q4.value + Q5.value + Q6.value + Q7.value +
                  Q8.value + Q9.value + Q10.value + Q11.value + Q12.value + Q13.value + Q14.value +
                  Q15.value + Q16.value + Q17.value + Q18.value;*/
//string currentdatetime = DateTime.Now.ToString("yyyy’-‘MM’-‘dd’T’HH’:’mm’:’ss");
//questionnaire.AnswerDateTime = currentdatetime;
/*public void CountPatientSpiderData()
    {
        //reference.Child("Questionnaires").Child(user.UserId).Child("Spider").GetValueAsync().ContinueWithOnMainThread(task =>
        reference.Child("Questionnaires").Child(user.UserId).Child("Spider").ValueChanged += HandleValueChanged;

        void HandleValueChanged(object sender, ValueChangedEventArgs args)
        {
            if (args.DatabaseError != null)
            {
                Debug.LogError(args.DatabaseError.Message);
            }
            else
            {
                Debug.Log(args.Snapshot.ChildrenCount);
                if (args.Snapshot.ChildrenCount == 0)
                {
                    iterateNum = 1;
                    Debug.Log("Empty Children, now starting from 1");
                }
                else
                {
                    iterateNum = (int)args.Snapshot.ChildrenCount;
                    iterateNum = iterateNum + 1;
                }
                /*reference.Child("Questionnaires").Child(user.UserId).Child("Spider").GetValueAsync()
                    .ContinueWithOnMainThread(task =>
                    {
                        if (task.IsCompleted)
                        {
                            DataSnapshot snapshot = task.Result;
                            Debug.Log(snapshot.ChildrenCount.ToString());
                            Debug.Log("Successful");
                            if (snapshot.ChildrenCount == 0)
                            {
                                iterateNum = 1;
                                Debug.Log("Empty Children, now starting from 1");
                            }
                            else
                            {
                                iterateNum = (int)snapshot.ChildrenCount;
                                iterateNum = iterateNum + 1;
                            }
                        }
                        else
                        {
                            Debug.Log("Unsuccessful");
                        }
                    });#1#
            }
        }
    }*/
// Debug.Log(snapshot.ChildrenCount.ToString());
// Debug.Log("Successful");
// Debug.Log("Empty Children, now starting from 1");
