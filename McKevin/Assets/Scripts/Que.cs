using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor.Experimental.GraphView;
using UnityEngine;

public class Que // not a mono behavoir but an object 
{
    Customer front;
    Customer rear;
    int size; 
    public Que()
    {
        front = null; rear = null;
        size = 0; 
    }
    public int Length()
    {
        return size;
    }
    public bool IsEmpty()
    {
        return size == 0;
    }
    public void EnQueue(Customer c)
    {
        Customer newest = c;
        if (IsEmpty())
        {
            front = newest;
        }
        else
        {
            rear.next = newest;
        }
        rear = newest;
        size++;
    }
    public Customer DeQueue()
    {
        if (IsEmpty())
        {
            Console.WriteLine("Q is Empty");
            return null; 
        }
        Customer nextUp = front;
        front = front.next;
        size--;
        if (IsEmpty())
        {
            rear = null;
        }
        return nextUp; 
    }
}
