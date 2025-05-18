using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Elevator : MonoBehaviour
{
    [SerializeField] private float speed;
    [SerializeField] private List<Transform> points;

    private bool running = true;
    private bool reverse = false;
    private int targetIndex = 0;

    private void Start()
    {
        transform.position = points[0].position;
    }

    void FixedUpdate()
    {
        if (Vector3.Distance(transform.position, points[targetIndex].position) < 0.01f)
        {
            running = false;
            StartCoroutine(EnableWait());

            if (targetIndex == points.Count - 1)
            {
                reverse = true;
                targetIndex--;
                return;
            }
            else if (targetIndex == 0)
            {
                reverse = false;
                targetIndex++;
                return;
            }

            if (reverse)
            {
                targetIndex--;
            }
            else
            {
                targetIndex++;
            }
        }

        if (running)
        {
            transform.position = Vector3.MoveTowards(transform.position, points[targetIndex].position, speed * Time.fixedDeltaTime);
        }
    }

    private IEnumerator EnableWait()
    {
        yield return new WaitForSeconds(1f);

        running = true;
    }
}
