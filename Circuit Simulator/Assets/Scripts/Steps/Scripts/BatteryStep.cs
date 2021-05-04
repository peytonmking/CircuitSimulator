using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "CircuitSimulator/Step/BatteryStep")]
public class BatteryStep : Step
{
    public override bool CheckComplete(BreadboardManager bm)
    {
        return PowerManager.instance.battery;
    }
}
