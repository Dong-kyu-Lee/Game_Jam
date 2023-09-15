using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    [SerializeField]
    GameObject board;

    ProblemMaker problemMaker;

    void Start()
    {
        problemMaker = GetComponent<ProblemMaker>();
    }

    void Update()
    {
        
    }
}
