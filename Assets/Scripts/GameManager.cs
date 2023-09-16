using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

[System.Serializable]
public class PlayerInfo
{
    public string playerName;
    public string playerNumber;
    public int playerScore;

    public PlayerInfo(string nm, string nb)
    {
        playerName = nm;
        playerNumber = nb;
        playerScore = 0;
    }
}

public enum QuestionKind { Math, History, CommonSense }

public class GameManager : MonoBehaviour
{
    private static GameManager instance;
    public static GameManager Instance
    {
        get
        {
            return instance;
        }
    }

    PlayerInfo currentPlayer;

    private QuestionKind questionKind;
    public QuestionKind QuestionKind { get => questionKind; }

    void Awake()
    {
        Init();
    }

    static void Init()
    {
        if (instance == null)
        {
            GameObject go = GameObject.Find("GameManager");
            if (go == null)
            {
                go = new GameObject { name = "GameManager" };
                go.AddComponent<GameManager>();
            }

            DontDestroyOnLoad(go);
            instance = go.GetComponent<GameManager>();
        }
    }

    public void ExitButton()
    {
        Application.Quit();
    }

    public void SelectQuestionKind(int kind)
    {
        questionKind = (QuestionKind)kind;
    }

    public void CreateNewPlayer(string name, string number)
    {
        currentPlayer = new PlayerInfo(name, number);
        Debug.Log(currentPlayer.playerName + " " + currentPlayer.playerNumber);
    }

    public void ChangeScene(string sceneName)
    {
        SceneManager.LoadScene(sceneName);
    }

    public void AddScore(int score)
    {
        if (currentPlayer != null)
            currentPlayer.playerScore += score;
    }
    
    public int GetScore()
    {
        return currentPlayer.playerScore;
    }

    public void SavePlayerInfo()
    {
        if (currentPlayer != null)
            Debug.Log(currentPlayer.playerName + ", " + currentPlayer.playerNumber + ", " +
                currentPlayer.playerScore);
    }

    
}
