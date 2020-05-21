using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class LevelPlayed : MonoBehaviour
{
    private GameSessions gameSessions;
    // Use this for initialization
    void Start()
    {
        string levelPlayed = "Level ";
        var tmp = GetComponent<TextMeshProUGUI>();
        gameSessions = FindObjectOfType<GameSessions>();
        levelPlayed += gameSessions.level.ToString();
        levelPlayed += " Played";
        tmp.text = levelPlayed;
    }
    // Update is called once per frame
    void Update()
    {

    }
}
