using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[System.Serializable]
public struct BugMovementState
{
    public float scale, speed;
}
public class BugMovement : MonoBehaviour
{
    [SerializeField] 
    private BugMovementState[] movementSpeed;
    private int currentSpeedState = 0;
    private Vector2 targetPoint;

    public void UpdatePath(Vector2 targetPoint)
    {
        this.targetPoint = targetPoint;
        UpdateRotation();
    }
    public void UpdatePath(Vector2 bugPosition , Vector2 targetPoint)
    {
        this.targetPoint = targetPoint;
        transform.position = bugPosition;
        UpdateRotation();
    }
    public bool IsFinishPath()
    {
        return Vector2.Distance(transform.position, targetPoint) < .1f;
    }
    void UpdateRotation()
    {
        Vector3 vectorToTarget = targetPoint - (Vector2)transform.position;
        float angle = Mathf.Atan2(vectorToTarget.y, vectorToTarget.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.AngleAxis(angle - 90, Vector3.forward);
    }
    void Update()
    {
        transform.position = Vector3.MoveTowards(transform.position, targetPoint, movementSpeed[currentSpeedState].speed * Time.deltaTime);
    }
    public void UpdateScale()
    {
        transform.localScale = Vector3.one * movementSpeed[currentSpeedState].scale;
    }
    public bool IncreaseSpeed()
    {
        if (currentSpeedState + 1 >= movementSpeed.Length) return false;
        currentSpeedState++;
        UpdateScale();
        return true;
    }
}
