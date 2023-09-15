using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayManager : MonoBehaviour
{
    private bool isGamePlaying = false;
    private bool isGameOver = false;

    [SerializeField]
    Board board;
    [SerializeField]
    ProblemMaker problemMaker;
    [SerializeField]
    GameObject player;

    void Start()
    {
        board.gameObject.SetActive(false);
        StartCoroutine(GameStartCoroutine());
    }

    void Update()
    {
        if(isGameOver == true)
        {
            // 게임 매니저에 점수 전달
            gameObject.SetActive(false);
        }

        if(isGamePlaying == false)
        {

        }
    }

    void ProblemSet()
    {
        Problem problem = problemMaker.GetProblem();
        Debug.Log(problem.sentence);
        board.DecideRightPath(problem.answerWay);

        //UI 세팅

    }

    IEnumerator GameStartCoroutine()
    {
        yield return new WaitForSeconds(1);
        Debug.Log("3");
        yield return new WaitForSeconds(1);
        Debug.Log("2");
        yield return new WaitForSeconds(1);
        Debug.Log("1");
        yield return new WaitForSeconds(1);
        Debug.Log("Go");
        yield return new WaitForSeconds(0.5f);

        board.gameObject.SetActive(true);
        ProblemSet();
        isGamePlaying = true;
    }

    private void GameRefresh()
    {
        // 점수 증가
        ProblemSet(); // UI 세팅을 함수로 따로 뺄까?
        board.BoardRefresh();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.transform.CompareTag("Player"))
        {
            GameRefresh();
        }
        else if(other.transform.CompareTag("Board"))
        {

        }
    }
}
