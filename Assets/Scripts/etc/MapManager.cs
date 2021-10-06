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
    [SerializeField]
    private GameObject plane;

    public PlayerController pc;

    public Renderer rend;

    public Transform lastTrm;
    
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
        rend = plane.GetComponent<MeshRenderer>();
        lastTrm = pc.transform;
        pool = new Pool<Map>(new PrefabFactory<Map>(mapObj), START_SIZE);
        for (int i = 0; i < pool.members.Count; i++)
        {
            pool.members[i].gameObject.SetActive(false);
        }
        for (int i = 0; i < 10; i++)
        {
            Spawn();
        }
       
    }

    private void Update()
    {
        if (mapCount <= 10) 
        {
            Spawn();
        }
    }

    void Spawn()
    {
        Map map = pool.Allocate();

        EventHandler handler = null;

        rend.material.color = new Color(UnityEngine.Random.Range(1,100), UnityEngine.Random.Range(1, 100), UnityEngine.Random.Range(1, 100));
        map.transform.position = new Vector3(lastTrm.transform.position.x, lastTrm.transform.position.y, lastTrm.transform.position.z + rend.bounds.size.z);

        // 생성시 사망 처리용 Death 이벤트핸들러를 직접 정의 (Lambda식 구현)
        handler = (sender, e) => {
            pool.Release(map);
            map.Death -= handler;
            mapCount--;
        };
        lastTrm = map.transform;

        map.Death += handler;
        map.gameObject.SetActive(true);
        mapCount++;
    }
}
