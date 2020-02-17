using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SelectionScript : MonoBehaviour
{
    public Text text;

    public void Update()
    {

        var touchPosition = Input.GetTouch(0).position;

        Ray ray = Camera.main.ScreenPointToRay(touchPosition);

        RaycastHit hitObject;
        if (Physics.Raycast(ray, out hitObject))
        {
            //PlaceObject selectedObj = hitObject.transform.GetComponent<GameObject>();

            print($" hit {hitObject.collider.gameObject.name}");
            text.text = hitObject.collider.gameObject.name;
        }


    }

}
