using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DropTrap : MonoBehaviour
{
    Animator anim;
    public Transform animParent;

    private void Start()
    {
        anim = animParent.GetComponent<Animator>();
    }

    public void PlayAnim()
    {
        anim.Play("FBX_Temp 1");
    }

    private void OnTriggerEnter(Collider col)
    {
        if(col.CompareTag("Player"))
        {
            PlayAnim();
        }
    }

}
