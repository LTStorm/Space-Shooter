using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
//Class này được dùng để load các màn khác nhau trong trò chơi
public class LoadLevel : MonoBehaviour
{
    [SerializeField] private float waitSeceond = 2f;
    private GameSessions gameSessions;
    private string PausedSceneName;
	// Use this for initialization
	void Awake ()
    {
        gameSessions = FindObjectOfType<GameSessions>();
    }
	
	// Update is called once per frame
	void Update ()
    {
		
	}

    public void LoadNextScene()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex+1);
    }

    public void LoadLogScene()
    {
        gameSessions.ResetGame();
        SceneManager.LoadScene("Log");
    }

    public void LoadStartScene()
    {
        gameSessions.ResetGame();
        SceneManager.LoadScene("Start Game");
    }
    public void LoadChooseScene()
    {
        SceneManager.LoadScene("Choose Level");
    }

    public void LoadLevel1Scene()
    {
        FindObjectOfType<GameSessions>().level = 1;
        SceneManager.LoadScene("Level 1");
    }

    public void LoadLevel2Scene()
    {
        FindObjectOfType<GameSessions>().level = 2;
        SceneManager.LoadScene("Level 2");
    }

    public void LoadGameOver()
    {
        gameSessions.UpdateHighScore();
        StartCoroutine(WaitAndLoad());
    }

    public void LoadLevelAgain()
    {
        gameSessions.ResetGame();
        if (gameSessions.level == 1) LoadLevel1Scene();
        else LoadLevel2Scene();
    }

    IEnumerator WaitAndLoad()
    {
        yield return new WaitForSeconds(waitSeceond);
        SceneManager.LoadScene("Game Over");
    }

    public void LoadWinScene()
    {
        SceneManager.LoadScene("Win Scene");   
    }
    public void QuitGame()
    {
        Application.Quit();
    }
}
