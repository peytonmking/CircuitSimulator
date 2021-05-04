using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BreadSocketManager : MonoBehaviour
{
    public BreadboardSocketManager socketManager;
    public MeshRenderer meshRenderer;

    public string socketType;

    public int[] inputX;
    public int[] inputY;

    public int[] outputX;
    public int[] outputY;

    public int[] xValues;
    public int[] yValues;


    public int[,] inputPositions;
    public int[,] outputPositions;

    void Start()
    {
        socketManager = GetComponent<BreadboardSocketManager>();
        meshRenderer = GetComponent<MeshRenderer>();

        if (socketType == "led")
        {
            inputPositions = new int[,] { {inputX[0], inputY[0]} };
            outputPositions = new int[,] { {outputX[0], outputY[0]} };
        }

        if (socketType == "switch")
        {
            inputPositions = new int[,] { {inputX[0], inputY[0]}, {inputX[1], inputY[1]} };
            outputPositions = new int[,] { {outputX[0], outputY[0]}, {outputX[1], outputY[1]} };
        }

        // if (socketType == "ic")
        // {
        //     inputPositions = new int[,] { {inputX[0], inputY[0]}, {inputX[1], inputY[1]}, {inputX[2], inputY[2]}, {inputX[3], inputY[3]}, {inputX[4], inputY[4]}, {inputX[5], inputY[5]}, {inputX[6], inputY[6]}, {inputX[7], inputY[7]}, {inputX[8], inputY[8]}, {inputX[9], inputY[9]} };
        //     outputPositions = new int[,] { {outputX[0], outputY[0]}, {outputX[1], outputY[1]}, {outputX[2], outputY[2]}, {outputX[3], outputY[3]} };
        // }

    }

    public CircuitComponent circComponent;
    // When component is added to the breadboard
    public void OnTriggerEnter(Collider other)
    {
        if (!other.CompareTag("Hand") && other.gameObject.GetComponent<CircuitComponent>())
        {
            if (other.gameObject.GetComponent<CircuitComponent>().componentType != "wire")
            {
                circComponent = other.gameObject.GetComponent<CircuitComponent>();     
            }
        }
    }

    public void Enter()
    {          
        if (socketType == "led" && circComponent.componentType == "led")
        {
            if (circComponent != null)
            {
                circComponent.inputPositions[0,0] = inputPositions[0,0];
                circComponent.inputPositions[0,1] = inputPositions[0,1];

                circComponent.outputPositions[0,0] = outputPositions[0,0];
                circComponent.outputPositions[0,1] = outputPositions[0,1];

                circComponent.Insert();   
            }  

        }

        if (socketType == "switch")
        {
            Debug.Log("Switch!");
            circComponent.inputPositions[0,0] = inputPositions[0,0];
            circComponent.inputPositions[0,1] = inputPositions[0,1];

            circComponent.outputPositions[0,0] = outputPositions[0,0];
            circComponent.outputPositions[0,1] = outputPositions[0,1];

            circComponent.inputPositions[1,0] = inputPositions[1,0];
            circComponent.inputPositions[1,1] = inputPositions[1,1];

            circComponent.outputPositions[1,0] = outputPositions[1,0];
            circComponent.outputPositions[1,1] = outputPositions[1,1];

            circComponent.Insert();
        }

        if (socketType == "ic")
        {

            if (circComponent.componentType == "7404")
            {

                inputX = new int[8];
                inputY = new int[8];

                outputX = new int[6];
                outputY = new int[6];
                int outputCounter = 0;
                int inputCounter = 0;

                List<int> outputValues = new List<int>();
                outputValues.Add(1);
                outputValues.Add(3);
                outputValues.Add(5);
                outputValues.Add(9);
                outputValues.Add(11);
                outputValues.Add(13);
                    

                for (int i = 0; i < xValues.Length; i++)
                {
                    if (outputValues.Contains(i))
                    {
                        outputX[outputCounter] = xValues[i];
                        outputY[outputCounter] = yValues[i];
                        outputCounter+=1;
                    } 
                    else
                    {
                        inputX[inputCounter] = xValues[i];
                        inputY[inputCounter] = yValues[i];
                        inputCounter+=1;
                    }
        
                }

                inputPositions = new int[,] { {inputX[0], inputY[0]}, {inputX[1], inputY[1]}, {inputX[2], inputY[2]}, {inputX[3], inputY[3]}, {inputX[4], inputY[4]}, {inputX[5], inputY[5]}, {inputX[6], inputY[6]}, {inputX[7], inputY[7]} };
                outputPositions = new int[,] { {outputX[0], outputY[0]}, {outputX[1], outputY[1]}, {outputX[2], outputY[2]}, {outputX[3], outputY[3]}, {outputX[4], outputY[4]}, {outputX[5], outputY[5]}, };
            
                for (int i = 0; i < 8; i++)
                {

                    circComponent.inputPositions[i, 0] = inputPositions[i, 0];
                    circComponent.inputPositions[i, 1] = inputPositions[i, 1];
                }


                for (int i = 0; i < 6; i++)
                {
                    circComponent.outputPositions[i, 0] = outputPositions[i, 0];
                    circComponent.outputPositions[i, 1] = outputPositions[i, 1];
                }
                    
                circComponent.Insert();
            }

            if (circComponent.componentType == "7408")
            {
                inputX = new int[10];
                inputY = new int[10];

                outputX = new int[4];
                outputY = new int[4];

                int outputCounter = 0;
                int inputCounter = 0;

                List<int> outputValues = new List<int>();
                outputValues.Add(2);
                outputValues.Add(5);
                outputValues.Add(10);
                outputValues.Add(13);
                    

                for (int i = 0; i < xValues.Length; i++)
                {
                    if (outputValues.Contains(i))
                    {
                        outputX[outputCounter] = xValues[i];
                        outputY[outputCounter] = yValues[i];
                        outputCounter+=1;
                    } 
                    else
                    {
                        inputX[inputCounter] = xValues[i];
                        inputY[inputCounter] = yValues[i];
                        inputCounter+=1;
                    }
        
                }
        
                inputPositions = new int[,] { {inputX[0], inputY[0]}, {inputX[1], inputY[1]}, {inputX[2], inputY[2]}, {inputX[3], inputY[3]}, {inputX[4], inputY[4]}, {inputX[5], inputY[5]}, {inputX[6], inputY[6]}, {inputX[7], inputY[7]}, {inputX[8], inputY[8]}, {inputX[9], inputY[9]} };
                outputPositions = new int[,] { {outputX[0], outputY[0]}, {outputX[1], outputY[1]}, {outputX[2], outputY[2]}, {outputX[3], outputY[3]} };

                for (int i = 0; i < 10; i++)
                {

                    circComponent.inputPositions[i, 0] = inputPositions[i, 0];
                    circComponent.inputPositions[i, 1] = inputPositions[i, 1];
                }


                for (int i = 0; i < 4; i++)
                {
                    circComponent.outputPositions[i, 0] = outputPositions[i, 0];
                    circComponent.outputPositions[i, 1] = outputPositions[i, 1];
                }
                    
                circComponent.Insert();
            }
        }
    }

    public void Exit()
    {
  
        circComponent.RemoveComponent();
        
    }

    // public void HoverComponentEnter()
    // {
    //     if (!socketManager.previewEnabled)
    //     {
    //         socketManager.previewEnabled = true;
    //         meshRenderer.enabled = true;
    //     }
    // }

    // public void HoverComponentExit()
    // {
    //     socketManager.previewEnabled = false;
    //     meshRenderer.enabled = false;
    // }

}
