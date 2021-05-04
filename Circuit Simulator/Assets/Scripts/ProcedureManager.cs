using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class ProcedureManager : MonoBehaviour
{
    public static ProcedureManager instance;
    public Lab lab;
    public bool[] stepsCompleted;

    [TextArea]
    public string[] stepsDescription;

    public Step[] steps;

    public TextMeshProUGUI[] stepsText;

    public int page;
    public int maxPage;

    public GameObject backSteps;
    public GameObject nextSteps;
    public GameObject[] pages;

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
        stepsCompleted = lab.stepsCompleted;
        stepsDescription = lab.stepsDescription;

        for (int i = 0; i < stepsDescription.Length; i++)
        {
            stepsText[i].text = "Step " + (i+1) + ": " + stepsDescription[i];
        }
        backSteps.SetActive(false);
    }

    public void CompleteStep(int step)
    {
        stepsText[step].text = "<s>" + stepsText[step].text + "</s>";
    }

    public void ChangePage(int next)
    {
        backSteps.SetActive(true);
        nextSteps.SetActive(true);

        pages[page].SetActive(false);
        page+=next;
        pages[page].SetActive(true);

        if (page == 0)
        {
            backSteps.SetActive(false);
        }

        if (page == maxPage-1)
        {
            nextSteps.SetActive(false);
        }



    }

    public void CheckSteps(BreadboardManager bm)
    {
        for (int i = 0; i < steps.Length; i++)
        {
            if (i == 0)
            {   
                stepsCompleted[i] = steps[i].CheckComplete(bm);
            }
            else
            {
                if (stepsCompleted[i-1] && !stepsCompleted[i])
                {
                    stepsCompleted[i] = steps[i].CheckComplete(bm);
                }
            }
           
        }

        for (int i = 0; i < stepsCompleted.Length; i++)
        {
            if (stepsCompleted[i])
            {
                CompleteStep(i);
            }
        }
    }
}
