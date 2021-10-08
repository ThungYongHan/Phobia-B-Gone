using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[System.Serializable]
// Object of this class will hold the data and then this object will be converted to JSON
// removed monobehavior
public class PatientData
{
    //public int Id;
    public string Scenario;
    public int InsectRealism;
    public int EvalChoice;
    public string CurrentDate;
    public string CurrentTime;
}

public class PatientCollection
{
    //public string username;
    public PatientData[] patients;
}