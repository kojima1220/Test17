using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mouse : MonoBehaviour
{
    private Vector3 screenPoint;
    private Vector3 offset;

    void OnMouseDown() {
        this.screenPoint = Camera.main.WorldToScreenPoint(transform.position);
        this.offset = transform.position - Camera.main.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, screenPoint.z));
    }

    void OnMouseDrag() {
        Vector3 currentScreenPoint = new Vector3(Input.mousePosition.x, Input.mousePosition.y, screenPoint.z);
        Vector3 currentPosition = Camera.main.ScreenToWorldPoint(currentScreenPoint) + this.offset;
        transform.position = currentPosition;
    }
    
    // void Click()
    // {
    //     if(Input.GetMouseButtonDown(0))
    //     {
    //         Transform myTransform = this.transform;
    //         Vector3 pos = myTransform.position;
    //         pos.x += 0;
    //         pos.y += 0.5f;
    //         pos.z += 0;
    //         myTransform.position = pos;
    //     }
    // }
}
