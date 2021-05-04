using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MenuNavigation : MonoBehaviour
{
    public int selectedLab;

    public Lab[] labs;

    public Text labTitle;
    public Text[] labComponents;
    public Text labDescription;

    void Start()
    {
        if (labs.Length > 0)
        {
            SelectLab(selectedLab);
        }
    }

    public void ChangeScene(string newScene)
    {
        SceneManager.LoadScene(newScene);
    }

    public void SelectLab(int lab)
    {
        selectedLab = lab;
        UpdateLabSelection(selectedLab);
    }

    void UpdateLabSelection(int lab)
    {
        labTitle.text = labs[lab].title;

        for (int i = 0; i < labs[lab].components.Length; i++)
        {
            labComponents[i].text = labs[lab].components[i];
        }

        labDescription.text = labs[lab].description;
    }

    public void StartLab()
    {
        SceneManager.LoadScene(labs[selectedLab].title);
    }

    public void Quit()
    {
        Application.Quit();
    }
}

