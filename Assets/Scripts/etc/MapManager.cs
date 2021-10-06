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
            Debug.Log("�ټ��� �� �Ŵ����� �������Դϴ�");
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

        // ������ ��� ó���� Death �̺�Ʈ�ڵ鷯�� ���� ���� (Lambda�� ����)
        handler = (sender, e) => {
            pool.Release(map);
            map.Death -= handler;
            print("����ȣ~");
            mapCount--;
        };

        map.Death += handler;
        map.gameObject.SetActive(true);
        mapCount++;
    }
}
