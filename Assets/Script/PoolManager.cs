using System.Collections.Generic;
using UnityEngine;

public class PoolManager : MonoBehaviour
{
    public GameObject[] prefabs; // ��������� ������ ����.
    List<GameObject>[] pools; // Ǯ ����� �ϴ� ����Ʈ��

    void Awake()
    {
        pools = new List<GameObject>[prefabs.Length];
        // �ν����Ϳ��� �ʱ�ȭ
        for (int index = 0; index < pools.Length; index++)
            pools[index] = new List<GameObject>();
    }
    public GameObject Get(int index) //���� ������Ʈ ��ȯ �Լ�
    {
        GameObject select = null;
        //������ Ǯ�� ��Ȱ��ȭ �� ���� ������Ʈ ����.
        // �߰��ϸ� select ������ �Ҵ�// ������ ���� ������� 
        
        foreach (GameObject item in pools[index])
        {
            if (!item.activeSelf)
            {
                select = item;
                select.SetActive(true);
                break;
            }
        }
        // ��ã���� ���Ӱ� ������ �Ҵ�// ��� ���� ���� �ʰ� �������
        if (!select)
        {
            select = Instantiate(prefabs[index], transform);
            pools[index].Add(select);
        }

        return select;
    }
    public void Clear(int index)
    {
        foreach (GameObject item in pools[index])
            item.SetActive(false);
    }

    public void ClearAll()
    {
        for (int index = 0; index < pools.Length; index++)
            foreach (GameObject item in pools[index])
                item.SetActive(false);
    }
}
