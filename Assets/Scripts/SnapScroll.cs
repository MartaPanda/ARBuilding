using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class SnapScroll : MonoBehaviour
{
    public GameObject itemPrefab;
    [Range (1, 50)]
    public int itemCount;
    private GameObject item;

    [NonSerialized]
    public List<GameObject> items = new List<GameObject>();

    public void Start()
    {
        for (int i = 0; i < itemCount; i++)
        {
            item = Instantiate(itemPrefab, transform, false);
            if (items.Count == 0)
            {
                print("++");
                items.Add(item);

                continue;
            }
            //item.transform.position = new Vector3(0, 0, 0);
            else
            {
                var itemTransform = items.ElementAt(items.Count - 1).transform.localPosition;
                print(itemTransform);
                item.transform.localPosition = new Vector3(itemTransform.x + 10, itemTransform.y, 0);
                print($"item.transform.position {item.transform.position}");
            }
            items.Add(item);
        }
    }


}
