using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using UnityEngine;

public class JSONReadandWrite : MonoBehaviour
{

    //private List<Patient> patients = new List<Patient>();
    /*public class PatientData
    {
        public int Id;
        public string Scenario;
        public int EvalChoice;
        public string CurrentDate;
        public string CurrentTime;
    }*/
    // public int IdInput = 0;
    public string ScenarioInput;
    public int InsectRealismInput;
    public int EvalInput;
    public string DateInput;
    public string TimeInput;

    public string txt;
    //public string filePath;
    //public TextAsset JsonFile;
    
    //private PatientData[] patientInstance = new PatientData[3];
    // SaveToJson (passed data from public - passing from testingform)
    // save to Json creates the directory file and an empty array
    public void SaveToJson()
    {
        DateInput = System.DateTime.Now.ToString();
        TimeInput = System.DateTime.Now.ToString();
        /*PatientCollection pat = JsonUtility.FromJson<PatientCollection>(json);
        List<PatientData> listOfPatients = pat.patients.ToList();        
        
        //PatientCollection pat = JsonUtility.FromJson<PatientCollection>(JsonFile.text);
        
        listOfPatients.Add(new PatientData { Id = 3, Scenario = "channel03", EvalChoice = 1, CurrentTime = TimeInput, CurrentDate = DateInput});

        // Revert back to array
        pat.patients = listOfPatients.ToArray();

        // This will be the new json
        string newJsonString = JsonUtility.ToJson(pat);*/
        /*PatientData data = new PatientData();
        data.Id = IdInput;
        data.Scenario = ScenarioInput;
        data.EvalChoice = EvalInput;
        data.CurrentDate = DateInput;
        data.CurrentTime = TimeInput;*/
        /*patientInstance[1] = new PatientData();
        patientInstance[1].Id = IdInput;
        patientInstance[1].Scenario = ScenarioInput;
        patientInstance[1].EvalChoice = EvalInput;
        patientInstance[1].CurrentDate = DateInput;
        patientInstance[1].CurrentTime = DateInput;*/
        /*patientInstance[1] = new PatientData();
        patientInstance[1].Id = IdInput + 1;
        patientInstance[1].Scenario = ScenarioInput;
        patientInstance[1].EvalChoice = EvalInput;
        patientInstance[1].CurrentDate = DateInput;
        patientInstance[1].CurrentTime = DateInput;*/
        /*PatientCollection newF = new PatientCollection();
        newF.*/
        // get filepath
       // #if UNITY_EDITOR
       // string filePath = Application.dataPath + "/SpiderPatientDataFile.json";
        //#endif
        /*#if UNITY_EDITOR
         string filePath = Application.dataPath + "/SpiderTestPatientDataFile.json";
        #endif*/
        /*#if UNITY_ANDROID
            //string filePath2 = Application.persistentDataPath + "/SpiderTestPatientDataFile.json";
            string  filePath2 = Path.Combine(Application.persistentDataPath, "SpiderTestPatientDataFile.json");
        #endif*/
        //#if UNITY_ANDROID
       //string filePath = Application.persistentDataPath + "SpiderPatientDataFile.json";
        //#endif
        //string playerToJson = JsonHelper.ToJson(patientInstance, true);
        // string json1 = JsonUtility.ToJson(patientInstance, true);
        // the "/" is needed 
       // string filePath = Application.dataPath + "/PatientDataFile.json";
       //string json = File.ReadAllText(Application.dataPath + "/PatientDataFile.json");
        #if UNITY_EDITOR
        string filePath = Application.dataPath + "/SpiderTestPatientDataFile.json";
        if (!File.Exists(filePath))
        {
            // if a file with the name does not exist, create a new file
            Directory.CreateDirectory(Path.GetDirectoryName(filePath));
            //PatientCollection pat = JsonUtility.FromJson<PatientCollection>(JsonFile.text);
            //Debug.Log(GetType().Name + "-Creating new Json...");
            //Directory.CreateDirectory(Path.GetDirectoryName(filePath));
            /*PatientData newF = new PatientData();
            newF.Id = 1;
            newF.Scenario = "derpyDan";
            newF.EvalChoice = 1;
            patientInstance[1].CurrentDate = DateInput;
            patientInstance[1].CurrentTime = DateInput;
            */
            PatientCollection newPat = new PatientCollection();
            //newPat.username = "testingspider";
            
            // create Json array (empty) this should only run once - move it to initialize 
            // if(!File.Exists(filePath) prevents the overwriting of the existing array because it checks if the file already exists, and if it does, it does not make a new empty array
            newPat.patients = new PatientData[0];
            /*PatientData thisQ = new PatientData();
            thisQ.Id = 1;
            thisQ.Scenario = "question.question";
            thisQ.EvalChoice = 1;
            thisQ.CurrentDate = DateInput;
            thisQ.CurrentTime = DateInput;*/
            string parentData = JsonUtility.ToJson(newPat, true);
            File.WriteAllText(filePath, parentData);
        }
        #endif
        #if UNITY_ANDROID
        string filePath2 = Path.Combine(Application.persistentDataPath, "SpiderTestPatientDataFile.json");
        if (!File.Exists(filePath2))
        {
            // if a file with the name does not exist, create a new file
            Directory.CreateDirectory(Path.GetDirectoryName(filePath2));
            //PatientCollection pat = JsonUtility.FromJson<PatientCollection>(JsonFile.text);
            //Debug.Log(GetType().Name + "-Creating new Json...");
            //Directory.CreateDirectory(Path.GetDirectoryName(filePath));
            /*PatientData newF = new PatientData();
            newF.Id = 1;
            newF.Scenario = "derpyDan";
            newF.EvalChoice = 1;
            patientInstance[1].CurrentDate = DateInput;
            patientInstance[1].CurrentTime = DateInput;
            */
            PatientCollection newPat = new PatientCollection();
            //newPat.username = "testingspider";
            // create Json array (empty) this should only run once - move it to initialize 
            // if(!File.Exists(filePath) prevents the overwriting of the existing array because it checks if the file already exists, and if it does, it does not make a new empty array
            newPat.patients = new PatientData[0];
            /*PatientData thisQ = new PatientData();
            thisQ.Id = 1;
            thisQ.Scenario = "question.question";
            thisQ.EvalChoice = 1;
            thisQ.CurrentDate = DateInput;
            thisQ.CurrentTime = DateInput;*/
            string parentData = JsonUtility.ToJson(newPat, true);
            File.WriteAllText(filePath2, parentData);
        }
        #endif
        /*Debug.Log("hello");
         PatientData thisQ = new PatientData();
         thisQ.Id = 1;
         thisQ.Scenario = "question.question";
         thisQ.EvalChoice = 1;
         thisQ.CurrentDate = DateInput;
         thisQ.CurrentTime = DateInput;
         string parentData = JsonUtility.ToJson(thisQ, true);
         File.WriteAllText(filePath, parentData);*/
           /*Debug.Log("hello");
           PatientData thisQ = new PatientData();
           thisQ.Id = 1;
           thisQ.Scenario = "question.question";
           thisQ.EvalChoice = 1;
           thisQ.CurrentDate = DateInput;
           thisQ.CurrentTime = DateInput;*/
           /*QuestionSchema thisQ = new QuestionSchema();
           thisQ.questionNo = 1;
           thisQ.question = question.question;
           thisQ.givenAnswer = answer;*/
           // File.WriteAllText(Application.dataPath + "/PatientDataFile.json", json);
        //File.AppendAllText(Application.dataPath + "/PatientDataFile.json", playerToJson);
    }

