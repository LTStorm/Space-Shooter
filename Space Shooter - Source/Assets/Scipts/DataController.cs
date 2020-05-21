using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;
using System.IO;
using UnityEngine.UI;

// Class giúp kiểm soát lưu trữ, truy xuất dữ liệu người dùng
public class DataController : MonoBehaviour
{
    private User user;
    private Data data = new Data();
    private string gameDataProjectFilePath;  // đường dẫn tới file dữ liệu người dùng

    // Tạo duy nhất một đối tượng
    public void Awake()
    {
        int numberDataController = FindObjectsOfType<DataController>().Length;
        if (numberDataController > 1)
        {
            Destroy(gameObject);
        }
        else
        {
            DontDestroyOnLoad(gameObject);
        }
        user = FindObjectOfType<User>();
    }

    // Đăng nhập với tên và ID đã gửi tới từ Log Manager
    public bool LogIn(string userName, string ID)
    {
        if (userName.Length == 0 || ID.Length == 0) return false;
        LoadGameData(userName);
        if (data.ID == ID.GetHashCode().ToString())
        {
            user.name = userName;
            return true;
        }
        return false;        
    }

    // Gọi khi người dùng chưa đăng xuất ở lần trước
    public bool SetUserLoggedIn(string userName)
    {
        if (userName.Length == 0) return false;
        LoadGameData(userName);
        FindObjectOfType<User>().name = userName;
        return true;
    }

    // Đăng kí với tên và ID đã gửi tới từ Log Manager
    public bool SignUp(string userName, string ID)
    {
        Debug.Log(ID);
        if (userName.Length == 0 || ID.Length == 0) return false;
        user.Reset();
        user.name = userName;
        user.SetID(ID.GetHashCode().ToString());
        SaveGameData();
        return true;
    }

    // Đăng xuất và lưu lại nếu save = true
    public bool LogOut(bool Save)
    {
        if (Save) SaveGameData();
        FindObjectOfType<LoadLevel>().LoadLogScene();
        user.Reset();
        return true;
    }

    // Load dữ liệu khi vào Game
    private void LoadGameData(string userName)
    {
        gameDataProjectFilePath = "/StreamingAssets/" + userName + ".json";
        string filePath = Application.dataPath + gameDataProjectFilePath;

        if (File.Exists(filePath))
        {
            string dataAsJson = File.ReadAllText(filePath);
            data = JsonUtility.FromJson<Data>(dataAsJson);
            user.SetHighScorelv1(data.highScorelv1);
            user.SetHighScorelv2(data.highScorelv2);
            user.SetID(data.ID);
        }
    }

    // Lưu ngay
    public void NowSave()
    {
        SaveGameData();
    }

    // Lưu dữ liệu mới
    private void SaveGameData()
    {
        data.Name = user.name;
        data.ID = user.GetID();
        data.highScorelv1 = user.GetHighScorelv1();
        data.highScorelv2 = user.GetHighScorelv2();
        gameDataProjectFilePath = "/StreamingAssets/" + user.name + ".json";
        string dataAsJson = JsonUtility.ToJson(data);
        string filePath = Application.dataPath + gameDataProjectFilePath;
        File.WriteAllText(filePath, dataAsJson);
    }

    void Update()
    {
        
    }
}