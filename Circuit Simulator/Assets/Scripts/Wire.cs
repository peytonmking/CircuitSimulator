using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Wire : MonoBehaviour
{
    public CircuitComponent circuitComponent;
    public WireRender wireRender;

    public void RemoveWire()
    {
        circuitComponent.RemoveComponent();
        wireRender.RemoveRender();
        Destroy(this.gameObject);
    }
}