    // saving actually takes data from other classes and saves it into the array
    public void Saving(String ScenarioInput, int InsectRealismInput, int EvalInput)
    {
        DateInput = System.DateTime.Now.ToString();
        TimeInput = System.DateTime.Now.ToString();
        
        #if UNITY_EDITOR
        string filePath = Application.dataPath + "/SpiderTestPatientDataFile.json";
        string json = File.ReadAllText(Application.dataPath + "/SpiderTestPatientDataFile.json");
        
        //string filePath2 = Application.dataPath + "/SpiderTestPatientDataFile.json";
        Debug.Log(json);
        PatientCollection pat = JsonUtility.FromJson<PatientCollection>(json);
        
        // Convert to list so we can add new entries
        List<PatientData> listOfChannels = pat.patients.ToList();

        // Add new entry
        //listOfChannels.Add(new PatientData { Id = 3, Scenario = "channel03", EvalChoice = 1, CurrentDate = "test", CurrentTime = "test"});
        listOfChannels.Add(new PatientData {Scenario = ScenarioInput, InsectRealism = InsectRealismInput, EvalChoice = EvalInput, CurrentDate = DateInput, CurrentTime = TimeInput});

        // Revert back to array
        pat.patients = listOfChannels.ToArray();

        // This will be the new json
        string newJsonString = JsonUtility.ToJson(pat, true);
        Debug.Log(newJsonString);
        
        // string parentData = JsonUtility.ToJson(newPat, true);
        File.WriteAllText(filePath, newJsonString);
        #endif
        
        #if UNITY_ANDROID
                string filePath2 = Path.Combine(Application.persistentDataPath, "SpiderTestPatientDataFile.json");
                string json2 = File.ReadAllText(filePath2);
                
                //string filePath2 = Application.dataPath + "/SpiderTestPatientDataFile.json";
                Debug.Log(json2);
                PatientCollection pat2 = JsonUtility.FromJson<PatientCollection>(json2);
                
                // Convert to list so we can add new entries
                List<PatientData> listOfChannels2 = pat2.patients.ToList();

                // Add new entry
                //listOfChannels.Add(new PatientData { Id = 3, Scenario = "channel03", EvalChoice = 1, CurrentDate = "test", CurrentTime = "test"});
                listOfChannels2.Add(new PatientData {Scenario = ScenarioInput, InsectRealism = InsectRealismInput, EvalChoice = EvalInput, CurrentDate = DateInput, CurrentTime = TimeInput});

                // Revert back to array
                pat2.patients = listOfChannels2.ToArray();

                // This will be the new json
                string newJsonString2 = JsonUtility.ToJson(pat2, true);
                Debug.Log(newJsonString2);
                
                // string parentData = JsonUtility.ToJson(newPat, true);
                File.WriteAllText(filePath2, newJsonString2);
        #endif
    }
    //public void LoadFromJson()
    // changed from void to String in order to return String object
    
