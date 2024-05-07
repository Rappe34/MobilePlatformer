using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackgroundScroll : MonoBehaviour
{
    [SerializeField] private List<Transform> images;
    [SerializeField] private float scrollSpeed = 1.25f;

    private Camera cam;

    private void Awake()
    {
        cam = Camera.main;
    }

    void Update()
    {
        foreach (Transform image in images)
        {
            image.Translate(Vector2.left * scrollSpeed * Time.deltaTime);

            float camLeftEdgeX = cam.ViewportToWorldPoint(new Vector3(0, 0, cam.nearClipPlane)).x;

            if (image.position.x + image.GetComponent<RectTransform>().rect.width < camLeftEdgeX)
            {
                image.position += new Vector3(GetPosShift(), 0);
            }
        }
    }

    private float GetPosShift()
    {
        float dx = 0;
        foreach (Transform image in images)
        {
            dx += image.GetComponent<RectTransform>().rect.width;
        }
        return dx;
    }
}
