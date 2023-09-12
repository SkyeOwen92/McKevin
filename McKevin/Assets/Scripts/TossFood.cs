using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TossFood : MonoBehaviour
{
    public GameObject Plate;
    public int OrderIndex;
    Vector3 pos;

    //when the button is pressed 
    private void OnMouseDown()
    {
        pos = transform.localPosition;
        transform.localPosition = new Vector3(pos.x, pos.y, pos.z - 0.1f); // pushes button down
        plateReset(); 
    }

    //pushes button up
    private void OnMouseUp()
    {
        transform.localPosition = pos; 
    }
    private void plateReset()
    {
        foreach (FoodItem item in Plate.GetComponent<BuildBurger>().myBurger) // get rid of burgers 
        {
            Destroy(item.gameObject);
        }
        Plate.GetComponent<BuildBurger>().myBurger.Clear(); // empty my list so we can put a new order on that plate
        Plate.GetComponent<BuildBurger>().burgerValue = 0; // reset burger value
    }
}
