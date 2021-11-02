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
// GetPatientData();

public class spiderTreatmentProgress : MonoBehaviour
{
    private FirebaseAuth _auth;
    private FirebaseUser _user;
    private DatabaseReference _reference;
    public TextMeshProUGUI TreatmentProgressText;
    public TextMeshPro SpeechText;
    
    void Start()
    {
        // prevent retrieval of deleted or old data
        FirebaseDatabase.DefaultInstance.SetPersistenceEnabled(false);
        _auth = FirebaseAuth.DefaultInstance;
        // get the root reference location of the firebase database
        _reference = FirebaseDatabase.DefaultInstance.RootReference;
        // get currently logged-in user
        _user = _auth.CurrentUser;
        LoadPatientSpiderTreatmentData();
        ComparePatientData();
    }
    
    private void LoadPatientSpiderTreatmentData()
    {
        // get snapshot of all data at specified path
        _reference.Child("Questionnaires").Child(_user.UserId).Child("Spider").GetValueAsync().ContinueWithOnMainThread(task =>
        {
            if (task.IsCompleted)
            {
                // get result value from task
                DataSnapshot snapshot = task.Result;
                // get the number of children in the snapshot
                int treatmentCount = (int)snapshot.ChildrenCount;
                // for loop to iterate through child data one-by-one in the snapshot and then display them all
                for (int i = 1; i <= treatmentCount; i++)
                {   
                    TreatmentProgressText.text += snapshot.Child(i.ToString()).Child("AnswerDateTime").Value + "                   " 
                        + snapshot.Child(i.ToString()).Child("AnswerScore").Value + "\n" + "\n";
                }
            }
            else
            {
                SpeechText.text = "Arachnophobia Treatment Data Retrieval Failed";
            }
        });
    }
    
    private void ComparePatientData()
    {
        // get snapshot of all data at specified path
        _reference.Child("Questionnaires").Child(_user.UserId).Child("Spider").GetValueAsync().ContinueWithOnMainThread(task =>
        {
            if (task.IsCompleted)
            {
                // get result value from task
                DataSnapshot snapshot = task.Result;
                // get the number of children in the snapshot
                int treatmentDataCount = (int)snapshot.ChildrenCount;
                // store the number of children in the snapshot as string
                string strtreatmentDataCount = treatmentDataCount.ToString();
                // store the first FSQ score as string
                string firstTreatmentData= snapshot.Child("1").Child("AnswerScore").Value.ToString();
                // store the last FSQ score as string
                string lastTreatmentData = snapshot.Child(strtreatmentDataCount).Child("AnswerScore").Value.ToString();
                int integerfirstTreatmentData = int.Parse(firstTreatmentData);
                int integerlastTreatmentData = int.Parse(lastTreatmentData);
                // first FSQ score minus last FSQ score and store the difference as integer for comparison
                int firstminuslast = integerfirstTreatmentData - integerlastTreatmentData;
                string strfirstminuslast = firstminuslast.ToString(); 
                
                // if currently logged-in user only has one FSQ score data
                if (treatmentDataCount == 1)
                {
                    SpeechText.text = "Welcome to Phobia-B-Gone! Answer the FSQ after exposure tasks and see your progress over-time!";
                }
                else if (treatmentDataCount == 0)
                {
                    SpeechText.text = "You do not have any existing FSQ score data. Please go answer the FSQ to get your baseline score!";
                }
                // if the first FSQ score minus last FSQ score difference is larger than 0 (patient phobia improved)
                else if (firstminuslast > 0)
                {
                    SpeechText.text = "Well done! Your FSQ score has improved by " + strfirstminuslast + " points since you started!";
                }
                // if the first FSQ score minus last FSQ score difference is smaller than 0 (patient phobia worsened)
                else if (firstminuslast < 0)
                {
                    string negativeToPositive = strfirstminuslast;
                    // remove first char of score difference string (which is negative symbol)
                    negativeToPositive = negativeToPositive.Substring(1);
                    SpeechText.text = "Your FSQ score has dropped by " + negativeToPositive + " points since you started, please take a " +
                                      "break or consult a therapist if needed.";
                }
                // if the first FSQ score minus last FSQ score difference is equal to 0
                else if (firstminuslast == 0)
                {
                    SpeechText.text = "Your current FSQ score is the same as your initial score, use the app as much as you want " +
                                      "and take breaks if needed!";
                }
            }
            else
            {
                SpeechText.text = "Arachnophobia Treatment Data Comparison Failed";
            }
        });
    }

    // private void GetPatientData()
    // {
    //     _reference.Child("Questionnaires").Child(_user.UserId).Child("Spider").GetValueAsync().ContinueWith(task =>
    //     {
    //         if (task.IsCompleted)
    //         {
    //             DataSnapshot snapshot = task.Result;
    //             Debug.Log("Successful");
    //             //Debug.Log(snapshot.ChildrenCount.ToString());
    //         }
    //         else
    //         {
    //             Debug.Log("Unsuccessful");
    //         }
    //     });
    // }
    
    public void BackToMenuButton()
    {
        SceneManager.LoadScene("SpiderPhobiaMenu");
    }
    
    public void quitApp()
    {
        _auth.SignOut();
        Application.Quit();
        //UnityEditor.EditorApplication.isPlaying = false;
        //UnityEditor.EditorApplication.isPlaying = false;
    }
    
    public void AnswerFSQButton()
    {
        SceneManager.LoadScene("TreatSpiderEval");
    }
    

}

//     // Handle initialization of the necessary firebase modules:
//     void InitializeFirebase() {
//         Debug.Log("Setting up Firebase Auth");
//         _auth = Firebase.Auth.FirebaseAuth.DefaultInstance;
//         _auth.StateChanged += AuthStateChanged;
//         AuthStateChanged(this, null);
//     }
//
// // Track state changes of the auth object.
//     void AuthStateChanged(object sender, System.EventArgs eventArgs) {
//         if (_auth.CurrentUser != _user) {
//             bool signedIn = _user != _auth.CurrentUser && _auth.CurrentUser != null;
//             if (!signedIn && _user != null) {
//                 Debug.Log("Signed out " + _user.UserId);
//             }
//             _user = _auth.CurrentUser;
//             if (signedIn) {
//                 Debug.Log("Signed in " + _user.UserId);
//             }
//         }
//     }
//
//     void OnDestroy() {
//         _auth.StateChanged -= AuthStateChanged;
//         _auth = null;
//     }
// private int firstnum;
// private int lastnum;
// private int firstminuslast;
// DatabaseReference spider;
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
// InitializeFirebase();
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
/*string test1= snapshot.Child("1").Child("AnswerScore").Value.ToString();
string test2 = snapshot.Child(strtreatmentCount).Child("AnswerScore").Value.ToString();
int test1num = int.Parse(test1);
int test2num = int.Parse(test2);
int firstminuslast = test1num - test2num;
string strfirstminuslast = firstminuslast.ToString(); */
// SpeechText.text = "Your FSQ score has dropped by " + strfirstminuslast + " points since you started, please take a break or consult a therapist if needed.";
