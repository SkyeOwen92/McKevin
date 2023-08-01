using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Copiable : MonoBehaviour
{ 
    public Transform newFood;
    public Transform Parent;
    bool copied = false;
    Vector3 spawnPoint;
    private void Start()
    {
        
    }
    private void OnMouseDown()
    {
        spawnPoint = new Vector3(Parent.position.x, Parent.position.y, Parent.position.z);
    }
    private void OnMouseUp()
    {
        if (!copied)
        {
            Instantiate(newFood, spawnPoint, newFood.rotation);
            copied = true;
        }
    }
}
