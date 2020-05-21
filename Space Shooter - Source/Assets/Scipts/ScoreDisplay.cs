using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

// Sử dụng cho hiển thị điểm trong màn chơi
public class ScoreDisplay : MonoBehaviour
{
    private Text scoreDisplay;

    private GameSessions gameSessions;
	// Use this for initialization
	void Start ()
	{
	    scoreDisplay = GetComponent<Text>();
	    gameSessions = FindObjectOfType<GameSessions>();
	}
	// Update is called once per frame
    //Lấy điểm từ Game Session tham khảo
	void Update ()
	{
	    scoreDisplay.text = gameSessions.GetScore().ToString();
	}
}
