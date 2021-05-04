using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "CircuitSimulator/Step/ConnectWireStep")]
public class ConnectWire : Step
{
    public int charge;

    public string componentType;

    public override bool CheckComplete(BreadboardManager bm)
    {
        if (charge == 1)
        {

            if (componentType == "wire")
            {
                foreach(KeyValuePair<int, CircuitComponent> component in bm.wires)
                {
                    foreach(KeyValuePair<int, CircuitComponent> led in bm.leds)
                    {
                        if (component.Value.inputPositions[0,1] == charge && component.Value.outputPositions[0,0] == led.Value.outputPositions[0,0])
                        {
                            Debug.Log("returning true");
                            return true;
                        }
                    }
                }
            }
        }
        else
        {
            if (componentType == "wire")
            {
                foreach(KeyValuePair<int, CircuitComponent> component in bm.wires)
                {
                    foreach(KeyValuePair<int, CircuitComponent> led in bm.leds)
                    {
                        Debug.Log("-------------------------");
                        Debug.Log(component.Value.inputPositions[0,1] == charge);
                        Debug.Log(component.Value.outputPositions[0,0] == led.Value.inputPositions[0,0]);
                        Debug.Log(component.Value.inputPositions[0,1] == charge && component.Value.outputPositions[0,0] == led.Value.inputPositions[0,0]);
                        if (component.Value.inputPositions[0,1] == charge && component.Value.outputPositions[0,0] == led.Value.inputPositions[0,0])
                        {
                            Debug.Log("returning true");
                            return true;
                        }
                    }
                }
            }
        }
        return false;
    }
}
