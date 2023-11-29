using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EffectsPool : MonoBehaviour
{
    [Header("����Ʈ ����")]
    public GameObject[] prefabs;
    public List<GameObject>[] pools;
    private void Awake()
    {
        pools = new List<GameObject>[prefabs.Length];
        for (int i = 0; i < pools.Length; i++)
        {
            pools[i] = new List<GameObject>();
        }
    }
    //����Ʈ Ǯ
    public GameObject Get(int index, Transform pos)
    {
        GameObject select = null;
        foreach (GameObject item in pools[index])
        {
            if (!item.activeSelf)
            {
                select = item;
                select.SetActive(true);
                break;
            }
        }

        if (!select)
        {
            select = Instantiate(prefabs[index], pos);
            pools[index].Add(select);
        }
        select.GetComponent<ParticleSystem>().Play();
        select.transform.parent = transform; //���� ��ġ �θ�

        //���� ��ġ ����
        switch (index)
        {
            case 0:
            case 1:
            case 2:
                select.transform.position = pos.position + new Vector3(0, -1f, 0); 
                break;
           case 3:
                select.transform.position = pos.position;
                break;
        }

        StartCoroutine(HitPsSetFalse(select, 2)); //�ð� ��� �� ��Ȱ��ȭ

        return select;
    }
    private IEnumerator HitPsSetFalse(GameObject select, float time) //��Ȱ��ȭ
    {
        yield return new WaitForSeconds(time);
        select.SetActive(false);
    }
}
