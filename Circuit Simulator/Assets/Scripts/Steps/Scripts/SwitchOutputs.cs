using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "CircuitSimulator/Step/SwitchOutputs")]
public class SwitchOutputs : Step
{
    public string choice;
    public override bool CheckComplete(BreadboardManager bm)
    {
        if (choice == "a")
        {
            foreach(KeyValuePair<int, CircuitComponent> component in bm.wires)
            {
                if (component.Value.inputPositions[0,0] == 1 || component.Value.outputPositions[0,0] == 1 && component.Value.inputPositions[0,1] >= 7 && component.Value.inputPositions[0,1] <= 11)
                {
                    return true;
                }

            }
        }
        else
        {
            foreach(KeyValuePair<int, CircuitComponent> component in bm.wires)
            {
                if (component.Value.inputPositions[0,0] == 2 || component.Value.outputPositions[0,0] == 2 && component.Value.inputPositions[0,1] >= 7 && component.Value.inputPositions[0,1] <= 11)
                {
                    return true;
                }

            }
        }




        return false;
    }
}
