using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
public class BugPointGenerator 
{

    const float BORDAR_SIZE = .5f;
    const int SIDES_COUNTS = 4;
    private static Vector2 GetScreenScale()
    {
        Camera camera = Camera.main;
        float height = camera.orthographicSize * 2;
        float weight = camera.orthographicSize * camera.aspect * 2;

        return new Vector2(weight , height);
    }
    private static float GetRandomPoint(float scale)
    {
        return UnityEngine.Random.Range(-scale / 2, scale / 2);
    }
    private static Vector2 GetRandomPoint(int side)
    {
        Vector2 screenScale = GetScreenScale();
        switch (side)
        {
            case 0:
                return new Vector2(GetRandomPoint(screenScale.x), screenScale.y / 2 + BORDAR_SIZE);
            case 1:
                return new Vector2(GetRandomPoint(screenScale.x), -(screenScale.y / 2 + BORDAR_SIZE));
            case 2:
                return new Vector2(screenScale.x / 2 + BORDAR_SIZE, GetRandomPoint(screenScale.y));
            case 3:
                return new Vector2(-(screenScale.x / 2 + BORDAR_SIZE), GetRandomPoint(screenScale.y));
        }
        throw new Exception("Input not correct");
    }

    public static (Vector2, Vector2) CreateDiffSidePoint()
    {
        int sideA = UnityEngine.Random.Range(0, SIDES_COUNTS);
        int sideB = (sideA + UnityEngine.Random.Range(0, SIDES_COUNTS - 1) + 1) % 4;

        return (GetRandomPoint(sideA), GetRandomPoint(sideB));
    }

}
