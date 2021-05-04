using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "CircuitSimulator/Step/PowerStep")]
public class PowerStep : Step
{
    public override bool CheckComplete(BreadboardManager bm)
    {
        return bm.powerOn;
    }  
}
