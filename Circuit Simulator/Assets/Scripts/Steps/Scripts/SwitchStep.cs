using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "CircuitSimulator/Step/SwitchStep")]
public class SwitchStep : Step
{
    public override bool CheckComplete(BreadboardManager bm)
    {
        if (bm.switches.Count >= 1)
        {
            return true;
        }

        return false;
    }
}
