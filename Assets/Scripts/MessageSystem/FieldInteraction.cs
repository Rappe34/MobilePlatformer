using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FieldInteraction : MonoBehaviour
{
    [SerializeField] private GameObject interactMessage;

    private Transform player;
    private Camera cam;

    private void Awake()
    {
        player = GameObject.Find("Player").transform;
        cam = Camera.main;

        interactMessage.SetActive(false);
    }

    private void Update()
    {
        if (interactMessage.activeSelf)
            interactMessage.transform.position = cam.WorldToScreenPoint(player.position);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("InteractibleEnvironment"))
        {
            interactMessage.SetActive(true);
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("InteractibleEnvironment"))
        {
            interactMessage.SetActive(false);
        }
    }
}
