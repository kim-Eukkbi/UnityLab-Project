using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
    public float speed;
    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        gameObject.transform.LookAt(GameManager.instance.pc.transform);
        transform.position += transform.forward * speed * Time.deltaTime ;
    }
}
