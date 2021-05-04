using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "CircuitSimulator/Step/ICPower")]
public class ICPower : Step
{
    public bool boolone;
    public bool booltwo;
    public override bool CheckComplete(BreadboardManager bm)
    {
        foreach(KeyValuePair<int, CircuitComponent> component in bm.icsEight)
        {
            Debug.Log(BreadboardManager.instance.stripsPower[component.Value.inputPositions[4, 0], component.Value.inputPositions[4,1]]);
            Debug.Log(BreadboardManager.instance.stripsPower[component.Value.inputPositions[5, 0], component.Value.inputPositions[5,1]]);
            if (BreadboardManager.instance.stripsPower[component.Value.inputPositions[4, 0], component.Value.inputPositions[4,1]] == -1 && BreadboardManager.instance.stripsPower[component.Value.inputPositions[5, 0], component.Value.inputPositions[5,1]] == 1)
            {
                boolone = true;
            }
        }

        foreach(KeyValuePair<int, CircuitComponent> component in bm.icsFour)
        {
            Debug.Log(BreadboardManager.instance.stripsPower[component.Value.inputPositions[3, 0], component.Value.inputPositions[3,1]]);
            Debug.Log(BreadboardManager.instance.stripsPower[component.Value.inputPositions[4, 0], component.Value.inputPositions[4,1]]);
             if (BreadboardManager.instance.stripsPower[component.Value.inputPositions[3, 0], component.Value.inputPositions[3,1]] == -1 && BreadboardManager.instance.stripsPower[component.Value.inputPositions[4, 0], component.Value.inputPositions[4,1]] == 1)
            {
                booltwo = true;
            }
        }


        if (boolone && booltwo)
        {
            return true;
        }

        return false;

    }
}
