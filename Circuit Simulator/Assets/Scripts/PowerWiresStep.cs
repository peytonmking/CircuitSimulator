using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "CircuitSimulator/Step/PowerWiresStep")]
public class PowerWiresStep : Step
{
    public override bool CheckComplete(BreadboardManager bm)
    {
        if (bm.strips[63,1] != null && bm.strips[63,0] != null)
        {

            return true;
        }
        else
        {

            return false;
        }
    }
}
