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
    public TextMeshProUGUI TreatmentProgressText;
    public TextMeshPro SpeechText;
    Firebase.Auth.FirebaseAuth auth;
    Firebase.Auth.FirebaseUser user;
    DatabaseReference reference;
    DatabaseReference cockroach;

    void Start()
    {
        FirebaseDatabase.DefaultInstance.SetPersistenceEnabled(false);
        reference = FirebaseDatabase.DefaultInstance.RootReference;
        InitializeFirebase();
        LoadPatientCockroachTreatmentData();
        GetPatientData();
        ComparePatientData();
    }
    
    public void ComparePatientData()
    {
        reference.Child("Questionnaires").Child(user.UserId).Child("Cockroach").GetValueAsync().ContinueWithOnMainThread(task =>
        {
            if (task.IsCompleted)
            {
                DataSnapshot snapshot = task.Result;
                Debug.Log("Successful");
                
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
        reference.Child("Questionnaires").Child(user.UserId).Child("Cockroach").GetValueAsync().ContinueWithOnMainThread(task =>
        {
            if (task.IsCompleted)
            {
                
                DataSnapshot snapshot = task.Result;
                Debug.Log("Successful");
                
                int treatmentCount = (int)snapshot.ChildrenCount;
                string strtreatmentCount = treatmentCount.ToString();
                Debug.Log(treatmentCount);
                for (int i = 1; i <= treatmentCount; i++)
                {   
                    TreatmentProgressText.text += snapshot.Child(i.ToString()).Child("AnswerDateTime").Value.ToString() + "                   " 
                        + snapshot.Child(i.ToString()).Child("AnswerScore").Value.ToString() + "\n" + "\n";
                }
            }
            else
            {
                Debug.Log("Unsuccessful");
            }
        });
    }
    
    public void GetPatientData()
    {
        reference.Child("Questionnaires").Child(user.UserId).Child("Cockroach").GetValueAsync().ContinueWith(task =>
        {
            if (task.IsCompleted)
            {
                DataSnapshot snapshot = task.Result;
                Debug.Log("Successful");
            }
            else
            {
                Debug.Log("Unsuccessful");
            }
        });
    }
    
    public void BackToMenuButton()
    {
        SceneManager.LoadScene("CockroachPhobiaMenu");
    }
    
    public void AnswerFCQButton()
    {
        SceneManager.LoadScene("FirstCockroachEval");
    }
    
    // Handle initialization of the necessary firebase modules:
    void InitializeFirebase() {
        Debug.Log("Setting up Firebase Auth");
        auth = Firebase.Auth.FirebaseAuth.DefaultInstance;
        auth.StateChanged += AuthStateChanged;
        AuthStateChanged(this, null);
    }

// Track state changes of the auth object.
    void AuthStateChanged(object sender, System.EventArgs eventArgs) {
        if (auth.CurrentUser != user) {
            bool signedIn = user != auth.CurrentUser && auth.CurrentUser != null;
            if (!signedIn && user != null) {
                Debug.Log("Signed out " + user.UserId);
            }
            user = auth.CurrentUser;
            if (signedIn) {
                Debug.Log("Signed in " + user.UserId);
            }
        }
    }

    void OnDestroy() {
        auth.StateChanged -= AuthStateChanged;
        auth = null;
    }
}


