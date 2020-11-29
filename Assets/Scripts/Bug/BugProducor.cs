using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BugProducor : MonoBehaviour
{
    [SerializeField] private GameObject bugPrefab;
    [SerializeField] private Transform bugParent;
    public int bugCount = 15;
    void Start()
    {
        for (int i = 0; i < bugCount; i++)
        {
            GameObject bug = Instantiate(bugPrefab, bugParent);
            bug.GetComponent<Bug>().CreateRandomPath();
        }
    }
}
