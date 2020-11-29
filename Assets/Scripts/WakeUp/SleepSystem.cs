using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SleepSystem : MonoBehaviour
{
    [SerializeField] private GameObject laptop;
    [SerializeField] private float wakeUpDelyPeriod = 1f;
    [SerializeField] private float wakeUpTime = 30;

    private bool isWakeUp , isWon;
    private float startTimer;

    private void Start()
    {
        MusicPlayer.OnLevelEnded += WakeUp;
        TaskManger.instance.OnFinishAllTasks += () => { isWon = true; };
    }
    private void OnDestroy()
    {
        MusicPlayer.OnLevelEnded -= WakeUp;
    }
    public void WakeUp(int levelIndex)
    {
        StartCoroutine(WakeUpDely());
    }
    IEnumerator WakeUpDely()
    {
        yield return new WaitForSeconds(wakeUpDelyPeriod);

        isWakeUp = true;
        startTimer = Time.time;

        laptop.SetActive(true);
    }
    void Sleep()
    {
        TaskManger.instance.DeactiveTaskMenu();

        isWakeUp = false;
        laptop.SetActive(false);
        //TODO tell dehyat to do peace of shit stuff
    }

    void LateUpdate()
    {
        if (!isWon && isWakeUp && Time.time - startTimer > wakeUpTime)
        {
            Sleep();
        }
    }
}
