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

public class spiderTreatmentProgress : MonoBehaviour
{
    private int firstnum;
    private int lastnum;
    private int firstminuslast;
    public TextMeshProUGUI TreatmentProgressText;
    public TextMeshPro SpeechText;
    Firebase.Auth.FirebaseAuth auth;
    Firebase.Auth.FirebaseUser user;
    DatabaseReference reference;
    DatabaseReference spider;
    // Start is called before the first frame update
    void Start()
    {
        FirebaseDatabase.DefaultInstance.SetPersistenceEnabled(false);
        reference = FirebaseDatabase.DefaultInstance.RootReference;
        InitializeFirebase();
        LoadPatientSpiderTreatmentData();
        GetPatientData();
        ComparePatientData();
    }

    // Update is called once per frame
    void Update()
    {
    }

    public void ComparePatientData()
    {
                reference.Child("Questionnaires").Child(user.UserId).Child("Spider").GetValueAsync().ContinueWithOnMainThread(task =>
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
                    SpeechText.text = "Welcome to Phobia-B-Gone! Answer the FSQ after exposure tasks and see your progress over-time!";
                }
                else if (firstminuslast > 0)
                {
                    SpeechText.text = "Well done! Your FSQ score has improved by " + strfirstminuslast  + " since you started!";
                }
                else if (firstminuslast < 0)
                {
                    SpeechText.text = "Your FSQ score has dropped by " + strfirstminuslast  + " since you started, please rest if needed";
                }
                else
                {
                    SpeechText.text = "Your current FSQ score is the same as your initial score, use the app as much as you want!";
                }
            }
            else
            {
                Debug.Log("Unsuccessful");
            }
        });
        
        
        
    }
    
    /*public void ComparePatientData()
    {
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
                int treatmentCount = (int)args.Snapshot.ChildrenCount;
                string strtreatmentCount = treatmentCount.ToString();
                Debug.Log(treatmentCount);
                string test1= args.Snapshot.Child("1").Child("AnswerScore").Value.ToString();
                string test2 = args.Snapshot.Child(strtreatmentCount).Child("AnswerScore").Value.ToString();
                int test1num = int.Parse(test1);
                int test2num = int.Parse(test2);
                int firstminuslast = test1num - test2num;
                string strfirstminuslast = firstminuslast.ToString(); 
                
                if (treatmentCount == 1)
                {
                    SpeechText.text = "Welcome to Phobia-B-Gone! Answer the FSQ after exposure tasks and see your progress over-time!";
                }
                else if (firstminuslast > 0)
                {
                    SpeechText.text = "Well done! Your FSQ score has improved by " + strfirstminuslast  + " since you started!";
                }
                else if (firstminuslast < 0)
                {
                    SpeechText.text = "Your FSQ score has dropped by " + strfirstminuslast  + " since you started, please rest if needed";
                }
                else
                {
                    SpeechText.text = "Your current FSQ score is the same as your initial score, use the app as much as you want!";
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
        
        reference.Child("Questionnaires").Child(user.UserId).Child("Spider").GetValueAsync().ContinueWithOnMainThread(task =>
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
                    SpeechText.text = "Welcome to Phobia-B-Gone! Answer the FSQ after exposure tasks and see your progress over-time!";
                }
                else if (firstminuslast > 0)
                {
                    SpeechText.text = "Well done! Your FSQ score has improved by " + strfirstminuslast  + " since you started!";
                }
                else if (firstminuslast < 0)
                {
                    SpeechText.text = "Your FSQ score has dropped by " + strfirstminuslast  + " since you started, please rest if needed";
                }
                else
                {
                    SpeechText.text = "Your current FSQ score is the same as your initial score, use the app as much as you want!";
                }
            }
            else
            {
                Debug.Log("Unsuccessful");
            }
        });
        
        
        
    }
    */
    
    
    
    public void LoadPatientSpiderTreatmentData()
    {
        reference.Child("Questionnaires").Child(user.UserId).Child("Spider").GetValueAsync().ContinueWithOnMainThread(task =>
        {
            if (task.IsCompleted)
            {
                
                DataSnapshot snapshot = task.Result;
                Debug.Log("Successful");
                
                int treatmentCount = (int)snapshot.ChildrenCount;
                string strtreatmentCount = treatmentCount.ToString();
                Debug.Log(treatmentCount);
                /*string test1= snapshot.Child("1").Child("AnswerScore").Value.ToString();
                string test2 = snapshot.Child(strtreatmentCount).Child("AnswerScore").Value.ToString();
                int test1num = int.Parse(test1);
                int test2num = int.Parse(test2);
                int firstminuslast = test1num - test2num;
                string strfirstminuslast = firstminuslast.ToString(); */
                
                for (int i = 1; i <= treatmentCount; i++)
                {   
                    TreatmentProgressText.text += snapshot.Child(i.ToString()).Child("AnswerDateTime").Value.ToString() + "                   " 
                        + snapshot.Child(i.ToString()).Child("AnswerScore").Value.ToString() + "\n" + "\n";
                }
                
                /*if (treatmentCount == 1)
                {
                    SpeechText.text = "Welcome to Phobia-B-Gone! Answer the FSQ after exposure tasks and see your progress over-time!";
                }
                else if (firstminuslast > 0)
                {
                    SpeechText.text = "Well done! Your FSQ score has improved by " + strfirstminuslast  + " since you started!";
                }
                else if (firstminuslast < 0)
                {
                    SpeechText.text = "Your FSQ score has dropped by " + strfirstminuslast  + " since you started, please rest if needed";

                }
                else
                {
                    SpeechText.text = "Your current FSQ score is the same as your initial score, use the app as much as you want!";
                }*/
            }
            else
            {
                Debug.Log("Unsuccessful");
            }
        });
    }
    
    public void GetPatientData()
    {
        reference.Child("Questionnaires").Child(user.UserId).Child("Spider").GetValueAsync().ContinueWith(task =>
        {
            if (task.IsCompleted)
            {
                DataSnapshot snapshot = task.Result;
                Debug.Log("Successful");
                //Debug.Log(snapshot.ChildrenCount.ToString());
            }
            else
            {
                Debug.Log("Unsuccessful");
            }
        });
        
        /*FirebaseDatabase.DefaultInstance.GetReference("Questionnaires")
            .GetValueAsync().ContinueWithOnMainThread(task => {
                if (task.IsFaulted) {
                    Debug.Log("Data is not retrieved!");
                    // Handle the error...
                }
                else if (task.IsCompleted) {
                    DataSnapshot snapshot = task.Result;
                    // Do something with snapshot...
                }
            });*/
        /*reference.Child("Questionnaires").Child(user.UserId).Child("Spider").GetValueAsync().ContinueWith(task =>
        {
            if (task.IsCompleted)
            {
                Debug.Log("successful");
                DataSnapshot snapshot = task.Result;
                //Debug.Log(data.text = snapshot.Child("UserName").Value.ToString());
                Debug.Log(snapshot.Child("2021-10-03T21:49:23").Value.ToString());
                /*Debug.Log(snapshot.Child("UserName").Value.ToString());
                Debug.Log(snapshot.Child("Email").Value.ToString());#1#
            }
            else
            {
                Debug.Log("Unsuccessful");
            }

        });*/
    }
    
    public void BackToMenuButton()
    {
        SceneManager.LoadScene("SpiderPhobiaMenu");
    }
    
    public void AnswerFSQButton()
    {
        SceneManager.LoadScene("FirstSpiderEval");
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


