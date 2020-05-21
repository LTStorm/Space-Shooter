using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;
using System.IO;
using UnityEngine.UI;

// Quản lí đăng nhập, đăng xuất
public class LogManager : MonoBehaviour
{
    private string Name;
    private string ID;
    private DataController dataControl;
    private LoadLevel loadLevel;
    private bool save = true;
    private string previousGameDataFilePath = "/StreamingAssets/LastLog.txt";

    // Khởi tạo và duy trì xuyên suốt duy nhất một đối tượng
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

    // Duy trì duy nhất một đối tượng
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

    // Cập nhật field Name
    public void SetName(InputField inputField)
    {
        Name = inputField.text;
    }

    // Cập nhật ID
    public void SetID(InputField inputField)
    {
        ID = inputField.text;
    }

    // Đăng kí bằng gửi yêu cầu đăng kí đến đối tượng data controller
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

    // Kiểm tra đã đăng xuất ở lần trước chưa
    private bool LoggedOut()
    {
        string filePath = Application.dataPath + previousGameDataFilePath;
        string previousGameData = "";
        if (File.Exists(filePath))
            previousGameData = File.ReadAllText(filePath);
        if (previousGameData == "Logged Out") return true;
        Name = previousGameData;
        return false;
    }

    // Đăng nhập bằng gửi yêu cầu đăng nhập đến đối tượng data controller
    public void LogIn()
    {
        if (Name.Length == 0 || ID.Length == 0) return;
        if (FindObjectOfType<DataController>().LogIn(Name, ID))
        {
            WriteToPreviousGameDataFile(Name);
            loadLevel.LoadNextScene();
        }
    }

    // Ghi lại trạng thái đã đăng xuất hay chưa, nếu chưa, ghi lại tên người dùng cuối
    private void WriteToPreviousGameDataFile(string dataToStore)
    {
        string filePath = Application.dataPath + previousGameDataFilePath;
        File.WriteAllText(filePath, dataToStore);
    }

    // Đăng xuất bằng gửi yêu cầu đăng xuất đến đối tượng data controller
    public void LogOut()
    {
        FindObjectOfType<DataController>().LogOut(save);
        WriteToPreviousGameDataFile("Logged Out");
        FindObjectOfType<LoadLevel>().LoadLogScene();
    }

}
