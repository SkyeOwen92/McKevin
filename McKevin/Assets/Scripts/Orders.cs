using JetBrains.Rider.Unity.Editor;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI; 

public class Orders : MonoBehaviour
{
    //customer list is static so it can be accessed from anywhere
    public static Customer[] CurrentOrders = { null, null, null, null };
    //this is the boards for the food item; 
    public MeshRenderer[] currentBoard;
    //i had to copy the boards so I can access it in another class
    public static MeshRenderer[] boards = new MeshRenderer[4];
    public Texture[] orderPics;
    public List<OrderObject> OrderMenu = new List<OrderObject>();
    public static float cash;
    public cats[] ChooseCats; //array of Cat Prefabs 
    public Que q = new Que(); // Queue of Customers in line. 
    public static int qLen; 
    public Transform[] seats;// array of seat locations 
    public static float timer; 

    private void Start()
    { 
        //add object of type OrderObject so the can be added to the Menu
        //this lets up compare the menu item value to the value of the burger on our plates 
        OrderObject Hamburger = new OrderObject(100011);
        OrderObject CheeseBurger = new OrderObject(100111);
        OrderObject CheeseNBacon = new OrderObject(110111);
        OrderObject CheeseNTom = new OrderObject(101111);
        OrderObject CheeseTomBacon = new OrderObject(111111);
        OrderObject BurgNTom = new OrderObject(101011);
        OrderObject BurgNBacon = new OrderObject(110011);
        OrderObject BurTomBacon = new OrderObject(111011);
        OrderMenu.Add(Hamburger);
        OrderMenu.Add(CheeseBurger);
        OrderMenu.Add(CheeseNBacon);
        OrderMenu.Add(CheeseNTom);
        OrderMenu.Add(CheeseTomBacon);
        OrderMenu.Add(BurgNTom);
        OrderMenu.Add(BurgNBacon);
        OrderMenu.Add(BurTomBacon);
        // adding our board to the static array so it can be used in other classes with out having an instance. 
        for(int i = 0; i < 4; i++)
        {
            boards[i] = currentBoard[i]; 
        }
        //adding customers to the queue
        q.EnQueue(Customer.GenerateCustomer());
        q.EnQueue(Customer.GenerateCustomer());
        q.EnQueue(Customer.GenerateCustomer());
        q.EnQueue(Customer.GenerateCustomer());
        q.EnQueue(Customer.GenerateCustomer());
        q.EnQueue(Customer.GenerateCustomer());
        q.EnQueue(Customer.GenerateCustomer());
        q.EnQueue(Customer.GenerateCustomer());
        q.EnQueue(Customer.GenerateCustomer());
        q.EnQueue(Customer.GenerateCustomer());
        q.EnQueue(Customer.GenerateCustomer());
        q.EnQueue(Customer.GenerateCustomer());
        qLen = q.Length();
        DequeCustomer(0); // call first customer right away. The rest will come in 4 seconds for better game flow. 
    }
    private void Update()
    {
        timer += Time.deltaTime;    
        if ( timer > 4)
        {
            StartCoroutine(CallNextCustomer()); // after 4 seconds we will call the next customer if there is an open seat.
            timer = 0;
        }
    }
    IEnumerator CallNextCustomer()
    {
        yield return new WaitForSeconds(1f);
        if (CurrentOrders[0] == null && q.IsEmpty() != true)  // check for first empty seat
        {
            DequeCustomer(0);
        }
        else if (CurrentOrders[1] == null && q.IsEmpty() != true)
        {
            DequeCustomer(1);
        }
        else if (CurrentOrders[2] == null && q.IsEmpty() != true)
        {
            DequeCustomer(2);
        }
        else if (CurrentOrders[3] == null && q.IsEmpty() != true)
        {
            DequeCustomer(3);
        }
    }

    private void DequeCustomer(int i) // take customer out of the que and give them a seat 
    {
        Customer nextUp = q.DeQueue();
        CurrentOrders[i] = nextUp;
        cats kitty = getGO(); // gets a random cat as the customer
        Vector3 pos = seats[i].position; 
        nextUp.cat = Instantiate(kitty, pos, kitty.transform.rotation); // clone the chosen cat
        nextUp.order = GenerateOrder(i); // this gives you the order and puts the pic on the board 
        nextUp.cat.Time = nextUp.cat.GetComponent<cats>().Time;
        nextUp.cat.seatNumber = i;
        nextUp.cat.tip = nextUp.cat.GetComponent<cats>().tip; 
        kitty.timer.GetComponent<WaitBar>().EnableBar(nextUp.cat.Time, i);
    }

    public cats getGO()// gets a random cat as the customer
    {
        int ran = Random.Range(0, 3);
        return ChooseCats[ran];
    }
    public OrderObject GenerateOrder(int i)// this gives you the order and puts the pic on the board 
    {
        int randomNumber = Random.Range(0, 8);
        boards[i].gameObject.SetActive(true); // shows the order board
        boards[i].GetComponent<MeshRenderer>().material.mainTexture = orderPics[randomNumber]; // shows random photo
        return OrderMenu[randomNumber]; // returns the order of the customer
    }
}
