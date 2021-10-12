using System.Collections;
using System.Collections.Generic;
using Firebase.Database;
using Firebase.Extensions;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneSelectManager : MonoBehaviour
{
    Firebase.Auth.FirebaseAuth auth;
    Firebase.Auth.FirebaseUser user;
    DatabaseReference reference;
    public GameObject FirstFSQPanel;
    public GameObject FirstFCQPanel;
    void Start()
    {
        FirebaseDatabase.DefaultInstance.SetPersistenceEnabled(false);
        reference = FirebaseDatabase.DefaultInstance.RootReference;
        InitializeFirebase();
        FirstFSQPanel.SetActive(false);
        FirstFCQPanel.SetActive(false);
     }

    public void checkIfNoSpiderData()
    {
        reference.Child("Questionnaires").Child(user.UserId).Child("Spider").GetValueAsync().ContinueWithOnMainThread(task =>
        {
            if (task.IsCompleted)
            {
                DataSnapshot snapshot = task.Result;
                Debug.Log("Successful");
                if (snapshot.ChildrenCount == 0)
                {
                    Debug.Log("No FSQ data found for this user!");
                    FirstFSQPanel.SetActive(true);
                }
                else
                {
                    SceneManager.LoadScene("SpiderPhobiaMenu");
                }
            }
            else
            {
                Debug.Log("Unsuccessful");
            }
        });
    }
    
    public void checkIfNoCockroachData()
    {
        reference.Child("Questionnaires").Child(user.UserId).Child("Cockroach").GetValueAsync().ContinueWithOnMainThread(task =>
        {
            if (task.IsCompleted)
            {
                DataSnapshot snapshot = task.Result;
                Debug.Log("Successful");
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
                Debug.Log("Unsuccessful");
            }
        });
    }
    
    public void navigateToSpiderQuestionnare()
    {
        SceneManager.LoadScene("FirstSpiderEval");
    }
    
    public void navigateToCockroachQuestionnare()
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
