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
            // ���� �Ŵ����� ���� ����
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

        //UI ����

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
        // ���� ����
        ProblemSet(); // UI ������ �Լ��� ���� ����?
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
