using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Edge : MonoBehaviour
{
    //36735 48419 23288 28713 6392 13476 33307
    [SerializeField] private LineRenderer lineRenderer;
    [SerializeField] private Transform pointA, pointB;
    [SerializeField] private GameObject connectionPrefab;
    [SerializeField] private int randomValue;
    public static int edgesSum;
    public static bool isOver = false;
    void Start()
    {
        Vector3[] points = new Vector3[]
        {
            pointA.position , pointB.position
        };
        lineRenderer.SetPositions(points);

        GameObject buttonA = Instantiate(connectionPrefab, transform);
        GameObject buttonB = Instantiate(connectionPrefab, transform);

        buttonA.GetComponent<ConnectionPoint>().edge = this;
        buttonB.GetComponent<ConnectionPoint>().edge = this;

        buttonA.transform.position = pointA.position;
        buttonB.transform.position = pointB.position;
    }
    public void ToggleState()
    {
        lineRenderer.enabled ^= true;
        if (lineRenderer.enabled)
        {
            edgesSum += randomValue;
        }
        else
        {
            edgesSum -= randomValue;
        }
        Clock.instance.UpdateAngle(edgesSum % 7);
        if (edgesSum % 7 == 4)
        {
            isOver = true;
        }
    }
}
