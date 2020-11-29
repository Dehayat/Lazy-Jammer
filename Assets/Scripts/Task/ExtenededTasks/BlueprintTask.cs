using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlueprintTask : Task
{
    private void LateUpdate()
    {
        if (Edge.isOver && Clock.instance.isOver)
            FinishTask();
    }
}
