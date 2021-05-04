using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "CircuitSimulator/Step/ICStep")]
public class ICStep : Step
{
    public string ic;
    public override bool CheckComplete(BreadboardManager bm)
    {
        if (ic == "7408")
        {
            if (bm.icsEight.Count >= 1)
            {
                return true;
            }
        }
        else
        {
            if (bm.icsFour.Count >= 1)
            {
                return true;
            }
        }


        return false;
    }
}
