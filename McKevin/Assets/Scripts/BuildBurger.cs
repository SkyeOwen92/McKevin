using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics.CodeAnalysis;
using UnityEngine;

public class BuildBurger : MonoBehaviour
{
    public List<FoodItem> myBurger;
    public int burgerValue; // this is used for comparing burger on plate to the order 
    public int burgerPrice; // this is used for the cost of the burger 
    // Start is called before the first frame update
    void Awake()
    {
        myBurger.Clear();
        burgerValue = 0; 
        burgerPrice = 0;
    }

    // Update is called once per frame

    public void ReOrderBurger()
    {
        for (int i = myBurger.Count - 1; i > 0; i--)
        {
            FoodItem temp = myBurger[i];
            if (myBurger[i].placement < myBurger[i - 1].placement)
            {
                myBurger[i] = myBurger[i - 1];
                myBurger[i-1] = temp;
            }
            Vector3 location = myBurger[0].transform.position;
            float raise = 0; 
            foreach (FoodItem item in myBurger)
            {
                item.transform.position = new Vector3(location.x, location.y + raise, location.z);
                raise += .25f;
            }
        }
    }
    public void GetBurgerValue()
    {
        int value = 0;
        foreach(FoodItem item in myBurger)
        {
            value += item.value;
        }
        burgerValue = value;
    }
    public float GetBurgerCost()
    {
        float cost = 0;
        foreach (FoodItem item in myBurger)
        {
            cost += item.cost;
        }
        return cost;
    }
}
