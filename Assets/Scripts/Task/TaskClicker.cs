using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TaskClicker : MonoBehaviour
{
    [SerializeField] private int taskID;
    void OnMouseDown()
    {
        TaskManger.instance.ActiveTask(taskID);
    }
}
