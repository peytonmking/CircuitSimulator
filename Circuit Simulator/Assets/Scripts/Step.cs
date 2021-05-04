using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Step : ScriptableObject
{
    public abstract bool CheckComplete(BreadboardManager bm);
}
