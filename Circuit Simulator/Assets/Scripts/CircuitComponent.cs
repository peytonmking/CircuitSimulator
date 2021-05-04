using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CircuitComponent : MonoBehaviour
{
    public int id;
    public string componentType;

    public int[,] inputPositions; // each input should be a 2D point
    public int[,] outputPositions; // each output should be a 2D point

    public Rigidbody rigidBody;

    public Material onMaterial;
    public Material offMaterial;

    public int maxInputs;
    public int maxOutputs;

    public bool[] outputs;

    public bool[] switches;

    public int logic;

    void Awake()
    {
        if (componentType == "7404")
        {
            inputPositions = new int[8,2];
            outputPositions = new int[6,2];
        }

        if (componentType == "7408")
        {
            inputPositions = new int[11,3];
            outputPositions = new int[5,3];
        }

        if (componentType == "led" || componentType == "wire")
        {
            inputPositions = new int[2,2];
            outputPositions = new int[2,2];
        }

        if (componentType == "switch")
        {
            switches = new bool[2];
            inputPositions = new int[2,2];
            outputPositions = new int[2,2];

        }
    }

    public void Insert()
    {
        for (int i = 0; i < maxInputs; i++)
        {
            BreadboardManager.instance.Insert(inputPositions[i,0], inputPositions[i,1], this);
        }
    }

    public void RemoveComponent()
    {
        //DebugLog.instance.Write("RemoveComponent() in CircuitComponent.cs: " + componentType);

        for (int i = 0; i < maxInputs; i++)
        {
            if (BreadboardManager.instance.strips[inputPositions[i,0], inputPositions[i,1]] != null)
            {
                BreadboardManager.instance.Remove(inputPositions[i,0], inputPositions[i,1]);
            }
            
        }

        if (componentType == "led")
        {
                var temp = this.GetComponent<MeshRenderer>().materials;
                temp[0] = offMaterial;
                this.GetComponent<MeshRenderer>().materials = temp;
        }

        // if (componentType == "wire")
        // {
        //     DebugLog.instance.Write("setting kin to false");
        //     rigidBody.isKinematic = false;
        // }
    }

    public void CheckLogic()
    {
        //DebugLog.instance.Write("CheckLogic() in CircuitComponent.cs: " + componentType);
        if (componentType == "resistor")
        {
            //DebugLog.instance.Write("Checking CircuitComponent.type \'resistor\'...");
            if (BreadboardManager.instance.stripsPower[inputPositions[0,0], inputPositions[0,1]] == 1)
            {
                //DebugLog.instance.Write("Setting (" + outputPositions[0,0] + ", " + outputPositions[0,1] + ") to 1.");
                BreadboardManager.instance.stripsPower[outputPositions[0,0], outputPositions[0,1]] = 1;
            }

            if (BreadboardManager.instance.stripsPower[inputPositions[0,0], inputPositions[0,1]] == -1)
            {
                //DebugLog.instance.Write("Setting (" + outputPositions[0,0] + ", " + outputPositions[0,1] + ") to 1.");
                BreadboardManager.instance.stripsPower[outputPositions[0,0], outputPositions[0,1]] = -1;
            }

            PowerManager.instance.voltage = 0;
        }

        if (componentType == "wire")
        {
            //DebugLog.instance.Write("Checking CircuitComponent.type \'wire\'...");
            if (BreadboardManager.instance.stripsPower[inputPositions[0,0], inputPositions[0,1]] == 1)
            {
                //DebugLog.instance.Write("Setting (" + outputPositions[0,0] + ", " + outputPositions[0,1] + ") to 1.");
                BreadboardManager.instance.stripsPower[outputPositions[0,0], outputPositions[0,1]] = 1;
            }

            if (BreadboardManager.instance.stripsPower[inputPositions[0,0], inputPositions[0,1]] == -1)
            {
                DebugLog.instance.Write("From (" + inputPositions[0,0] + ", " + inputPositions[0,1] + ") Setting (" + outputPositions[0,0] + ", " + outputPositions[0,1] + ") to -1.");
                BreadboardManager.instance.stripsPower[outputPositions[0,0], outputPositions[0,1]] = -1;
            }

            if (BreadboardManager.instance.stripsPower[outputPositions[0,0], outputPositions[0,1]] == 1)
            {
                //DebugLog.instance.Write("Setting (" + inputPositions[0,0] + ", " + inputPositions[0,1] + ") to 1.");
                BreadboardManager.instance.stripsPower[inputPositions[0,0], inputPositions[0,1]] = 1;
            }

            if (BreadboardManager.instance.stripsPower[outputPositions[0,0], outputPositions[0,1]] == -1)
            {
                DebugLog.instance.Write("From (" + outputPositions[0,0] + ", " + outputPositions[0,1] + ") Setting (" + inputPositions[0,0] + ", " + inputPositions[0,1] + ") to -1.");
                BreadboardManager.instance.stripsPower[inputPositions[0,0], inputPositions[0,1]] = -1;
            }
        }

        if (componentType == "7408") // and
        {
            // have to add if input is not true and false for ceertain inputs on ic

            //DebugLog.instance.Write("Checking CircuitComponent.type \'7408 (and)\'...");

            // Checking if 7 and 14 on ic are gnd and v
            Debug.Log(BreadboardManager.instance.stripsPower[inputPositions[4, 0], inputPositions[4,1]]);
            Debug.Log(BreadboardManager.instance.stripsPower[inputPositions[5, 0], inputPositions[5,1]]);
            if (BreadboardManager.instance.stripsPower[inputPositions[4, 0], inputPositions[4,1]] == -1 && BreadboardManager.instance.stripsPower[inputPositions[5, 0], inputPositions[5,1]] == 1)
            {
                List<int> outputValues = new List<int>();
                outputValues.Add(0);
                outputValues.Add(0);
                outputValues.Add(1);
                outputValues.Add(1);
                outputValues.Add(2);
                outputValues.Add(2);
                outputValues.Add(3);
                outputValues.Add(3);

                List<int> inputValues = new List<int>();
                inputValues.Add(0);
                inputValues.Add(1);
                inputValues.Add(2);
                inputValues.Add(3);
                inputValues.Add(6);
                inputValues.Add(7);
                inputValues.Add(8);
                inputValues.Add(9);

                for (int i = 0; i < 8; i+=2) // looping through all 4 gates in the IC
                {
                    Debug.Log(inputPositions[inputValues[i],0]);
                    Debug.Log(inputPositions[inputValues[i],1]);
                    Debug.Log(BreadboardManager.instance.stripsPower[inputPositions[inputValues[i],0], inputPositions[inputValues[i],1]]);
                    Debug.Log(inputPositions[inputValues[i+1],0]);
                    Debug.Log(inputPositions[inputValues[i+1],1]);
                    Debug.Log(BreadboardManager.instance.stripsPower[inputPositions[inputValues[i+1],0], inputPositions[inputValues[i+1],1]]);
                    if (BreadboardManager.instance.stripsPower[inputPositions[inputValues[i],0], inputPositions[inputValues[i],1]] == 1 && BreadboardManager.instance.stripsPower[inputPositions[inputValues[i+1],0], inputPositions[inputValues[i+1],1]] == 1) // checking and logic gate
                    {
                        DebugLog.instance.Write("Setting (" + outputPositions[outputValues[i],0] + ", " + outputPositions[outputValues[i],1] + ") to 1.");
                        BreadboardManager.instance.stripsPower[outputPositions[outputValues[i],0], outputPositions[outputValues[i],1]] = 1;
                    }

                }
            }
            else
            {
                DebugLog.instance.Write("GND or V is not connected properly.");
            }
        }

        if (componentType == "7404") // nor
        {
            List<int> outputValues = new List<int>();
            outputValues.Add(0);
            outputValues.Add(1);
            outputValues.Add(2);
            outputValues.Add(3);
            outputValues.Add(4);
            outputValues.Add(5);

            List<int> inputValues = new List<int>();
            inputValues.Add(0);
            inputValues.Add(1);
            inputValues.Add(2);
            inputValues.Add(5);
            inputValues.Add(6);
            inputValues.Add(7);

            //DebugLog.instance.Write("Checking CircuitComponent.type \'7404 (not )\'...");
            // Checking if 7 and 14 on ic are gnd and v
            if (BreadboardManager.instance.stripsPower[inputPositions[3, 0], inputPositions[3,1]] == -1 && BreadboardManager.instance.stripsPower[inputPositions[4, 0], inputPositions[4,1]] == 1)
            {

                for (int i = 0; i < 6; i++) // looping through all 4 gates in the IC
                {
                    if (BreadboardManager.instance.stripsPower[inputPositions[inputValues[i],0], inputPositions[inputValues[i],1]] == 1) // checking and logic gate
                    {
                        //DebugLog.instance.Write("Setting (" + outputPositions[outputValues[i],0] + ", " + outputPositions[outputValues[i],1] + ") to 0.");
                        Debug.Log("changing to 0");
                        if (outputPositions[outputValues[i], 1] >= 7)
                        {
                            for (int k = 7; k < 12; k++)
                            {
                                BreadboardManager.instance.stripsPower[outputPositions[outputValues[i],0], k] = 0;
                                Debug.Log(outputPositions[outputValues[i],0]);
                                Debug.Log(k);
                                if (BreadboardManager.instance.strips[outputPositions[outputValues[i],0],k] != null)
                                {
                                    if (BreadboardManager.instance.strips[outputPositions[outputValues[i],0],k].inputPositions[0,1] >= 7)
                                    {
                                        for (int j = 7; j < 12; j++)
                                        {
                                            BreadboardManager.instance.stripsPower[BreadboardManager.instance.strips[outputPositions[outputValues[i],0],k].inputPositions[0,0], j] = 0;
                                            Debug.Log("Setting " + BreadboardManager.instance.strips[outputPositions[outputValues[i],0],k].inputPositions[0,0] + "," + j + " to 0");

                                        }
                                    }
                                    else
                                    {
                                        for (int j = 2; j < 7; j++)
                                        {
                                            BreadboardManager.instance.stripsPower[BreadboardManager.instance.strips[outputPositions[outputValues[i],0],k].inputPositions[0,0], j] = 0;
                                            Debug.Log("Setting " + BreadboardManager.instance.strips[outputPositions[outputValues[i],0],k].inputPositions[0,0] + "," + j + " to 0");

                                        }
                                    }

                                    if (BreadboardManager.instance.strips[outputPositions[outputValues[i],0],k].outputPositions[0,1] >= 7)
                                    {
                                        for (int j = 7; j < 12; j++)
                                        {
                                            BreadboardManager.instance.stripsPower[BreadboardManager.instance.strips[outputPositions[outputValues[i],0],k].outputPositions[0,0], j] = 0;
                                            Debug.Log("Setting " + BreadboardManager.instance.strips[outputPositions[outputValues[i],0],k].outputPositions[0,0] + "," + j + " to 0");
                                        }
                                    }
                                    else
                                    {
                                        for (int j = 2; j < 7; j++)
                                        {
                                            BreadboardManager.instance.stripsPower[BreadboardManager.instance.strips[outputPositions[outputValues[i],0],k].outputPositions[0,0], j] = 0;
                                            Debug.Log("Setting " + BreadboardManager.instance.strips[outputPositions[outputValues[i],0],k].outputPositions[0,0] + "," + j + " to 0");
                                        }
                                    }
                                }
                            }
                        }
                        else
                        {
                            for (int k = 2; k < 7; k++)
                            {
                                BreadboardManager.instance.stripsPower[outputPositions[outputValues[i],0], k] = 0;
                                Debug.Log(outputPositions[outputValues[i],0]);
                                Debug.Log(k);
                                if (BreadboardManager.instance.strips[outputPositions[outputValues[i],0],k] != null)
                                {
                                    if (BreadboardManager.instance.strips[outputPositions[outputValues[i],0],k].inputPositions[0,1] >= 7)
                                    {
                                        for (int j = 7; j < 12; j++)
                                        {
                                            BreadboardManager.instance.stripsPower[BreadboardManager.instance.strips[outputPositions[outputValues[i],0],k].inputPositions[0,0], j] = 0;
                                            Debug.Log("Setting " + BreadboardManager.instance.strips[outputPositions[outputValues[i],0],k].inputPositions[0,0] + "," + j + " to 0");
                                        }
                                    }
                                    else
                                    {
                                        for (int j = 2; j < 7; j++)
                                        {
                                            BreadboardManager.instance.stripsPower[BreadboardManager.instance.strips[outputPositions[outputValues[i],0],k].inputPositions[0,0], j] = 0;
                                            Debug.Log("Setting " + BreadboardManager.instance.strips[outputPositions[outputValues[i],0],k].inputPositions[0,0] + "," + j + " to 0");
                                        }
                                    }

                                    if (BreadboardManager.instance.strips[outputPositions[outputValues[i],0],k].outputPositions[0,1] >= 7)
                                    {
                                        for (int j = 7; j < 12; j++)
                                        {
                                            BreadboardManager.instance.stripsPower[BreadboardManager.instance.strips[outputPositions[outputValues[i],0],k].outputPositions[0,0], j] = 0;
                                            Debug.Log("Setting " + BreadboardManager.instance.strips[outputPositions[outputValues[i],0],k].outputPositions[0,0] + "," + j + " to 0");
                                        }
                                    }
                                    else
                                    {
                                        for (int j = 2; j < 7; j++)
                                        {
                                            BreadboardManager.instance.stripsPower[BreadboardManager.instance.strips[outputPositions[outputValues[i],0],k].outputPositions[0,0], j] = 0;
                                            Debug.Log("Setting " + BreadboardManager.instance.strips[outputPositions[outputValues[i],0],k].outputPositions[0,0] + "," + j + " to 0");
                                        }
                                    }
                                }

                            }
                        }
                        
                    }
                    if (BreadboardManager.instance.stripsPower[inputPositions[inputValues[i],0], inputPositions[inputValues[i],1]] == 0) // checking and logic gate
                    {
                        //DebugLog.instance.Write("Setting (" + outputPositions[outputValues[i],0] + ", " + outputPositions[outputValues[i],1] + ") to 1.");
                        BreadboardManager.instance.stripsPower[outputPositions[outputValues[i],0], outputPositions[outputValues[i],1]] = 1;
                    }
                }
            }
            else
            {
                DebugLog.instance.Write("GND or V is not connected properly.");
            }
        }

        if (componentType == "led")
        {
            //DebugLog.instance.Write("Checking CircuitComponent.type \'led\'...");
            //DebugLog.instance.Write("Displaying positions:" + BreadboardManager.instance.stripsPower[inputPositions[0,0], inputPositions[0,1]] + " " + BreadboardManager.instance.stripsPower[outputPositions[0,0], outputPositions[0,1]]);
            if (BreadboardManager.instance.stripsPower[inputPositions[0,0], inputPositions[0,1]] == 1 && BreadboardManager.instance.stripsPower[outputPositions[0,0], outputPositions[0,1]] == -1)
            {
                //DebugLog.instance.Write("LED Component powered...");
                var temp = this.GetComponent<MeshRenderer>().materials;
                temp[0] = onMaterial;
                this.GetComponent<MeshRenderer>().materials = temp;
            }
            else
            {
                var temp = this.GetComponent<MeshRenderer>().materials;
                temp[0] = offMaterial;
                this.GetComponent<MeshRenderer>().materials = temp;
            }

            
            // lighting up the led...
        }

        if (componentType == "switch")
        {
            if (BreadboardManager.instance.stripsPower[inputPositions[0,0], inputPositions[0,1]] == 1 && switches[0])
            {
                BreadboardManager.instance.stripsPower[outputPositions[0,0], outputPositions[0,1]] = 1;
            }

            if (BreadboardManager.instance.stripsPower[inputPositions[1,0], inputPositions[1,1]] == 1 && switches[1])
            {
                BreadboardManager.instance.stripsPower[outputPositions[1,0], outputPositions[1,1]] = 1;
            }
        }

        logic+=1;

    }

    public void PowerOff()
    {
        if (componentType == "led")
        {
                var temp = this.GetComponent<MeshRenderer>().materials;
                temp[0] = offMaterial;
                this.GetComponent<MeshRenderer>().materials = temp;
        }
    }
}