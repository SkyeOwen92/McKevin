using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Draggable : MonoBehaviour
{
    public delegate void DragEndedDelegate(Draggable draggableItems); // assign delegate sig
    public DragEndedDelegate DragEndedCallBack; //variable of delegate 
    Vector3 mousePosOffSet;
    private float mZCoord; 
    private Vector3 getMouseWorldPos()
    {
        Vector3 mousePoint = new Vector3(Input.mousePosition.x,Input.mousePosition.y +1f);
        mousePoint.z = mZCoord; 
        return Camera.main.ScreenToWorldPoint(mousePoint);
    }
    private void OnMouseDown()
    {
        mZCoord = Camera.main.WorldToScreenPoint(gameObject.transform.position).z;
        mousePosOffSet = gameObject.transform.position- getMouseWorldPos();
    }
    private void OnMouseDrag()
    {
        transform.position = getMouseWorldPos() + mousePosOffSet;
    }
    private void OnMouseUp()
    {
        DragEndedCallBack(this); // passes ref to current draggable object. 
        //since it is a delegate, the logic will be different if the draggable
        //object is plateable or grillable
    }
}

