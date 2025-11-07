using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class Boid : MonoBehaviour
{
    public float maxSpeed = 10.0f;
    public float maxForce = 5.0f;
    public float rotationSpeed = 5.0f;

    [Header("Weights")]
    public float sw = 0.5f;
    public float aw = 1.0f;
    public float cw = 1.0f;
    public float fw = 1.0f;

    public float radius = 1.0f;
    public float separationDist = 1.0f;

    private Vector3 acceleration = Vector3.zero;
    private Vector3 velocity = Vector3.up;
    private bool followMouse = false;

    private void Update()
    {
        if (Mouse.current.leftButton.wasPressedThisFrame)
        {
            followMouse = !followMouse;
        }

        ApplyForces(GetNeighbors());
        Integrate();
        Rotate();
        Wrap();
    }

    private void ApplyForces(List<Boid> n)
    {
        acceleration = sw * Separation(n);

        if (followMouse)
        {
            acceleration += fw * FollowMouse();
        }
        else
        {
            acceleration += aw * Alignment(n) + cw * Cohesion(n);
        }

        if (acceleration.magnitude > maxForce)
            acceleration = acceleration.normalized * maxForce;
    }

    private void Integrate()
    {
        velocity += acceleration * Time.deltaTime;

        if (velocity.magnitude > maxSpeed)
            velocity = velocity.normalized * maxSpeed;

        transform.position += velocity * Time.deltaTime;
    }

    private List<Boid> GetNeighbors()
    {
        List<Boid> neighbors = new();

        foreach (Boid b in GameManager.boids)
        {
            if (b != this && Vector3.Distance(b.transform.position, transform.position) < radius) 
                neighbors.Add(b);
        }

        return neighbors;
    }

    private void Rotate()
    {
        float angle = Mathf.Atan2(velocity.y, velocity.x) * Mathf.Rad2Deg;
        Quaternion targetRotation = Quaternion.Euler(0, 0, angle - 90);
        transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, Time.deltaTime * rotationSpeed);
    }

    private void Wrap()
    {
        Camera cam = GameManager.mainCamera;

        Vector3 viewportPos = cam.WorldToViewportPoint(transform.position);

        if (viewportPos.x > 1) viewportPos.x = 0;
        else if (viewportPos.x < 0) viewportPos.x = 1;

        if (viewportPos.y > 1) viewportPos.y = 0;
        else if (viewportPos.y < 0) viewportPos.y = 1;

        transform.position = cam.ViewportToWorldPoint(viewportPos);
    }

    private Vector3 Separation(List<Boid> n)
    {
        Vector3 separation = Vector3.zero; 

        foreach(Boid neighbor in n)
        {
            Vector3 p1 = transform.position;
            Vector3 p2 = neighbor.transform.position;
            Vector3 p1p2 = p2 - p1;
            float dist = p1p2.magnitude;

            if (dist < separationDist)
            {
                if (dist == 0) dist = 0.01f;
                separation -= p1p2.normalized / Mathf.Max(0.1f, dist);
            }
        }

        return separation;
    }

    private Vector3 Alignment(List<Boid> n)
    {
        if (n.Count == 0) return Vector3.zero;

        Vector3 sum = Vector3.zero;

        foreach (Boid neighbor in n)
        {
            sum += neighbor.velocity;
        }

        Vector3 avg = sum / n.Count;

        return avg - velocity;
    }

    private Vector3 Cohesion(List<Boid> n)
    {
        if (n.Count == 0) return Vector3.zero;

        Vector3 sum = Vector3.zero;

        foreach (Boid neighbor in n)
        {
            sum += neighbor.transform.position;
        }

        Vector3 avg = sum / n.Count;

        return avg - transform.position;
    }

    private Vector3 FollowMouse()
    {
        Vector3 targetPos = Camera.main.ScreenToWorldPoint(Mouse.current.position.ReadValue());
        targetPos.z = 0;

        Vector3 desired = targetPos - transform.position;
        if (desired.magnitude == 0) return Vector3.zero;

        desired = desired.normalized * maxSpeed;
        return desired - velocity;
    }

}
