using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameplayManager : MonoBehaviour
{

    public GameObject destroyWin;
    public SpriteRenderer playerSprite;
    public MusicPlayer musicPlayer;

    void Start()
    {
        TaskManger.instance.OnFinishAllTasks += Instance_OnFinishAllTasks;
    }

    private void Instance_OnFinishAllTasks()
    {
        Destroy(destroyWin, 0);
        musicPlayer.isRandoming = true;
        playerSprite.color = new Color(0.7921569f, 0.4588235f, 0.627451f);
    }
}
