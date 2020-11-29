using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Task : MonoBehaviour
{
    public int taskID;
    public virtual void FinishTask()
    {
        TaskManger.instance.FinishTask(taskID);
    }
}
