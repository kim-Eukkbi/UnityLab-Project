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

    public List<Pool<Map>> poolList = new List<Pool<Map>>();


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
        pc = GameObject.Find("Player").GetComponentInChildren<PlayerController>();

        lastTrm = pc.transform;
        for (int i = 0; i < mapObjList.Count; i++)
        {
            poolList.Add(new Pool<Map>(new PrefabFactory<Map>(mapObjList[i]), START_SIZE));
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
        int idx = 0;
        do
        {
            idx = UnityEngine.Random.Range(0, mapObjList.Count - 1);
        } while (idx == fastIdx);

        if (mapCount == 0)
            idx = 9;
        Map map = poolList[idx].Allocate();
        

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
            poolList[idx].Release(map);
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
