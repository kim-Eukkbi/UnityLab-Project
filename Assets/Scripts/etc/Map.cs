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
       
    }

    public void Reset()
	{
		// �ٽ� Ǯ�� �ö� �� ���� ���� ex) �浹ü ����, HP ���� ���
		// IResettable �������̽��� �⺻������ �ݵ�� �����ؾ� ������ ����.
	}
}
