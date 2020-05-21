using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

// Script dùng cho khối hiển thị máu 
public class HealthDisplay : MonoBehaviour {

    private Text healthDisplay;

    private GameSessions gameSessions;
    // Use this for initialization
    void Start()
    {
        healthDisplay = GetComponent<Text>();
        gameSessions = FindObjectOfType<GameSessions>(); // Tìm kiếm và tham khảo đối tượng game Sessions
    }

    // Update is called once per frame
    void Update()
    {
        int health = gameSessions.GetHealth(); // Lấy dữ liệu từ game Sessions
        healthDisplay.text = health.ToString();
        if (health < 500) healthDisplay.color = Color.red; // Hiển thị mức độ nguy hiểm của người chơi bằng màu chữ
        else healthDisplay.color = Color.blue;
    }
}
