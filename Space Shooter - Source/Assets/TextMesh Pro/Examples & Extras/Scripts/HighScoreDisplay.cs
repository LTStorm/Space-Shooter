using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class highScoreDisplay : MonoBehaviour
{
    private Text highScore;
    private GameSessions gameSessions;

    // Use this for initialization
    void Start()
    {
        highScore = GetComponent<Text>();
        gameSessions = FindObjectOfType<GameSessions>();
        int highScoreInt;
        if (gameSessions.level == 1)
            highScoreInt = gameSessions.GetHighScorelv1();
        else highScoreInt = gameSessions.GetHighScorelv2();
        this.highScore.text = highScoreInt.ToString();
    }
    // Update is called once per frame
    void Update()
    {
        
    }
}


