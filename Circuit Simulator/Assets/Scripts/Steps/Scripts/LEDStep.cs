using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "CircuitSimulator/Step/LEDStep")]
public class LEDStep : Step
{
    public int count;
    public override bool CheckComplete(BreadboardManager bm)
    {
        if (bm.leds.Count >= count)
        {
            return true;
        }

        return false;
    }
}
