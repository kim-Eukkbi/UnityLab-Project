using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Pool;
using System;

public class Map : MonoBehaviour, IResettable
{
	public event EventHandler Death;
  

    private void Start()
    {
        Death += (sender, e) =>
        {
            this.gameObject.SetActive(false);
        };
    }

    public void Reset()
	{
		// 다시 풀로 올라갈 때 내용 구현 ex) 충돌체 리셋, HP 리셋 등등
		// IResettable 인터페이스의 기본형으로 반드시 구현해야 오류가 없음.
	}
    private void Update()
    {
       
        if (this.gameObject.transform.position.z < MapManager.instance.pc.gameObject.transform.position.z - 100)
        {
            Death?.Invoke(this, null);
        }
    }
}
