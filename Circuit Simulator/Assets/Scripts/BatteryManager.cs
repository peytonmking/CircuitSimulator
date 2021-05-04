using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BatteryManager : MonoBehaviour
{
    public void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Battery"))
        {
            PowerManager.instance.battery = true;
            ProcedureManager.instance.CheckSteps(BreadboardManager.instance);
        }
    }

    public void OnTriggerExit(Collider other)
    {
        if (PowerManager.instance.battery && other.CompareTag("Battery"))
        {
            PowerManager.instance.battery = false;
            ProcedureManager.instance.CheckSteps(BreadboardManager.instance);
        }
    }  
}
