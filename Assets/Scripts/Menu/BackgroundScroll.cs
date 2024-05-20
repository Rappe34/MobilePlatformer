using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackgroundScroll : MonoBehaviour
{
    [SerializeField] private List<Transform> images;
    [SerializeField] private float scrollSpeed = .75f;

    private Camera cam;
    private float camLeftEdgeX;

    private void Awake()
    {
        cam = Camera.main;
    }

    private void Start()
    {
        camLeftEdgeX = cam.ViewportToWorldPoint(new Vector3(0, 0, cam.nearClipPlane)).x;
        float currentX = camLeftEdgeX;

        foreach (Transform image in images)
        {
            float imageWidth = image.GetComponent<Renderer>().bounds.size.x;
            image.position = new Vector3(currentX + imageWidth / 2, image.position.y, image.position.z);
            currentX += imageWidth;
        }
    }

    private void Update()
    {
        foreach (Transform image in images)
        {
            image.Translate(Vector2.left * scrollSpeed * Time.deltaTime);

            float imageWidth = image.GetComponent<Renderer>().bounds.size.x;

            if (image.position.x + imageWidth / 2 < camLeftEdgeX)
            {
                image.position += new Vector3(GetPosShift(), 0, 0);
            }
        }
    }

    private float GetPosShift()
    {
        float dx = 0;
        foreach (Transform image in images)
        {
            dx += image.GetComponent<Renderer>().bounds.size.x;
        }
        return dx;
    }
}
