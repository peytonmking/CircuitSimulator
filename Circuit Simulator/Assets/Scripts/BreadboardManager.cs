using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BreadboardManager : MonoBehaviour
{
    public static BreadboardManager instance;
    
    //public int[,] busStripsPower;
    //public int[,] terminalStripsPower;
    public int[,] stripsPower;

    public CircuitComponent[,] busStrips;
    public CircuitComponent[,] terminalStrips;
    public CircuitComponent[,] strips;

    public bool powerOn;

    public int busStripsLength;
    public int busStripsWidth;

    public int terminalStripsLength;
    public int terminalStripsWidth;

    public Text powerDebug; 

    public List<int> busIndecis = new List<int>();

    public int componentsPlaced;

    public int color;

    public List<CircuitComponent> componentQueue = new List<CircuitComponent>();
    public Dictionary<int, CircuitComponent> switches = new Dictionary<int, CircuitComponent>();
    public Dictionary<int, CircuitComponent> leds = new Dictionary<int, CircuitComponent>();
    public Dictionary<int, CircuitComponent> wires = new Dictionary<int, CircuitComponent>();
    public Dictionary<int, CircuitComponent> icsFour  = new Dictionary<int, CircuitComponent>();
    public Dictionary<int, CircuitComponent> icsEight = new Dictionary<int, CircuitComponent>();

    public int idCounter;

    public int ledCount;
    // 0 = No power
    // 1 = Positive
    // -1 = Negative

    public Image previewColor;

    public int[] red;
    public int[] blue;
    public int[] green;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else if (instance != this)
        {
            Debug.Log("Instance already exists, destroying object!");
            Destroy(this);
        }
    }

    void Start()
    {
        busStrips = new CircuitComponent[busStripsLength+1,busStripsWidth]; // sides
        terminalStrips = new CircuitComponent[terminalStripsLength+1,terminalStripsWidth]; // middle
        strips = new CircuitComponent[busStripsLength + terminalStripsLength+1, busStripsWidth + terminalStripsWidth];

        //busStripsPower = new int[busStripsLength+1,busStripsWidth]; // sides
        //terminalStripsPower = new int[terminalStripsLength+1,terminalStripsWidth]; // middle
        stripsPower = new int[terminalStripsLength+1, busStripsWidth + terminalStripsWidth];

        componentsPlaced = 0;


        // Setting all slots to no power, 0.
        PowerOffLogic();
        PowerDebug();

    }

    // go through every slot one by one, if it has a component go to that component then power the outputs, check those outputs if they have inputs continue cycle.


    public void TogglePower()
    {
        //DebugLog.instance.Write("toggling breadboard power");
        if (!powerOn)
        {
            powerOn = true;
            PowerOnLogic();
            PowerDebug();
        } else {
            powerOn = false;
            PowerOffLogic();
            PowerDebug();
        }
    }



    public void PowerOnLogic()
    {
        //DebugLog.instance.Write("checking power on logic");
        stripsPower[63,0] = 1;
        stripsPower[63,1] = -1;

        if (strips[63,0] != null)
        {

            componentQueue.Add(strips[63,0]);

        }

        if (strips[63,1] != null)
        {


            componentQueue.Add(strips[63,1]);

        }


        for (int i = 0; i < componentQueue.Count; i++)
        {

            componentQueue[i].CheckLogic();
        }
        componentQueue = new List<CircuitComponent>();



        // turn all busStrip to true, checking for components if there is power, adding components to componentQueue
        for (int i = 0; i < busStripsLength; i++)
        {
            if (stripsPower[i,0] == 1)
            {

                for (int j = 0; j < busStripsLength; j++)
                {
                    stripsPower[j,0] = 1;
                    if (strips[j,0] != null && strips[j,0].logic <=3)
                    {
                        componentQueue.Add(strips[j,0]);
                    }
                }
                break;
            }
        }

        for (int i = 0; i < busStripsLength; i++)
        {
            if (stripsPower[i,1] == -1)
            {
                for (int j = 0; j < busStripsLength; j++)
                {
                    stripsPower[j,1] = -1;
                    if (strips[j,1] != null && strips[j,1].logic <=5)
                    {
                        componentQueue.Add(strips[j,1]);
                    }
                }
                break;
            }
        }


        // check the logic of components, see if output is changed
        // this should change the outputs to whatever is needed (i.e. if false will change false, if positive will change positive)
        for (int i = 0; i < componentQueue.Count; i++)
        {

            componentQueue[i].CheckLogic();
        }
        componentQueue = new List<CircuitComponent>();

        // check each strip to see if it has true or false value (positive or negative), if there is change everything in that strip to that value
        // false for ground in case of IC
        // checking every length then modifying all width
        componentsPlaced += 1;
        for (int z = 0; z < componentsPlaced; z++)
        {
            for (int i = 0; i < terminalStripsLength; i++)
            {
                // only checking from 2 to 7, because each strip is only 5 in length
                for (int j = 2; j < 7; j++)
                {
                    if (stripsPower[i,j] == 1)
                    {
                        for (int k = 2; k < 7; k++)
                        {
                            stripsPower[i,k] = 1;

                            if (strips[i,k] != null && strips[i,k].logic <=5)
                            {
                                componentQueue.Add(strips[i,k]);
                            }
                        }
                        break;
                    }

                    if (stripsPower[i,j] == -1)
                    {
                        for (int k = 2; k < 7; k++)
                        {
                            stripsPower[i,k] = -1;

                            if (strips[i,k] != null && strips[i,k].logic <=5)
                            {
                                componentQueue.Add(strips[i,k]);
                            }
                        }
                        break;
                    }
                }
            }

            for (int i = 0; i < terminalStripsLength; i++)
            {
                // only checking from 7 to 12, because each strip is only 5 in length
                for (int j = 7; j < 12; j++)
                {
                    if (stripsPower[i,j] == 1)
                    {
                        for (int k = 7; k < 12; k++)
                        {
                            stripsPower[i,k] = 1;

                            if (strips[i,k] != null && strips[i,k].logic <=5)
                            {
                                componentQueue.Add(strips[i,k]);
                            }
                        }
                        break;
                    }

                    if (stripsPower[i,j] == -1)
                    {
                        for (int k = 7; k < 12; k++)
                        {
                            stripsPower[i,k] = -1;

                            if (strips[i,k] != null && strips[i,k].logic <=5)
                            {
                                componentQueue.Add(strips[i,k]);
                            }
                        }
                        break;
                    }
                }
            }

            for (int i = 0; i < componentQueue.Count; i++)
            {
                componentQueue[i].CheckLogic();
            }
            componentQueue = new List<CircuitComponent>();

        }

                // turn all busStrip to true, checking for components if there is power, adding components to componentQueue
        for (int i = 0; i < busStripsLength; i++)
        {
            if (stripsPower[i,13] == 1)
            {

                for (int j = 0; j < busStripsLength; j++)
                {
                    stripsPower[j,13] = 1;
                    if (strips[j,13] != null)
                    {
                        componentQueue.Add(strips[j,13]);
                    }
                }
                break;
            }
        }

        for (int i = 0; i < busStripsLength; i++)
        {
            if (stripsPower[i,12] == -1)
            {
                for (int j = 0; j < busStripsLength; j++)
                {
                    stripsPower[j,12] = -1;
                    if (strips[j,12] != null)
                    {
                        componentQueue.Add(strips[j,12]);
                    }
                }
                break;
            }
        }
    }

    public void PowerOffLogic()
    {
        for (int width = 0; width < (busStripsWidth + terminalStripsWidth); width++)
        {
            if (busIndecis.Contains(width))
            {
                for (int length = 0; length < busStripsLength; length++)
                {
                    stripsPower[length,width] = 0;
                    if (strips[length, width] != null)
                    {
                        strips[length, width].logic = 0;
                        strips[length, width].PowerOff();
                    }
                }
            } else
            {
                for (int length = 0; length < terminalStripsLength; length++)
                {
                    stripsPower[length,width] = 0;
                    if (strips[length, width] != null)
                    {
                        strips[length, width].logic = 0;
                        strips[length, width].PowerOff();
                    }
                }
            }
        }

        if (strips[63,0] != null)
        {
            strips[63,0].logic = 0;
        }

        if (strips[63,1] != null)
        {
            strips[63,1].logic = 0;
        }
        stripsPower[63,0] = 0;
        
        stripsPower[63,1] = 0;
    }

    public void PowerDebug()
    {
        powerDebug.text = "";
        int counter = 0;

        for (int width = 0; width < (busStripsWidth + terminalStripsWidth); width++)
        {
            if (busIndecis.Contains(width))
            {
                powerDebug.text += "      ";
            }

            if (busIndecis.Contains(width))
            {
                for (int length = 0; length < busStripsLength; length++)
                {
                    counter += 1;
                    string temp = "0 ";

                    if (stripsPower[length,width] == 1)
                    {
                        temp = "1 ";
                    }
                    
                    if (stripsPower[length, width] == -1)
                    {
                        temp = "-1 ";
                    }

                    powerDebug.text += temp;

                    if (counter%5==0 && busIndecis.Contains(width))
                    {
                        powerDebug.text += "   ";
                    }
                }
            } else
            {
                for (int length = 0; length < terminalStripsLength; length++)
                {
                    string temp = "0 ";

                    if (stripsPower[length,width] == 1)
                    {
                        temp = "1 ";
                    }

                    if (stripsPower[length, width] == -1)
                    {
                        temp = "-1 ";
                    }

                    powerDebug.text += temp;

                }
            }




            if (busIndecis.Contains(width))
            {
                powerDebug.text += " - - ";
            }

            powerDebug.text += "\n";
        }

        if (stripsPower[63, 0] == 1)
        {
            powerDebug.text += "1";
        }
        else
        {
            powerDebug.text += "0";
        }

        if (stripsPower[63, 1] == -1)
        {
            powerDebug.text += "-1";
        }
        else
        {
            powerDebug.text += "0";
        }
    }

    public void Insert(int x, int y, CircuitComponent circuitComponent)
    {
        if (strips[x, y] == null)
        {
            //DebugLog.instance.Write("Inserting component" + x + "," + y);
            strips[x, y] = circuitComponent;
            componentsPlaced+=1;

            if (circuitComponent.componentType == "led")
            {
                circuitComponent.id = idCounter;
                leds.Add(idCounter, circuitComponent); 
            }

            if (circuitComponent.componentType == "wire")
            {
                circuitComponent.id = idCounter;
                wires.Add(idCounter, circuitComponent);
            }

            if (circuitComponent.componentType == "7404")
            {
                circuitComponent.id = idCounter;
                icsFour.Add(idCounter, circuitComponent);
            }

            if (circuitComponent.componentType == "7408")
            {
                circuitComponent.id = idCounter;
                icsEight.Add(idCounter, circuitComponent);
            }

            if (circuitComponent.componentType == "switch")
            {
                circuitComponent.id = idCounter;
                switches.Add(idCounter, circuitComponent);
            }

            ProcedureManager.instance.CheckSteps(this);
            idCounter+=1;
        }
    }

    public void Remove(int x, int y)
    {
        if (strips[x,y].componentType == "led")
        {
            leds.Remove(strips[x,y].id);
        }
        strips[x, y] = null;
        componentsPlaced-=1;

        if (powerOn)
        {
            PowerOnLogic();
        }
    }

    public void ChangeColor(int _color)
    {
        color = _color;
        previewColor.color = new Color(red[color], green[color], blue[color]);
    }
}
