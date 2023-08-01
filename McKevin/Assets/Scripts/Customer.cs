using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Customer // not a mono behavior because it is an object that I will make an instance of. 
{ 
    public Customer next;
    public cats cat;
    public OrderObject order;

    public static Customer GenerateCustomer()
    {
        int ranNum = Random.Range(0, 3);
        Customer customer = new Customer();
        customer.next = null;
        return customer;
    }

}
