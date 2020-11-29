using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ConnectionPoint : MonoBehaviour
{
    [HideInInspector] public Edge edge;
    private void OnMouseDown()
    {
        if (Edge.isOver) 
            return;
        edge.ToggleState();
    }
}
