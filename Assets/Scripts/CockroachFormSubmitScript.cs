using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Firebase.Database;
using Firebase.Auth;
using Firebase.Extensions;
using UnityEngine.SceneManagement;

public class CockroachFormSubmitScript : MonoBehaviour
{
    private FirebaseAuth _auth;
    private FirebaseUser _user;
    private DatabaseReference _reference;
    private int iterateNum = 0;
    public Slider Q1, Q2, Q3, Q4, Q5, Q6, Q7, Q8, Q9, Q10, Q11, Q12, Q13, Q14, Q15, Q16, Q17, Q18;
    
    
    void Start()
    {
        FirebaseDatabase.DefaultInstance.SetPersistenceEnabled(false);
        _auth = FirebaseAuth.DefaultInstance;
        _reference = FirebaseDatabase.DefaultInstance.RootReference;
        _user = _auth.CurrentUser;
        
        CountPatientCockroachData();
    }
    Questionnaire questionnaire = new Questionnaire();
    
    public void CountPatientCockroachData()
    {
        _reference.Child("Questionnaires").Child(_user.UserId).Child("Cockroach").GetValueAsync().ContinueWithOnMainThread(task =>
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

    public void submitFirstFCQ()
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
        _reference.Child("Questionnaires").Child(user.UserId).Child("Cockroach").Child(iterateNum.ToString()).SetRawJsonValueAsync(json).ContinueWith(task => 
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
        SceneManager.LoadScene("CockroachPhobiaMenu");
    }
    
    public void submitTreatmentFCQ()
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
        _reference.Child("Questionnaires").Child(user.UserId).Child("Cockroach").Child(iterateNum.ToString()).SetRawJsonValueAsync(json).ContinueWith(task => 
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
        SceneManager.LoadScene("TreatmentProgressCockroach");
    }
    
    public void quitApp()
    {
        Application.Quit();
        SceneManager.LoadScene("SignOutTreatmentProgressCockroach");
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
        _reference.Child("Questionnaires").Child(user.UserId).Child("Cockroach").Child(iterateNum.ToString()).SetRawJsonValueAsync(json).ContinueWith(task => 
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
        SceneManager.LoadScene("SignOutTreatmentProgressCockroach");
    }
    
    public void backFCQ()
    {
        SceneManager.LoadScene("TreatmentProgressCockroach");
    }
}
