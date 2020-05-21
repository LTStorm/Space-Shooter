using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameSessions : MonoBehaviour
{
    [SerializeField] private int score;
    [SerializeField] private int health;
    [SerializeField] private int highScorelv1 = 0;
    [SerializeField] private int highScorelv2 = 0;
    public int level = 1;
    // Use this for initialization
    public void Awake()
    {
        SetUpSingleton();
    }

    private void SetUpSingleton()
    {
        int numberGameSessions = FindObjectsOfType<GameSessions>().Length;
        if (numberGameSessions > 1)
        {
            Destroy(gameObject);
        }
        else
        {
            DontDestroyOnLoad(gameObject);
        }
    }
    public int GetScore()
    {
        return score;
    }

    public int GetHealth()
    {
        return health;
    }
    public void AddToScore(int score)
    {
        this.score += score;
    }

    public void SetHealth(int health)
    {
        this.health = health;
    }

    public int GetHighScorelv1()
    {
        return highScorelv1;
    }

    public int GetHighScorelv2()
    {
        return highScorelv2;
    }

    public void SetHighScorelv1(int highScore)
    {
        highScorelv1 = highScore;
    }

    public void SetHighScorelv2(int highScore)
    {
        highScorelv2 = highScore;
    }

    public void UpdateHighScore()
    {
        if (level == 1 && score > highScorelv1) SetHighScorelv1(score);
        if (level == 2 && score > highScorelv2) SetHighScorelv2(score);
        FindObjectOfType<User>().HighScoreUpdate();
    }
    public void ResetGame()
    {
        score = 0;
    }

    public void ResetHighScore()
    {
        highScorelv1 = highScorelv2 = 0;
    }
    // Update is called once per frame
    void Update ()
    {
        
	}
}
