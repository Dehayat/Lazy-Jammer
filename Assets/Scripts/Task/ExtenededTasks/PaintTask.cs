using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class PaintTask : Task
{
    [SerializeField] private Slider slider;
    private void LateUpdate()
    {
        if (slider.value > .99f)
            FinishTask();
    }
}
