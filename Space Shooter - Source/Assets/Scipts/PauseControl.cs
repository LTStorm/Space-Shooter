using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;
using System.IO;
using UnityEngine.UI;

// Quản lí việc tạm ngưng màn chơi
class PauseControl : MonoBehaviour
{
    // Các đối tượng thường ẩn nhưng sẽ hiển thị chỉ khi Pause
    [SerializeField]GameObject[] OnPauseObjects;

    // Use this for initialization
    void Start()
    {
        hideOnPause_Object();
    }

    public void Pause()
    {
        if (Time.timeScale == 1)
        {
            Time.timeScale = 0; // Tạm ngưng mọi hoạt động
            showOnPause_Object(); // Hiển thị các thành phần chỉ hiển thị khi Pause
        }
    }

    // Trở lại game
    public void Resume()
    {
        if (Time.timeScale == 0)
        {
            Time.timeScale = 1; // Tiếp tục màn chơi
            hideOnPause_Object(); // Ẩn các thành phần chỉ hiển thị khi Pause
        }
    }

    public void toMainMenu()
    {
        Time.timeScale = 1; // Trả về để các Object hoạt động bình thường
        FindObjectOfType<LoadLevel>().LoadStartScene();
    }
    // Update is called once per frame
    void Update()
    {

    }

    // Tải lại
    public void Reload()
    {
        Time.timeScale = 1;
        FindObjectOfType<LoadLevel>().LoadLevelAgain();
    }

    // Hiển thị các thành phần chỉ hiển thị khi Pause
    private void showOnPause_Object()
    {
        foreach (GameObject g in OnPauseObjects)
        {
            g.SetActive(true);
        }
    }

    //Ẩn các thành phần chỉ hiển thị khi Pause 
    private void hideOnPause_Object()
    {
        foreach (GameObject g in OnPauseObjects)
        {
            g.SetActive(false);
        }
    }


}