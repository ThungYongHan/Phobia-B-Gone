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

public class cockroachTreatmentProgress : MonoBehaviour
{
    private int firstnum;
    private int lastnum;
    private int firstminuslast;
    
    private FirebaseAuth _auth;
    private FirebaseUser _user;
    private DatabaseReference _reference;
    public TextMeshProUGUI TreatmentProgressText;
    public TextMeshPro SpeechText;
    // DatabaseReference cockroach;

    void Start()
    {
        FirebaseDatabase.DefaultInstance.SetPersistenceEnabled(false);
        _auth = FirebaseAuth.DefaultInstance;
        _reference = FirebaseDatabase.DefaultInstance.RootReference;
        // get currently logged-in user
        _user = _auth.CurrentUser;
        LoadPatientCockroachTreatmentData();
        // GetPatientData();
        ComparePatientData();
    }
    
    public void ComparePatientData()
    {
        _reference.Child("Questionnaires").Child(_user.UserId).Child("Cockroach").GetValueAsync().ContinueWithOnMainThread(task =>
        {
            if (task.IsCompleted)
            {
                DataSnapshot snapshot = task.Result;
                int treatmentCount = (int)snapshot.ChildrenCount;
                string strtreatmentCount = treatmentCount.ToString();
                Debug.Log(treatmentCount);
                string test1= snapshot.Child("1").Child("AnswerScore").Value.ToString();
                string test2 = snapshot.Child(strtreatmentCount).Child("AnswerScore").Value.ToString();
                int test1num = int.Parse(test1);
                int test2num = int.Parse(test2);
                int firstminuslast = test1num - test2num;
                string strfirstminuslast = firstminuslast.ToString(); 
                
                if (treatmentCount == 1)
                {
                    SpeechText.text = "Welcome to Phobia-B-Gone! Answer the FCQ after exposure tasks and see your progress over-time!";
                }
                else if (firstminuslast > 0)
                {
                    SpeechText.text = "Well done! Your FCQ score has improved by " + strfirstminuslast  + " since you started!";
                }
                else if (firstminuslast < 0)
                {
                    SpeechText.text = "Your FCQ score has dropped by " + strfirstminuslast  + " since you started, please take a break or consult a therapist if needed";
                }
                else
                {
                    SpeechText.text = "Your current FCQ score is the same as your initial score, use the app as much as you want and take breaks if needed!";
                }
            }
            else
            {
                Debug.Log("Unsuccessful");
            }
        });
    }
    
    public void LoadPatientCockroachTreatmentData()
    {
        _reference.Child("Questionnaires").Child(_user.UserId).Child("Cockroach").GetValueAsync().ContinueWithOnMainThread(task =>
        {
            if (task.IsCompleted)
            {
                DataSnapshot snapshot = task.Result;
                int treatmentCount = (int)snapshot.ChildrenCount;
                for (int i = 1; i <= treatmentCount; i++)
                {   
                    TreatmentProgressText.text += snapshot.Child(i.ToString()).Child("AnswerDateTime").Value + "                   " 
                        + snapshot.Child(i.ToString()).Child("AnswerScore").Value + "\n" + "\n";
                }
            }
            else
            {
                Debug.Log("Unsuccessful");
            }
        });
    }
    
    // public void GetPatientData()
    // {
    //     _reference.Child("Questionnaires").Child(_user.UserId).Child("Cockroach").GetValueAsync().ContinueWith(task =>
    //     {
    //         if (task.IsCompleted)
    //         {
    //             DataSnapshot snapshot = task.Result;
    //             Debug.Log("Successful");
    //         }
    //         else
    //         {
    //             Debug.Log("Unsuccessful");
    //         }
    //     });
    // }
    
    public void BackToMenuButton()
    {
        SceneManager.LoadScene("CockroachPhobiaMenu");
    }
    
    public void quitApp()
    {
        _auth.SignOut();
        Application.Quit();
    }
    
    public void AnswerFCQButton()
    {
        SceneManager.LoadScene("TreatCockroachEval");
    }
}


