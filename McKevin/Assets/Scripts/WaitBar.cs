using System.Collections;
using System.Collections.Generic;
using System.Drawing;
using UnityEngine;
using UnityEngine.UI; 

public class WaitBar : MonoBehaviour
{
    public float wait;
    public bool runtime = false; 
    public int sliderNum; 
    public Slider slider;
    public float tip; 

    public void EnableBar(float time, int i) //Makes wait Bar 
    {
        sliderNum = i; 
        slider.maxValue = time;
        wait = time;
        slider.value = wait;
        runtime = true;
        //tip = t; // implement later 
    }
    private void Update()
    {
        if (runtime)
        {
            if (wait > 0)
            {
                wait -= Time.deltaTime; // delta time is the actual seconds passed since last frame update
                slider.value = wait;
                if (wait <= (slider.maxValue/2)) // make Kevin look angry 
                {
                    slider.targetGraphic.color = UnityEngine.Color.red;
                    slider.gameObject.transform.Find("Anger").gameObject.SetActive(true);
                }
            }
        }
        if( wait <= 0f)
        {
            //fixed bug where the wrong cat was being deleted. Gave cat a seat number to fix wrong 
            //value being passed. 
            sliderNum = slider.GetComponentInParent<cats>().seatNumber; 
            Serve.ByeCat(sliderNum);
            wait = 6; //stops code from continuously exexcuiting 
            runtime = false;
        }
        
    }
}
