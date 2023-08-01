using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Grillable : MonoBehaviour
{
    public List<GameObject> Burners; 
    Draggable curItem; 
    public float burnerRange = 1f;
    private MeshRenderer meatColor;
    private string stillCooking = "n";
    ParticleSystem fire;

    public void Start()
    {
        meatColor = GetComponent<MeshRenderer>();
        GameObject[] gameObjects = GameObject.FindGameObjectsWithTag("Fire"); // adds burners to an array 
        foreach (GameObject gameObject in gameObjects) //puts that array into a list
        {
            Burners.Add(gameObject);
        }
        curItem = this.GetComponent<Draggable>(); // current item being dragged.
        curItem.DragEndedCallBack = OnDragEnded; // assigns OnDragEnded to the callback which is a delegate. 
    }
    IEnumerator CookTimer() //cooks burger and changes color. 
    {
        yield return new WaitForSeconds(10);
        if (stillCooking == "y")
        {
            meatColor.material.color = new Color(.3f, .3f, .3f);
            stillCooking = "n"; 
            fire.Stop();
            gameObject.GetComponent<Plateable>().enabled = true;
            gameObject.GetComponent<Grillable>().enabled = false;
        }
    }
    public void OnDragEnded(Draggable draggable) //the delegate points to this method 
    {
        float closestDistance = -1;
        GameObject closestBurner = null;
        foreach (GameObject burner in Burners) // find the closest burner
        {
            float currentDistance = Vector3.Distance(draggable.transform.position, burner.transform.position);
            if (closestBurner == null || currentDistance < closestDistance)
            {
                closestBurner = burner;
                closestDistance = currentDistance;
            }
        }
        if (closestBurner != null && closestDistance <= burnerRange) //if there is a close burner in range
        {
            draggable.transform.position = new Vector3(closestBurner.transform.position.x,
                                                       closestBurner.transform.position.y + .5f,
                                                       closestBurner.transform.position.z);
            //start cooking the burger and make it platable. 
            stillCooking = "y";
            StartCoroutine(CookTimer()); //this allows this IEnumerator to run at the same time as the rest of the code happens.
            fire = closestBurner.GetComponentInChildren<ParticleSystem>();
            fire.Play();
        }
        else
        {
            Destroy(gameObject);
        }
    }
}
