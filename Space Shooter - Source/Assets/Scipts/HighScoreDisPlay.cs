using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

// Sử dụng cho hiển thị điểm cao ở cuối màn chơi
public class HighScoreDisplay : MonoBehaviour
{ 
    private GameSessions gameSessions;

    // Use this for initialization
    void Start()
    {
        var tmp = GetComponent<TextMeshProUGUI>();

        // tham khảo đối tượng game Sessions
        gameSessions = FindObjectOfType<GameSessions>();
        int highScoreInt;

        // lấy dữ liệu 
        if (gameSessions.level == 1) 
            highScoreInt = gameSessions.GetHighScorelv1();
        else highScoreInt = gameSessions.GetHighScorelv2();

        tmp.text = highScoreInt.ToString();
    }
    // Update is called once per frame
    void Update()
    {

    }
}


