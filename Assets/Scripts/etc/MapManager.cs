using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Pool;
using System;

public class MapManager : MonoBehaviour
{
    public static MapManager instance;

    const int START_SIZE = 5;
    public Pool<Map> pool;
    public Dictionary<int,Map> mapDic;
    [SerializeField]
    private GameObject mapObj;

    public PlayerController pc;

    float playerSpd = 5f;
    float dist = 6f;

    int mapCount = 0;

    private void Awake()
    {
        if (instance != null) 
        {
            Debug.Log("다수의 맵 매니저가 실행중입니다");
        }
        instance = this;
    }
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

    private void Update()
    {
        if (mapCount <= 0) 
        {
            Spawn();
        }
    }

    void Spawn()
    {
        Map map = pool.Allocate();

        print(playerSpd * dist);
        map.gameObject.transform.position = new Vector3(pc.gameObject.transform.position.x, pc.gameObject.transform.position.y, pc.gameObject.transform.position.z + playerSpd * dist);


        EventHandler handler = null;

        // 생성시 사망 처리용 Death 이벤트핸들러를 직접 정의 (Lambda식 구현)
        handler = (sender, e) => {
            pool.Release(map);
            map.Death -= handler;
            print("무야호~");
            mapCount--;
        };

        map.Death += handler;
        map.gameObject.SetActive(true);
        mapCount++;
    }
}
