/*
using System.Collections;
using System.Collections.Generic;
using Firebase.Auth;
using UnityEngine;
using Firebase.Database;
using UnityEngine.UI;
using UnityEngine.XR.Management;

public class loadSpiderTreatmentDataVR : MonoBehaviour
{
    public Button testbutton;
    DatabaseReference reference;
    Firebase.Auth.FirebaseAuth auth;
    Firebase.Auth.FirebaseUser user;
    // Start is called before the first frame update
    void Start()
    {
        XRGeneralSettings.Instance.Manager.DeinitializeLoader();
        Screen.orientation = ScreenOrientation.Portrait;
        reference = FirebaseDatabase.DefaultInstance.RootReference;
        InitializeFirebase();
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
    
    public void LoadSpiderTreatmentData()
    {
        Debug.Log("clicktest");
        Debug.Log(auth.CurrentUser);
        var user = auth.CurrentUser;
        reference.Child("Questionnaire").Child(user.UserId).Child("Spider").Child("2021-09-27T22:10:04").GetValueAsync().ContinueWith(task =>
        {
            if (task.IsCompleted)
            {
                Debug.Log("successful");
                DataSnapshot snapshot = task.Result;
                //Debug.Log(snapshot.Key());
                Debug.Log(snapshot.Child("AnswerNum").Value.ToString());

                //Debug.Log(snapshot.Child("Email").Value.ToString());
            }
            else
            {
                Debug.Log("Unsuccessful");
            }

        });
    }
}
*/
