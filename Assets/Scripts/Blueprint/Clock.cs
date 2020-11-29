using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Clock : MonoBehaviour
{
    public static Clock instance;
    [SerializeField] private Transform arrow;
    [SerializeField] private int extraLap = 3;
    [SerializeField] private AnimationCurve distanceToSpeed;
    private float currentAngle, targetAngle;
    public bool isOver = false;

    private void Awake()
    {
        instance = this;
    }
    public void UpdateAngle(int index)
    {
        float correctAngle = (360 / 7f) * index;
        targetAngle = (extraLap +  Mathf.Ceil(currentAngle / 360)) * 360 + correctAngle;
    }
    void Update()
    {
        currentAngle = Mathf.MoveTowards(currentAngle, targetAngle, Time.deltaTime * distanceToSpeed.Evaluate(targetAngle - currentAngle));
        arrow.transform.rotation = Quaternion.Euler(0, 0, currentAngle);
        if (targetAngle - currentAngle < .1)
            isOver = true;
        else isOver = false;
    }
}
