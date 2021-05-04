using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RackSocketManager : MonoBehaviour
{
    public GameObject componentPrefab;

    void Start()
    {
        Taken();
    }

    public void Taken()
    {
        Instantiate(componentPrefab, this.transform.position, Quaternion.identity);
    }
}
