using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SleepSystem : MonoBehaviour
{
    [SerializeField] private GameObject laptop;
    [SerializeField] private float wakeUpDelyPeriod = 1f;
    [SerializeField] private float wakeUpTime = 30;

    private bool isWakeUp;
    private float startTimer;

    private void Start()
    {
        //TODO listen to the fucking event
    }
    public void WakeUp()
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
        isWakeUp = false;
        laptop.SetActive(false);
        //TODO tell dehyat to do peace of shit stuff
    }

    void LateUpdate()
    {
        if (isWakeUp && Time.time - startTimer > wakeUpTime)
        {
            Sleep();
        }
    }
}
