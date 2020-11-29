using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
public class Bug : MonoBehaviour
{
    private int state = 0;

    [SerializeField] private BugMovement movement;

    public void CreateRandomPath()
    {
        (Vector2 a , Vector2 b) = BugPointGenerator.CreateDiffSidePoint();
        movement.UpdatePath(a, b);
    }
    void Update()
    {
        if (movement.IsFinishPath())
        {
            CreateRandomPath();
        }
    }
    private void OnMouseDown()
    {
        if (!movement.IncreaseSpeed())
        {
            Destroy(gameObject);
        }
    }

}
