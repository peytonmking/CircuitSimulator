using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Tool : MonoBehaviour
{
    public GameObject componentTool;
    public Text toolText;
    GameObject firstHit;
    GameObject secondHit;

    int shotCounter = 0;

    GameObject instantiatedComponent;

    public void FireWire()
    {
        // check is slot, if it is attyach then finish on second
        Vector3 fwd = transform.TransformDirection(Vector3.forward);
        RaycastHit hit;
        //int layerMask = 1 << 6;

        if (Physics.Raycast(transform.position, fwd, out hit))
        {
            if (hit.transform.gameObject.CompareTag("Socket"))
            {
                toolText.text = "hit";
                if (shotCounter == 0)
                {
                    firstHit = hit.transform.gameObject;
        
                    //fire first
                    // make first end stick to slot, while second is attached to tool

                    shotCounter = 1;
                }
                else {

                    secondHit = hit.transform.gameObject;
                    Vector3 temp1 = new Vector3(0, firstHit.transform.position.y, firstHit.transform.position.z);
                    Vector3 temp2 = new Vector3(0, secondHit.transform.position.y, secondHit.transform.position.z);
                    
                    
                    instantiatedComponent = Instantiate(componentTool, firstHit.transform.position, Quaternion.Euler(Vector3.Angle(temp1, temp2), 0, 0));

                    Vector3 tempVector = firstHit.transform.position - secondHit.transform.position;
                    instantiatedComponent.transform.localScale = new Vector3(0.03f, tempVector.magnitude * .5f, 0.03f);

                    //Vector3 tempVector2 = (firstHit.transform.position + secondHit.transform.position) / 2;
                    //instantiatedComponent.transform.position = tempVector2 + new Vector3(.03f, 0, 0);

                    instantiatedComponent.GetComponent<CircuitComponent>().inputPositions[0,0] = firstHit.GetComponent<BreadboardSlot>().location[0];
                    instantiatedComponent.GetComponent<CircuitComponent>().inputPositions[0,1] = firstHit.GetComponent<BreadboardSlot>().location[1];
                    
                    instantiatedComponent.GetComponent<CircuitComponent>().outputPositions[0,0] = secondHit.GetComponent<BreadboardSlot>().location[0];
                    instantiatedComponent.GetComponent<CircuitComponent>().outputPositions[0,1] = secondHit.GetComponent<BreadboardSlot>().location[1];
                    BreadboardManager.instance.Insert(instantiatedComponent.GetComponent<CircuitComponent>().inputPositions[0,0], instantiatedComponent.GetComponent<CircuitComponent>().inputPositions[0,1], instantiatedComponent.GetComponent<CircuitComponent>());
                    BreadboardManager.instance.Insert(instantiatedComponent.GetComponent<CircuitComponent>().outputPositions[0,0], instantiatedComponent.GetComponent<CircuitComponent>().outputPositions[0,1], instantiatedComponent.GetComponent<CircuitComponent>());
                    instantiatedComponent.GetComponent<WireRender>().Initialize(firstHit.transform.position, secondHit.transform.position, BreadboardManager.instance.color);
                
                    //fire second
                    // make second stick to second slot
                    // instantiate the component to the breadboard
                    toolText.text = "inserted";
                    shotCounter = 0;
                }
            }
            else {
                toolText.text = "miss";
            }
        }   
    }

}