    // right now loads the date object
    public String LoadFromJson()
    {
        #if UNITY_EDITOR
        string json = File.ReadAllText(Application.dataPath + "/SpiderTestPatientDataFile.json");
        // Debug.Log(json);
        // when using JsonHelper,the top level array must be named "items"
            PatientData[] patient = JsonHelper.FromJson<PatientData>(json);
            Debug.Log(patient);
        // Debug.Log(patient[0].CurrentTime);
        // txt is a public string, so we save the CurrentTime of patient[0] into it
        /*if (0 < patient.Length)
            txt = "nodatayet";*/
            //return txt;
             //txt = patient[0].CurrentTime;
            // Debug.Log(patient[0].CurrentTime);
            //if (patient.ToString() == "PatientData[]")
            
            // if patient array has no data (patient.Length == 0)
            if (patient.Length == 0)
            {
                txt = "nodatacheck";
                Debug.Log(txt);
            }
            else
            {
                //txt = "small";
                txt = patient[0].CurrentTime;
            }
            // then return it and then we print it out as text string in testingform
            return txt;
            //Debug.Log(patient[1].CurrentTime);
            //List<PatientData> listOfChannels = pat.patients.ToList();

            //Debug.Log(patient[0].patients); 
            //Debug.Log(patient[1].CurrentDate);

            /*PatientData data = JsonUtility.FromJson<PatientData>(json);
            IdInput = data.Id;
            ScenarioInput = data.Scenario;
            EvalInput = data.EvalChoice;*/
            //DateTimeInput = data.CurrentDateTime;
        #endif
           
        #if UNITY_ANDROID
                string json2 = File.ReadAllText(Path.Combine(Application.persistentDataPath, "SpiderTestPatientDataFile.json"));
                Debug.Log(json2);
                // when using JsonHelper,the top level array must be named "items"
                PatientData[] patient2 = JsonHelper.FromJson<PatientData>(json2);
                // txt is a public string, so we save the CurrentTime of patient[0] into it
               if (patient2.Length == 0)
                {
                    txt = "nodatacheck";
                    Debug.Log(txt);
                }
                else
                {
                    //txt = "small";
                    txt = patient2[0].CurrentTime;
                }
                // txt = patient2[0].CurrentTime;
                // txt = "nodatayet";
                Debug.Log(txt);
                // then return it and then we print it out as text string in testingform
                return txt;
        #endif
        
    }
}
