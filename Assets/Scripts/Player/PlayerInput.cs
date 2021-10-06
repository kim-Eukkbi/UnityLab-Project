using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class PlayerInput : MonoBehaviour
{
    public float speed;
    public float jumpForce;
    public float sensitivity;

    private Rigidbody rid;
    private int jumpCount =0;

    public void Start()
    {
        rid = gameObject.GetComponent<Rigidbody>();
        Input.gyro.enabled = true;
    }
    public void Update()
    {
        Debug.Log(Input.gyro.attitude);
        //var gyroMoveA = Input.gyro.attitude;
        var gyroMoveR = Input.gyro.rotationRateUnbiased;
        transform.position = new Vector3(transform.position.x + gyroMoveR.y * sensitivity, transform.position.y, transform.position.z);
        transform.position = new Vector3(transform.position.x, transform.position.y, transform.position.z + speed * Time.deltaTime);

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
