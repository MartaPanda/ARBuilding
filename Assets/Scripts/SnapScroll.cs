using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class SnapScroll : MonoBehaviour
{
    public GameObject itemPrefab;
    [Range (1, 50)]
    public int itemCount;
    [Range(0,20)]
    public float snapSpeed;
    public int itemOffset;
    public float scale=3;
    private GameObject item;
    private RectTransform contentRect;
    public int selectedItemID;

    private List<Vector3> itemsPosition = new List<Vector3>();
    [NonSerialized]
    public List<GameObject> items = new List<GameObject>();
    public bool isScrolling;
    private Vector3 scrollItemPosition;
    public Vector3[] scalePos;

    public void Start()
    {
        contentRect = GetComponent<RectTransform>();
        scalePos = new Vector3[itemCount];
        for (int i = 0; i < itemCount; i++)
        {
            item = Instantiate(itemPrefab, transform, false);
            if (items.Count == 0)
            {
                items.Add(item);
                itemsPosition.Add(-item.transform.localPosition);
                continue;
            }
            //item.transform.position = new Vector3(0, 0, 0);
            else
            {
                var itemTransform = items.ElementAt(items.Count - 1).transform.localPosition;
                print(itemTransform);
                item.transform.localPosition = new Vector3(itemTransform.x + itemOffset, itemTransform.y, 0);
                //print($"item.transform.position {item.transform.position}");
            }
            items.Add(item);
            //"-" because scroll item position is inverce(see recttransform position)
            itemsPosition.Add(-item.transform.localPosition);
        }
    }

    public void FixedUpdate()
    {
        float centralPos =float.MaxValue ;
        //find central item
        foreach (Vector3 i in itemsPosition)
        {
            float distance = Vector3.Distance(contentRect.anchoredPosition, i);
            //float distance = Math.Abs(contentRect.anchoredPosition.x - i.x);
            if (distance < centralPos)
            {
                centralPos = distance;
                selectedItemID = itemsPosition.IndexOf(i);

            float scaleItem = Mathf.Clamp(1/(distance/itemOffset)*scale, 0.5f, 4f); // zoom main icon!!!
            scalePos[selectedItemID].x = Mathf.SmoothStep(items.ElementAt(selectedItemID).transform.localScale.x, scaleItem, 6*Time.fixedDeltaTime);
            scalePos[selectedItemID].y = Mathf.SmoothStep(items.ElementAt(selectedItemID).transform.localScale.y, scaleItem, 6*Time.fixedDeltaTime);
            items.ElementAt(selectedItemID).transform.localScale = scalePos[selectedItemID];
            }
        }

        //place selected item in center of canvas
        if (isScrolling) return;
        scrollItemPosition.x = Mathf.SmoothStep(contentRect.anchoredPosition.x, itemsPosition.ElementAt(selectedItemID).x, snapSpeed*Time.fixedDeltaTime);
        contentRect.anchoredPosition = scrollItemPosition;
    }


    public void Scrolling(bool scrool)
    {
        isScrolling = scrool;
    }



}
