using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Pool;
using System;

public class MapManager : MonoBehaviour
{

    const int START_SIZE = 5;
    Pool<Map> pool;
    [SerializeField]
    private GameObject mapObj;

    PlayerController pc;

    float playerSpd = 5f;
    float dist = 6f;
   

    void Start()
    {
        pc = GameObject.Find("Player").GetComponent<PlayerController>();

        pool = new Pool<Map>(new PrefabFactory<Map>(mapObj), START_SIZE);
        for (int i = 0; i < pool.members.Count; i++)
        {
            pool.members[i].gameObject.SetActive(false);
        }
        Spawn();
    }

   

    void Spawn()
    {
        Map map = pool.Allocate();

        map.gameObject.transform.position += new Vector3(0, 0, pc.gameObject.transform.position.z + playerSpd * dist);


        EventHandler handler = null;

        // 생성시 사망 처리용 Death 이벤트핸들러를 직접 정의 (Lambda식 구현)
        handler = (sender, e) => {
            pool.Release(map);
            map.Death -= handler;
        };

        map.Death += handler;
        map.gameObject.SetActive(true);
    }
}
