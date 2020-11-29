using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
public class TaskManger : MonoBehaviour
{
    public static TaskManger instance;

    [SerializeField] private GameObject taskMenu;
    [SerializeField] private GameObject[] tasks;
    [SerializeField] private SpriteRenderer[] spriteRenderers;
    public event Action OnFinishAllTasks;

    private bool[] isFinished;
    private int unfinishedBusiness;

    private void Awake()
    {
        instance = this;
    }
    private void Start()
    {
        isFinished = new bool[tasks.Length];
        unfinishedBusiness = tasks.Length;
    }

    public void ActiveTaskMenu()
    {
        for (int i = 0; i < isFinished.Length; i++)
        {
            tasks[i].SetActive(false);
        }
        taskMenu.SetActive(true);
    }
    public void DeactiveTaskMenu()
    {
        for (int i = 0; i < isFinished.Length; i++)
        {
            DeactiveTask(i);
        }
        taskMenu.SetActive(true);
    }
    public void ActiveTask(int taskID)
    {
        if (isFinished[taskID]) return;
        tasks[taskID].SetActive(true);
        taskMenu.SetActive(false);
    }
    void DeactiveTask(int taskID)
    {
        tasks[taskID].SetActive(false);
    }

    public void FinishTask(int taskID)
    {
        DeactiveTask(taskID);
        ActiveTaskMenu();
        spriteRenderers[taskID].color = new Color(0.2487184f, 0.8396226f, 0.3403681f, 1);
        isFinished[taskID] = true;
        if (--unfinishedBusiness == 0)
        {
            OnFinishAllTasks?.Invoke();
        }
    }
}
