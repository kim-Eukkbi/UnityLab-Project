using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Pool;
using System;

public class MapManager : MonoBehaviour
{
    public static MapManager instance;



    #region Pool
    const int START_SIZE = 5;
    public Pool<Map> pool1;
    public Pool<Map> pool2;
    public Pool<Map> pool3;
    public Pool<Map> pool4;
    public Pool<Map> pool5;

    #endregion
    #region Map
    public Transform lastTrm;
    int mapCount = 0;
    [SerializeField]
    private List<GameObject> mapObjList = new List<GameObject>();
    #endregion

    public PlayerController pc;

    public Renderer rend;

    public int fastIdx = 0;
    
 

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

        lastTrm = pc.transform;
        pool1 = new Pool<Map>(new PrefabFactory<Map>(mapObjList[0]), START_SIZE);
        pool2 = new Pool<Map>(new PrefabFactory<Map>(mapObjList[1]), START_SIZE);
        pool3 = new Pool<Map>(new PrefabFactory<Map>(mapObjList[2]), START_SIZE);
        pool4 = new Pool<Map>(new PrefabFactory<Map>(mapObjList[3]), START_SIZE);
        pool5 = new Pool<Map>(new PrefabFactory<Map>(mapObjList[4]), START_SIZE);
       
        
       
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
        int idx = 0;
        do
        {
            idx = UnityEngine.Random.Range(0, mapObjList.Count - 1);
        } while (idx == fastIdx);

        Map map = pool1.Allocate();
        switch (idx)
        {
            case 0:
                map = pool1.Allocate();
                break;
            case 1:
                map = pool2.Allocate();
                break;
            case 2:
                map = pool3.Allocate();
                break;
            case 3:
                map = pool4.Allocate();
                break;
            case 4:
                map = pool5.Allocate();
                break;
        }

        EventHandler handler = null;
        Transform[] trms = map.gameObject.GetComponentsInChildren<Transform>();

        for (int i = 0; i < trms.Length; i++)
        {
            if (trms[i].gameObject.CompareTag("Ground"))
            {
                rend = trms[i].GetComponent<MeshRenderer>();
            }
        }

        if (mapCount == 0) 
        {
            map.transform.position = new Vector3(lastTrm.transform.position.x, lastTrm.transform.position.y, pc.transform.transform.position.z);

        }
        else
        {
            map.transform.position = new Vector3(lastTrm.transform.position.x, lastTrm.transform.position.y, lastTrm.transform.position.z + rend.bounds.size.z);
        }

        // 생성시 사망 처리용 Death 이벤트핸들러를 직접 정의 (Lambda식 구현)
        handler = (sender, e) => {
            //pool.Release(map);
            map.Death -= handler;
            mapCount--;
        };
        lastTrm = map.transform;

        map.Death += handler;
        map.gameObject.SetActive(true);
        mapCount++;
        fastIdx = idx;
    }
}
