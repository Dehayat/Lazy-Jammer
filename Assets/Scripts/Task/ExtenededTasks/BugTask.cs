using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BugTask : Task
{
    [SerializeField] private Transform bugParent;
    public void LateUpdate()
    {
        if (bugParent.childCount == 0)
        {
            FinishTask();
        }
    }
}
