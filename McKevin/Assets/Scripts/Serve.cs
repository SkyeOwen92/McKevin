using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class Serve : MonoBehaviour
{
    public GameObject Plate;
    public int OrderIndex; 
    public static bool CorrectOrder = false;
    Vector3 pos;
    private void OnMouseDown()
    {
        pos = transform.localPosition;
        transform.localPosition = new Vector3(pos.x, pos.y, pos.z - 0.1f); // pushes button down
        if(Plate.GetComponent<BuildBurger>().burgerValue == Orders.CurrentOrders[OrderIndex].order.OrderValue) // compares burger to order
        {
            CorrectOrder = true;
            Orders.cash += Plate.GetComponent<BuildBurger>().GetBurgerCost();
            Plate.GetComponentInChildren<ParticleSystem>().Play(); //Play success confettii
            StartCoroutine(plateReset()); 
        }
    }
    private void OnMouseUp()
    {
        transform.localPosition = pos; //pushes button up
    }
    IEnumerator plateReset()
    {
        float time = .5f; //time so the cat can leave after having the food 
        yield return new WaitForSeconds(1);
        foreach (FoodItem item in Plate.GetComponent<BuildBurger>().myBurger) // get rid of burgers 
        {
            Destroy(item.gameObject);
        }
        cats go = Orders.CurrentOrders[OrderIndex].cat; // get rid of cat game object
        //float tip = go.GetComponent<Canvas>().GetComponent<Slider>().GetComponent<WaitBar>().tip;
        //Orders.cash += 10 + tip;
        Destroy(go.gameObject, time);
        Orders.CurrentOrders[OrderIndex] = null; // make the seat open
        Orders.timer = 0; // resets the timer to give time for the next customer
        Plate.GetComponent<BuildBurger>().myBurger.Clear(); // empty my list so we can put a new order on that plate
        Plate.GetComponent<BuildBurger>().burgerValue = 0; // reset burger value 
        Orders.boards[OrderIndex].GetComponent<MeshRenderer>().material.mainTexture = null;
        Orders.boards[OrderIndex].gameObject.SetActive(false);
        Plate.GetComponentInChildren<ParticleSystem>().Stop();
        Orders.qLen--; 

    }
    public static void ByeCat(int i) //customer leaves but the food on the plate stays. 
    {
        cats go = Orders.CurrentOrders[i].cat;
        Destroy(go.gameObject);
        Orders.CurrentOrders[i] = null;
        Orders.boards[i].GetComponent<MeshRenderer>().material.mainTexture = null;
        Orders.boards[i].gameObject.SetActive(false);
        Orders.timer = 0;
        Orders.qLen--; 
    }
}
