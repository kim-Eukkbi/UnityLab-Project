using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class PlayerInput : MonoBehaviour
{
    public float speed;
    public float jumpForce;

    private Rigidbody rid;
    private int jumpCount =0;

    public void Start()
    {
        rid = gameObject.GetComponent<Rigidbody>();
    }
    public void Update()
    {
        transform.position = new Vector3(transform.position.x - speed * Time.deltaTime, transform.position.y, transform.position.z);

        if(Input.GetMouseButtonDown(0))
        {
            if(jumpCount >= 2)
            {
                return;
            }
            else
            {
                if(jumpCount >= 1)
                {
                    rid.velocity = Vector3.zero;
                    rid.AddForce(Vector3.up * jumpForce, ForceMode.Acceleration);
                    jumpCount++;
                }
                else
                {
                    rid.AddForce(Vector3.up * jumpForce, ForceMode.Acceleration);
                    jumpCount++;
                }
            }
            
        }
    }

    public void OnCollisionEnter(Collision other)
    {
        if(other.gameObject.CompareTag("Ground"))
        {
            jumpCount = 0;
        }
    }
}
