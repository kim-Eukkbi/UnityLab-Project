using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;

    private void Awake()
    {
        if(instance != null)
        {
            Debug.Log("다수의 게임매니저가 실행중입니다");
        }
        instance = this;
    }


}
