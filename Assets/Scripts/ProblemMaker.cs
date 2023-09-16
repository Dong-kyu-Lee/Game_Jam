using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

public enum Answer { A, B }

[System.Serializable]
public struct Problem
{
    public string sentence; // 문제 문자열
    public string A, B; // 정답 문자열
    public Answer answerWay; // 정답 방향

    public Problem(string s, string a, string b, Answer way)
    {
        sentence = s;
        answerWay = way;
        A = a;
        B = b;
    }
}

[System.Serializable]
public class Problems
{
    public Problem[] problems;
}

public class ProblemMaker : MonoBehaviour
{
    [SerializeField]
    Problem[] problemData = null;
    [SerializeField]
    List<Problem> problemList = new List<Problem>();

    // 전체 경로 : D:\Unity_Folder\Game_Jam\Assets\Resources\Json\ProblemData.json
    private string path;
    private string mathDataPath;
    private string commonSenseDataPath;
    private string historyDataPath;

    void Start()
    {
        mathDataPath = Application.dataPath + "/Resources/Json/MathData.json";
        commonSenseDataPath = Application.dataPath + "/Resources/Json/CommonSenseData.json";
        historyDataPath = Application.dataPath + "/Resources/Json/HistoryData.json";

        path = null;
        QuestionKind kind = GameManager.Instance.QuestionKind;
        switch (kind)
        {
            case QuestionKind.Math:
                { path = mathDataPath; break; }
            case QuestionKind.History:
                { path = historyDataPath; break; }
            case QuestionKind.CommonSense:
                { path = commonSenseDataPath; break; }
            default:
                break;
        }

        string problemJsonData = File.ReadAllText(path);

        if (!File.Exists(path)) Debug.LogError("No Data");
        else
        {
            problemData = JsonUtility.FromJson<Problems>(problemJsonData).problems;
        }

        foreach (var item in problemData)
        {
            problemList.Add(item);
        }
    }

    public Problem GetProblem()
    {
        Problem problem = new Problem();

        if(problemList.Count != 0)
        {
            int randomIndex = Random.Range(0, problemList.Count - 1);
            problem = problemList[randomIndex];
            problemList.RemoveAt(randomIndex);

            if (problemList.Count == 0)
                RefreshProblems();
        }

        return problem;
    }

    void RefreshProblems()
    {
        foreach (var item in problemData)
        {
            problemList.Add(item);
        }

        Debug.Log("Problems refreshed");
    }

    void CreateProblemData()
    {
        List<Problem> datas = new List<Problem>();

        Problem p1 = new Problem("1 + 1 = ?", "1", "2", Answer.B);
        Problem p2 = new Problem("2 X 4 = ?", "8", "6", Answer.A);

        datas.Add(p1);
        datas.Add(p2);
        problemData = datas.ToArray();
        Problems tmpProblems = new Problems();
        tmpProblems.problems = problemData;

        string json = JsonUtility.ToJson(tmpProblems, true);
        File.WriteAllText(path, json);
    }
}
