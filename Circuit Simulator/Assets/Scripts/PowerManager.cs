using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PowerManager : MonoBehaviour
{
    public static PowerManager instance;

    public float voltage;

    public Text voltageText;

    public GameObject LED;

    public bool power;

    public Material[] mats; 

    public bool battery;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else if (instance != this)
        {
            Debug.Log("Instance already exists, destroying object!");
            Destroy(this);
        }
    }

    public void IncreaseVoltage()
    {
        voltage += 1f;
        UpdateVoltageText();
    }


    public void DecreaseVoltage()
    {
        voltage -= 0.1f;
        UpdateVoltageText();
    }

    public void UpdateVoltageText()
    {
        voltageText.text = voltage + "V"; 
    }

    public void TogglePower()
    {
        BreadboardManager.instance.TogglePower();
        power = !power;
        if (power)
        {
            var temp = LED.GetComponent<MeshRenderer>().materials;
            temp[0] = mats[0];
            LED.GetComponent<MeshRenderer>().materials = temp;
        } else 
        {
            var temp = LED.GetComponent<MeshRenderer>().materials;
            temp[0] = mats[1];
            LED.GetComponent<MeshRenderer>().materials = temp;
        }
        ProcedureManager.instance.CheckSteps(BreadboardManager.instance);
    }

    // public void OnTriggerEnter(Collider other)
    // {
    //     if (other.CompareTag("Battery"))
    //     {
    //         Debug.Log("battery");
    //         battery = true;
    //         ProcedureManager.instance.CheckSteps();
    //     }
    // }

    // public void OnTriggerExit(Collider other)
    // {
    //     if (battery)
    //     {
    //         battery = false;
    //         ProcedureManager.instance.CheckSteps();
    //     }
    // }    
}
