using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

// Sử dụng cho việc hiển thị Text Mesh Pro khi kết thúc màn chơi
public class GUIScoreDisplay : MonoBehaviour
{
    private GameSessions gameSessions;
    // Use this for initialization
    void Start()
    {
        var tmp = GetComponent<TextMeshProUGUI>();
        gameSessions = FindObjectOfType<GameSessions>(); 
        tmp.text = gameSessions.GetScore().ToString(); 
    }
    // Update is called once per frame
    void Update()
    {
        
    }
}
