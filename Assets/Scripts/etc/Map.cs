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
		// �ٽ� Ǯ�� �ö� �� ���� ���� ex) �浹ü ����, HP ���� ���
		// IResettable �������̽��� �⺻������ �ݵ�� �����ؾ� ������ ����.
	}
    private void Update()
    {
       
        if (this.gameObject.transform.position.z < MapManager.instance.pc.gameObject.transform.position.z - 100)
        {
            Death?.Invoke(this, null);
        }
    }
}
