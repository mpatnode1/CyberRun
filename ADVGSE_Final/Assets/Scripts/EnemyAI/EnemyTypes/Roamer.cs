using System.Runtime.CompilerServices;
using UnityEngine;

public class Roamer : MonoBehaviour
{
    /// <summary>
    /// Points that Roamer Enemy moves back and forth from.
    /// </summary>
    [SerializeField] Transform[] points;

    /// <summary>
    /// Enemy's movement speed.
    /// </summary>
    [SerializeField] float movementSpeed;

    [SerializeField] int targetPoint = 0;


    void FixedUpdate()
    {
        if (transform.position == points[targetPoint].position)
        {
            transform.Rotate(0, 180, 0, Space.Self);
            increaseTargetInt();
        }
        transform.position = Vector3.MoveTowards(transform.position, points[targetPoint].position, movementSpeed * Time.deltaTime);

    }

    void increaseTargetInt()
    {
        targetPoint++;
        if (targetPoint >= points.Length)
        {
            targetPoint = 0;
        }

    }
}
