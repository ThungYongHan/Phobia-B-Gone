using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Firebase.Database;
using Firebase.Auth;
using Firebase.Extensions;
using UnityEngine.SceneManagement;

public class FormSubmitScript : MonoBehaviour
{
    
    Firebase.Auth.FirebaseAuth auth;
    Firebase.Auth.FirebaseUser user;
    private int iterateNum = 0;
    public Slider Q1, Q2, Q3, Q4, Q5, Q6, Q7, Q8, Q9, Q10, Q11, Q12, Q13, Q14, Q15, Q16, Q17, Q18;
    DatabaseReference reference;
    //FirebaseAuth auth;
    void Start()
    {
        FirebaseDatabase.DefaultInstance.SetPersistenceEnabled(false);
        reference = FirebaseDatabase.DefaultInstance.RootReference;
        
        InitializeFirebase();
        CountPatientSpiderData();
        /*Invoke("InitializeFirebase",0.1f);
        Invoke("CountPatientSpiderData",0.2f);*/
        //auth = FirebaseAuth.DefaultInstance;
    }
    Questionnaire questionnaire = new Questionnaire();
    
    public void CountPatientSpiderData()
    {
        //reference.Child("Questionnaires").Child(user.UserId).Child("Spider").GetValueAsync().ContinueWithOnMainThread(task =>

        reference.Child("Questionnaires").Child(user.UserId).Child("Spider").GetValueAsync().ContinueWithOnMainThread(task =>
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
        });

    }

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


    public void submitQuestionnaire()
    {
        string sumNum = (Q1.value + Q2.value + Q3.value + Q4.value + Q5.value + Q6.value + Q7.value +
                         Q8.value + Q9.value + Q10.value + Q11.value + Q12.value + Q13.value + Q14.value +
                         Q15.value + Q16.value + Q17.value + Q18.value).ToString();
        
        /*
        Debug.Log(Q1.value + Q2.value + Q3.value + Q4.value + Q5.value + Q6.value + Q7.value + 
                  Q8.value + Q9.value + Q10.value + Q11.value + Q12.value + Q13.value + Q14.value + 
                  Q15.value + Q16.value + Q17.value + Q18.value);
                  */
        /*float phobiaNum = Q1.value + Q2.value + Q3.value + Q4.value + Q5.value + Q6.value + Q7.value +
                          Q8.value + Q9.value + Q10.value + Q11.value + Q12.value + Q13.value + Q14.value +
                          Q15.value + Q16.value + Q17.value + Q18.value;*/
        
        string currentdatetime = DateTime.Now.ToString("yyyy-MM-ddTHH:mm:ss");
        questionnaire.AnswerDateTime = currentdatetime;
        questionnaire.AnswerScore = sumNum;
        string json = JsonUtility.ToJson(questionnaire);
        //string currentdatetime = DateTime.Now.ToString("yyyy’-‘MM’-‘dd’T’HH’:’mm’:’ss");
        //questionnaire.AnswerDateTime = currentdatetime;
        
        Debug.Log(currentdatetime);
        var user = auth.CurrentUser;
        reference.Child("Questionnaires").Child(user.UserId).Child("Spider").Child(iterateNum.ToString()).SetRawJsonValueAsync(json).ContinueWith(task => 
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
        SceneManager.LoadScene("SpiderPhobiaMenu");
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
