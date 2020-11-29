using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StartGame : MonoBehaviour
{
    public KeyCode[] playKeys;

    private bool isStarting;

    // Update is called once per frame
    void Update()
    {
        if (isStarting) return;
        bool start = false;
        foreach (var key in playKeys)
        {
            start |= Input.GetKeyDown(key);
        }
        start |= Input.GetMouseButtonDown(0);
        if (!isStarting && start)
        {
            isStarting = true;
            UnityEngine.SceneManagement.SceneManager.LoadScene(1);
        }
    }
}
