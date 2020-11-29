using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SineScale : MonoBehaviour
{
    [SerializeField] private float startScale = 1, endScale = 2;
    [SerializeField] private float speed;
    void Update()
    {
        transform.localScale = Vector3.one * Mathf.Lerp(startScale, endScale, (Mathf.Sin(Time.time * speed) + 1) / 2);     
    }
}
