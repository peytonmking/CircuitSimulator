using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WireRender : MonoBehaviour
{
    public LineRenderer lineRenderer;
    Vector3 pointA;
    Vector3 pointB;
    Vector3 instantiatePosition;
    float lerpValue;
    float distance;
    int segmentsToCreate;

    public Material[] mats;

    public void Initialize(Vector3 position1, Vector3 position2, int color)
    {
        pointA = position1 + new Vector3(0, 0, .03f);
        pointB = position2 + new Vector3(0, 0, .03f);

        this.transform.position = (pointA + pointB)/2;

        Vector3 tempVector = new Vector3(pointA.x, pointA.y + 1, pointA.z);

        transform.LookAt(transform.position - (pointB-transform.position));
        transform.rotation *= Quaternion.Euler(90, 0, 0);


        var temp = this.GetComponent<MeshRenderer>().materials;
        temp[0] = mats[color];   ;
        this.GetComponent<MeshRenderer>().materials = temp;     



        lineRenderer.SetPosition(0, position1);
        lineRenderer.SetPosition(1, pointA);
        lineRenderer.SetPosition(3, position2);
        lineRenderer.SetPosition(2, pointB);
    }

    public void RemoveRender()
    {
        lineRenderer.SetPosition(0, new Vector3(0, -2, 0));
        lineRenderer.SetPosition(1, new Vector3(0, -2, 0));
        lineRenderer.SetPosition(2, new Vector3(0, -2, 0));
        lineRenderer.SetPosition(3, new Vector3(0, -2, 0));
    }
}
