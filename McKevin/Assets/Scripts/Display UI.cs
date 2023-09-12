using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using TMPro;

public class DisplayUI : MonoBehaviour
{
    //private field that won't be used elsewhere but since it is Serialized I can change it in the 
    //inspector. Allowing me to access it outside of the script. 
    [SerializeField] TextMeshProUGUI txtQue; 
    [SerializeField] TextMeshProUGUI txtTime;
    [SerializeField] TextMeshProUGUI txtCash; // will be used implemented in the upcoming feature. 
    float GameTime = 600;
    float currentTime;
    public void Update()
    { 
        txtQue.text = "Customers: " + Orders.qLen;
        currentTime = GameTime - Time.time;
        txtTime.text = currentTime.ToString();
        txtCash.text = Orders.cash.ToString(); 
    }
}
