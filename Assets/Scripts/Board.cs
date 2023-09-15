using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Board : MonoBehaviour
{
    [SerializeField]
    private GameObject player;
    [SerializeField]
    private BoxCollider colliderA;
    [SerializeField]
    private BoxCollider colliderB;
    public int speed;

    private Vector3 startPos = new Vector3(0, 0, -5);

    void OnEnable()
    {
        if (player == null) player = GameObject.Find("Player");
        transform.position = startPos;
    }

    void Update()
    {
        Move();
    }

    void Move()
    {
        transform.position = new Vector3(transform.position.x + speed * Time.deltaTime, 
            transform.position.y, transform.position.z);
    }

    public void DecideRightPath(Answer answer)
    {
        if (answer == Answer.A)
        {
            colliderA.enabled = false;
            colliderB.enabled = true;
        }
        else
        {
            colliderA.enabled = true;
            colliderB.enabled = false;
        }
    }

    public void BoardRefresh()
    {
        colliderA.enabled = true;
        colliderB.enabled = true;
        gameObject.SetActive(false);
        gameObject.transform.position = startPos;
    }
}
