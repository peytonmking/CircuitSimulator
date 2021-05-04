using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Switch : MonoBehaviour
{
    // Start is called before the first frame update
    public GameObject switchBox;
    public CircuitComponent circComponent;
    public int switchNum;


    public void MoveSwitch()
    {

        if (circComponent.switches[switchNum])
        {
            switchBox.transform.Rotate(0, 90, 0);
        }
        else
        {
            switchBox.transform.Rotate(0, -90, 0);
        }

        circComponent.switches[switchNum] = !circComponent.switches[switchNum];
        BreadboardManager.instance.PowerOffLogic();
        BreadboardManager.instance.PowerOnLogic();
        BreadboardManager.instance.PowerDebug();
        
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Hand"))
        {
            MoveSwitch();
        }
    }
}
