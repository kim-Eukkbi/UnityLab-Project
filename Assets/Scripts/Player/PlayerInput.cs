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
    private Animator anim;
    private int jumpCount =0;

    public void Start()
    {
        rid = gameObject.GetComponent<Rigidbody>();
        anim = gameObject.GetComponent<Animator>();
        Input.gyro.enabled = true;
    }
    public void Update()
    {
        Debug.Log(Input.gyro.attitude);
        //var gyroMoveA = Input.gyro.attitude;
        var gyroMoveR = Input.gyro.rotationRateUnbiased;
        rid.velocity = new Vector3(gyroMoveR.y * sensitivity, rid.velocity.y , speed);
        

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
                    anim.SetTrigger("DoubleJump");
                    JumpProcess();
                }
                else
                {
                    anim.SetTrigger("Jump");
                    JumpProcess();
                }
            }
            
        }
    }

    public void JumpProcess()
    {
        rid.AddForce(Vector3.up * jumpForce, ForceMode.Acceleration);
        jumpCount++;
    }

    public void OnCollisionEnter(Collision other)
    {
        if(other.gameObject.CompareTag("Ground"))
        {
            jumpCount = 0;
            anim.Play("Running");
        }
    }
}
