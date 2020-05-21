using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

// Class Người chơi, quản lí người đang đăng nhập và chơi game, lưu dữ liệu khi chơi, sử dụng để lưu khi đăng xuất
public class User : MonoBehaviour
{
    public new string name = "";
    private string ID = "";
    private int highScorelv1 = 0;
    private int highScorelv2 = 0;
    private GameSessions gameSessions;
    
    // Use this for initialization
    public void Awake()
    {
        SetUpSingleton();
        gameSessions = FindObjectOfType<GameSessions>();
    }
    
    // Khởi tạo và duy trì duy nhất 1 đối tượng
    private void SetUpSingleton()
    {
        int numberUser = FindObjectsOfType<User>().Length;
        if (numberUser > 1)
        {
            Destroy(gameObject);
        }
        else
        {
            DontDestroyOnLoad(gameObject);
        }
    }

    // Lấy điểm cao level 1
    public int GetHighScorelv1()
    {
        return highScorelv1;       
    }

    // Lấy điểm cao level 2
    public int GetHighScorelv2()
    {
        return highScorelv2;
    }

    // Lấy ID
    public string GetID()
    {
        return ID;
    }

    // Thay đổi ID
    public void SetID(string ID)
    {
        this.ID = ID;
    }

    // Thay đổi điểm cao level 1
    public void SetHighScorelv1(int newHighScore)
    {
        highScorelv1 = newHighScore;
        FindObjectOfType<GameSessions>().SetHighScorelv1(highScorelv1);
    }

    // Thay đổi điểm cao level 2
    public void SetHighScorelv2(int newHighScore)
    {
        highScorelv2 = newHighScore;
        FindObjectOfType<GameSessions>().SetHighScorelv2(highScorelv2);
    }
    
    // Cập nhật điểm cao bằng dữ liệu mới nhất từ Game sesions
    public void HighScoreUpdate()
    {
        gameSessions = FindObjectOfType<GameSessions>();
        SetHighScorelv1(gameSessions.GetHighScorelv1());
        SetHighScorelv2(gameSessions.GetHighScorelv2());
        FindObjectOfType<DataController>().NowSave();
    }

    // Thiết lập lại khi người chơi đăng xuất, người dùng hiện tại không có
    public void Reset()
    {
        name = ID = "unknown player";
        highScorelv1 = highScorelv2 = 0;
        FindObjectOfType<GameSessions>().ResetHighScore();
    }

}
