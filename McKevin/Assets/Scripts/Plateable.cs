using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UIElements;

public class Plateable : MonoBehaviour
{
    
    public List<Transform> plates;
    Draggable curItem; 
    public List<FoodItem> foodItems;
    public float plateRange = 1f;
    
    public void Start()
    {
        GameObject[] gameObjects = GameObject.FindGameObjectsWithTag("Plate");
        foreach (GameObject gameObject in gameObjects)
        {
            plates.Add(gameObject.transform);
        }
        curItem = this.GetComponent<Draggable>();
        curItem.DragEndedCallBack = OnDragEnded; // assigns OnDragEnded to the callback which is a delegate. 

    }
    public void OnDragEnded(Draggable draggable) //the delegate points to this method 
    {
        float closestDistance = -1;
        Transform closestPlate = null; 
        foreach(Transform plate in plates)
        {
            float currentDistance = Vector3.Distance(draggable.transform.position, plate.position);
            if(closestPlate == null || currentDistance < closestDistance)
            {
                closestPlate = plate;
                closestDistance = currentDistance;
            }
        }
        if(closestPlate != null && closestDistance <= plateRange) // we build a burger a FoodItem is dropped onto the plate 
        {
            FoodItem fo = draggable.GetComponent<FoodItem>();
            if (!closestPlate.GetComponent<BuildBurger>().myBurger.Contains(fo))
            {
                closestPlate.GetComponent<BuildBurger>().myBurger.Add(fo);
                switch (closestPlate.GetComponent<BuildBurger>().myBurger.Count) // drops the food at different hights 
                {
                    case 1:
                        draggable.transform.position = closestPlate.position;
                        break;
                    case 2:
                        draggable.transform.position = new Vector3(closestPlate.position.x,
                                                                   closestPlate.position.y + 1f,
                                                                   closestPlate.position.z);
                        break;
                    case 3:
                        draggable.transform.position = new Vector3(closestPlate.position.x,
                                                                   closestPlate.position.y + 1.25f,
                                                                   closestPlate.position.z);
                        break;
                    case 4:
                        draggable.transform.position = new Vector3(closestPlate.position.x,
                                                                   closestPlate.position.y + 1.5f,
                                                                   closestPlate.position.z);
                        break;
                    case 5:
                        draggable.transform.position = new Vector3(closestPlate.position.x,
                                                                   closestPlate.position.y + 1.75f,
                                                                   closestPlate.position.z);
                        break;
                    case 6:
                        draggable.transform.position = new Vector3(closestPlate.position.x,
                                                                   closestPlate.position.y + 2f,
                                                                   closestPlate.position.z);
                        break;
                    default:
                        draggable.transform.position = new Vector3(closestPlate.position.x,
                                                                   closestPlate.position.y + 2.25f,
                                                                   closestPlate.position.z);
                        break;
                }
            }
            closestPlate.GetComponent<BuildBurger>().ReOrderBurger(); //reorder based on placement number 
            closestPlate.GetComponent<BuildBurger>().GetBurgerValue();
        }
        else
        {
            if(!gameObject.GetComponent<Grillable>())
            {
                Destroy(gameObject); 
            }
            
        }
     }
}
