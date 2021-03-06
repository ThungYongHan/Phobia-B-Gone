/*
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Firebase.Database;
using UnityEngine.UI;

public class realtimedatabase : MonoBehaviour
{
    DatabaseReference reference;

    [SerializeField] InputField username;
    [SerializeField] InputField email;
    
    [SerializeField] InputField nametoread;
    [SerializeField] Text data;


    // Start is called before the first frame update
    void Start()
    {
        reference = FirebaseDatabase.DefaultInstance.RootReference;
    }
    User user = new User();
    
    public void savedata()
    {
        user.UserName = username.text;
        user.Email = email.text;
        string json = JsonUtility.ToJson(user);
        //reference.Child("User").Child("YongHan").Child("Questionnaire").SetRawJsonValueAsync(json).ContinueWith(task =>
        // reference.Child("User").Child(user.UserName).SetRawJsonValueAsync(json).ContinueWith(task =>
        // reference.Child("NewSession").SetRawJsonValueAsync(json); it works
        reference.Child("User").SetRawJsonValueAsync(json).ContinueWith(task => 
        {
            if (task.IsCompleted)
            {
                reference.Child("Sessions").SetRawJsonValueAsync(json);
                Debug.Log("Successfully added data to Firebase");
            }
            else
            {
                Debug.Log("Unsuccessful");
            }
        });
    }
    
    /*public void addData()
    {
        user.UserName = username.text;
        user.Email = email.text;
        string json = JsonUtility.ToJson(user);

        reference.Child("User").Child("YongHan").Push("info").SetRawJsonValueAsync(json).ContinueWith(task =>
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
    }#1#

    public void Read_Data()
    {
        reference.Child("User").Child(nametoread.text).GetValueAsync().ContinueWith(task =>
        {
            if (task.IsCompleted)
            {
                Debug.Log("successful");
                DataSnapshot snapshot = task.Result;
                //Debug.Log(data.text = snapshot.Child("UserName").Value.ToString());
                Debug.Log(snapshot.Child("UserName").Value.ToString());
                Debug.Log(snapshot.Child("Email").Value.ToString());
            }
            else
            {
                Debug.Log("Unsuccessful");
            }

        });
    }
}
*/
