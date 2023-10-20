using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    Rigidbody rigid;
    
    [SerializeField] float speed;
    [SerializeField] float maxVelocity;

    void Start()
    {
        rigid = GetComponent<Rigidbody>();
    }

    private void Update()
    {
        if(Input.GetButtonUp("Horizontal"))
        {
            rigid.velocity = Vector3.zero;
        }
    }

    void FixedUpdate()
    {
        float h = Input.GetAxisRaw("Horizontal");

        if (rigid.velocity.magnitude <= maxVelocity)
        {
            Vector3 direction = new Vector3(0, 0, h).normalized;
            rigid.AddForce(direction * speed * Time.deltaTime, ForceMode.Impulse);
            
            if (h < 0.1f && h > -0.1f)
            {
                rigid.AddForce(-1 * rigid.velocity * Time.deltaTime, ForceMode.Impulse);
            }
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.transform.CompareTag("Finish"))
            gameObject.SetActive(false);
    }
}
