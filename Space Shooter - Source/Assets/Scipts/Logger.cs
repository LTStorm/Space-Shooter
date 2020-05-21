using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;
using System.IO;
using UnityEngine.UI;

public class Logger : MonoBehaviour
{
    private string Name;
    private string ID;
    private DataController dataControl;
    private LoadLevel loadLevel;
    private bool save = true;
    private string previousGameDataFilePath = " / StreamingAssets / LastLog.txt";

    public void Awake()
    {
        SetUpSingleton();
        loadLevel = FindObjectOfType<LoadLevel>();
        dataControl = FindObjectOfType<DataController>();
        if (!LoggedOut())
        {
            if (dataControl.SetUserLoggedIn(Name)) loadLevel.LoadNextScene();
        }
    }

    private void SetUpSingleton()
    {
        int numberLogger = FindObjectsOfType<Logger>().Length;
        if (numberLogger > 1)
        {
            Destroy(gameObject);
        }
        else
        {
            DontDestroyOnLoad(gameObject);
        }
    }

    public void SetName(InputField inputField)
    {
        Name = inputField.text;
    }

    public void SetID(InputField inputField)
    {
        ID = inputField.text;
    }

    public void SignUp()
    {
        if (FindObjectOfType<DataController>().LogIn(Name, ID))
        {
            LogOut();
            return;
        }
        if (FindObjectOfType<DataController>().SignUp(Name, ID))
        {
            WriteToPreviousGameDataFile(Name);
            loadLevel.LoadNextScene();
        }
    }

    private bool LoggedOut()
    {
        string filePath = Application.dataPath + previousGameDataFilePath;
        string previousGameData = "";
        if (File.Exists("d:/a.json"))
            previousGameData = File.ReadAllText("d:/a.json");
        if (previousGameData == "Logged Out") return true;
        Name = previousGameData;
        return false;
    }

    public void LogIn()
    {
        Debug.Log(Name);
        Debug.Log(ID);
        if (FindObjectOfType<DataController>().LogIn(Name, ID))
        {
            WriteToPreviousGameDataFile(Name);
            loadLevel.LoadNextScene();
        }
    }

    private void WriteToPreviousGameDataFile(string dataToStore)
    {
        string filePath = Application.dataPath + previousGameDataFilePath;
        File.WriteAllText("d:/a.json", dataToStore);
    }
    public void LogOut()
    {
        FindObjectOfType<DataController>().LogOut(save);
        WriteToPreviousGameDataFile("Logged Out");
        FindObjectOfType<LoadLevel>().LoadLogScene();
    }

    void Update()
    {

    }
}
