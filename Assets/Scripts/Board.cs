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

    void Start()
    {
        if (player == null) player = GameObject.Find("Player");
        colliderA.enabled = false;
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

    void DecideRightPath(Answer answer)
    {
        if (answer == Answer.A) colliderA.enabled = false;
        else colliderB.enabled = false;
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.transform.CompareTag("Finish"))
        {
            if (player.activeInHierarchy == true)
                Debug.Log("Win");
            else Debug.Log("Dead");
        }
        colliderA.enabled = true;
        colliderB.enabled = true;
        gameObject.SetActive(false);
    }
}
