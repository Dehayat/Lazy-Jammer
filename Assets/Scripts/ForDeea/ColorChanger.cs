using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ColorChanger : MonoBehaviour
{

    public Color[] colors;

    public Color goodColor;

    private int currentColor = 0;
    private SpriteRenderer sprite;
    private GameObject outineChild;

    private void Start()
    {
        sprite = GetComponent<SpriteRenderer>();
        currentColor = 0;
        sprite.color = colors[currentColor];
        if (transform.childCount > 0)
        {
            outineChild = transform.GetChild(0).gameObject;
        }
        UnityEngine.Random.InitState(0);
    }


    private void OnMouseDown()
    {
        //currentColor++;
        //if (currentColor == colors.Length) currentColor = 0;
        //sprite.color = colors[currentColor];
        sprite.color = new Color(UnityEngine.Random.Range(0.1f, 0.9f), UnityEngine.Random.Range(0.1f, 0.9f), UnityEngine.Random.Range(0.1f, 0.9f));
    }
    private void OnMouseEnter()
    {
        if (outineChild)
        {
            outineChild.SetActive(true);
        }
    }
    private void OnMouseExit()
    {
        if (outineChild)
        {
            outineChild.SetActive(false);
        }
    }
    public float GetScore()
    {
        //Vector4 color = (goodColor - colors[currentColor]);
        Vector4 color = goodColor - sprite.color;
        return Mathf.Clamp(color.magnitude, 0, 1);
    }
}
