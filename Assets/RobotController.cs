using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class RobotController : MonoBehaviour
{
    [Header("Movement Settings")]
    public float moveSpeed = 3f;
    public float rotationSpeed = 5f;
    public float hoverHeight = 0.5f;

    private List<Vector3> currentPath = new List<Vector3>();
    private int currentPathIndex = 0;
    private bool isMoving = false;

    [Header("Animation")]
    public Transform bodyTransform; // Assign the Body child
    private float bobSpeed = 4f;
    private float bobAmount = 0.1f;

    void Update()
    {
        if (isMoving && currentPath.Count > 0)
        {
            MoveAlongPath();
            BobAnimation();
        }
    }

    void BobAnimation()
    {
        // Simple hover/bob animation
        if (bodyTransform != null)
        {
            float newY = Mathf.Sin(Time.time * bobSpeed) * bobAmount;
            bodyTransform.localPosition = new Vector3(0, newY, 0);
        }
    }

    void MoveAlongPath()
    {
        if (currentPathIndex >= currentPath.Count)
        {
            isMoving = false;
            Debug.Log("Robot reached destination!");
            return;
        }

        Vector3 targetPos = currentPath[currentPathIndex];
        targetPos.y = hoverHeight; // Keep robot at hover height

        // Rotate towards target
        Vector3 direction = (targetPos - transform.position).normalized;
        if (direction != Vector3.zero)
        {
            Quaternion targetRotation = Quaternion.LookRotation(direction);
            transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, rotationSpeed * Time.deltaTime);
        }

        // Move towards target
        transform.position = Vector3.MoveTowards(transform.position, targetPos, moveSpeed * Time.deltaTime);

        // Check if reached waypoint
        if (Vector3.Distance(transform.position, targetPos) < 0.1f)
        {
            currentPathIndex++;
        }
    }

    public void SetPath(List<Vector3> path)
    {
        currentPath = path;
        currentPathIndex = 0;
        isMoving = true;

        Debug.Log($"Robot received path with {path.Count} waypoints");
    }

    public void StopMoving()
    {
        isMoving = false;
        currentPath.Clear();
    }
}