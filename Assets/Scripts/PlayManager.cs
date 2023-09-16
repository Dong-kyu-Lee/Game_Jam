using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayManager : MonoBehaviour
{
    [SerializeField]
    Board board;
    [SerializeField]
    ProblemMaker problemMaker;
    [SerializeField]
    GameObject player;
    [SerializeField]
    float speedIncreaseAmount;

    Vector3 playerStartPos = new Vector3(15, 1, -5);

    void Start()
    {
        board.gameObject.SetActive(false);
        StartCoroutine(InitialGameStartCoroutine());
    }

    void ProblemSet()
    {
        Problem problem = problemMaker.GetProblem();
        Debug.Log(problem.sentence + ", " + problem.answerWay);
        board.DecideRightPath(problem.answerWay);

        //UI 세팅
        UIManager.Instance.RefreshUI(problem);
        UIManager.Instance.SetActiveUITexts(true, false);
    }

    IEnumerator InitialGameStartCoroutine()
    {
        UIManager.Instance.SetActiveUITexts(false, true);
        UIManager.Instance.RefreshScore(GameManager.Instance.GetScore());

        yield return new WaitForSeconds(1);
        UIManager.Instance.StartCount("3", Color.green);
        yield return new WaitForSeconds(1);
        UIManager.Instance.StartCount("2", Color.yellow);
        yield return new WaitForSeconds(1);
        UIManager.Instance.StartCount("1", Color.red);
        yield return new WaitForSeconds(1);
        UIManager.Instance.StartCount("Go!", Color.red);
        yield return new WaitForSeconds(0.5f);

        board.gameObject.SetActive(true);
        ProblemSet();
    }

    IEnumerator GameStartCoroutine()
    {
        player.transform.position = playerStartPos;
        player.transform.rotation = Quaternion.Euler(0, 0, 0);
        player.GetComponent<Rigidbody>().freezeRotation = true;
        board.BoardRefresh(); // Board의 SetActive = false

        UIManager.Instance.StartCount("Great!", Color.blue);
        yield return new WaitForSeconds(1);
        UIManager.Instance.StartCount("Next!", Color.green);
        yield return new WaitForSeconds(0.5f);

        player.GetComponent<Rigidbody>().freezeRotation = false;
        board.gameObject.SetActive(true);
        ProblemSet();
    }

    private void GameRefresh()
    {
        // 점수 증가
        board.EnhanceSpeed(speedIncreaseAmount);
        GameManager.Instance.AddScore(100);
        UIManager.Instance.RefreshScore(GameManager.Instance.GetScore());
        UIManager.Instance.SetActiveUITexts(false, true);
        StartCoroutine(GameStartCoroutine());
    }

    private void GameOver()
    {
        StartCoroutine(GameOverCoroutine());
    }

    IEnumerator GameOverCoroutine()
    {
        player.gameObject.SetActive(false);
        board.gameObject.SetActive(false);

        UIManager.Instance.RefreshScore(GameManager.Instance.GetScore());
        UIManager.Instance.StartCount("Game Over", Color.red);
        yield return new WaitForSeconds(0.5f);
        UIManager.Instance.SetActiveUITexts(false, true);
        yield return new WaitForSeconds(0.5f);
        UIManager.Instance.BigScoreTextActive();
    }

    private void OnTriggerEnter(Collider other)
    {
        UIManager.Instance.SetActiveUITexts(false, false);

        if (other.transform.CompareTag("Player")) // 오답
        {
            GameOver();
        }
        else if (other.transform.CompareTag("Board")) // 정답
        {
            GameRefresh();
        }
    }
}
