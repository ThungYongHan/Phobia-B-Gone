using System.Collections;
using System.Collections.Generic;
using Firebase.Auth;
using Firebase.Database;
using Firebase.Extensions;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PhobiaSelectManager : MonoBehaviour
{
    private FirebaseAuth _auth;
    private FirebaseUser _user;
    private DatabaseReference _reference;
    public GameObject FirstFSQPanel, FirstFCQPanel;
    public TextMeshProUGUI errortextspider, errortextcockroach;
    void Start()
    {
        // prevent retrieval of deleted or old data
        FirebaseDatabase.DefaultInstance.SetPersistenceEnabled(false);
        _auth = FirebaseAuth.DefaultInstance;
        // get the root reference location of the firebase database
        _reference = FirebaseDatabase.DefaultInstance.RootReference;
        // get currently logged-in user
        _user = _auth.CurrentUser;

        // deactivate first fsq and fcq reminder panel
        FirstFSQPanel.SetActive(false);
        FirstFCQPanel.SetActive(false);
     }

    // check if logged-in user has existing fsq data
    public void checkIfNoSpiderData()
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
                    // show first fsq reminder panel so user can be
                    // led to fill in mandatory first questionnaire
                    FirstFSQPanel.SetActive(true);
                }
                else
                {
                    // load the arachnophobia menu scene 
                    SceneManager.LoadScene("SpiderPhobiaMenu");
                }
            }
            else
            {
                errortextspider.text = "Existing FSQ Data Checking Failed";
            }
        });
    }
    
    // check if logged-in user has existing fcq data
    public void checkIfNoCockroachData()
    {
        _reference.Child("Questionnaires").Child(_user.UserId).Child("Cockroach").GetValueAsync().ContinueWithOnMainThread(task =>
        {
            if (task.IsFaulted)
            {
                errortextcockroach.text = "Existing FCQ Data Checking Failed";
            }
            
            if (task.IsCompleted)
            {
                DataSnapshot snapshot = task.Result;
                if (snapshot.ChildrenCount == 0)
                {
                    Debug.Log("No FCQ data found for this user!");
                    FirstFCQPanel.SetActive(true);
                }
                else
                {
                    SceneManager.LoadScene("CockroachPhobiaMenu");
                }
            }
            else
            {
                errortextcockroach.text = "Existing FCQ Data Checking Failed";
            }
        });
    }
    
    // user clicks sign out button
    public void SignOut()
    {
        // removes currently logged-in user's credentials from this client
        _auth.SignOut();
        SceneManager.LoadScene("LogInSignUp");
    }
    public void navigateToSpiderQuestionnare()
    {
        SceneManager.LoadScene("FirstSpiderEval");
    }
    
    public void navigateToCockroachQuestionnare()
    {
        SceneManager.LoadScene("FirstCockroachEval");
    }
}
