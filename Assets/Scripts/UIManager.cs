using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class UIManager : MonoBehaviour
{
    private static UIManager instance;
    public static UIManager Instance
    {
        get
        {
            Init();
            return instance;
        }
    }

    [SerializeField] private TextMeshProUGUI lText;
    [SerializeField] private TextMeshProUGUI rText;
    [SerializeField] private TextMeshProUGUI qText;
    [SerializeField] private TextMeshProUGUI countText;
    [SerializeField] private TextMeshProUGUI scoreText;
    [SerializeField] private TextMeshProUGUI bigScoreText;

    void Awake()
    {
        Init();
    }

    private static void Init()
    {
        if (instance == null)
        {
            GameObject go = GameObject.Find("UIManager");
            if (go == null)
            {
                go = new GameObject { name = "UIManager" };
                go.AddComponent<UIManager>();
            }

            instance = go.GetComponent<UIManager>();
        }
    }

    public void RefreshUI(Problem problem)
    {
        lText.text = problem.A;
        rText.text = problem.B;
        qText.text = problem.sentence;
    }

    public void SetActiveUITexts(bool questionActive, bool countActive)
    {
        lText.transform.parent.gameObject.SetActive(questionActive);
        rText.transform.parent.gameObject.SetActive(questionActive);
        qText.transform.parent.gameObject.SetActive(questionActive);
        scoreText.transform.parent.gameObject.SetActive(questionActive);

        countText.gameObject.SetActive(countActive);
    }

    public void StartCount(string text, Color color)
    {
        countText.text = text;
        countText.color = color;
    }

    public void RefreshScore(int score)
    {
        scoreText.text = "점수 : " + score.ToString();
        bigScoreText.text = "점수 : " + score.ToString();
    }

    public void BigScoreTextActive()
    {
        bigScoreText.gameObject.SetActive(true);
    }
}
