using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class PlayerMessageBroadcaster : MonoBehaviour
{
    [SerializeField] private GameObject messageSpritePrefab;

    private Transform player;
    private Camera cam;

    private List<Message> activeMessages = new List<Message>();
    private const int maxMessages = 3;

    private void Awake()
    {
        player = GameObject.Find("Player").transform;
        cam = Camera.main;
    }

    private void OnEnable()
    {
        TimeTickSystem.OnTick += TimeTickSystem_OnTick;
    }

    private void OnDisable()
    {
        TimeTickSystem.OnTick -= TimeTickSystem_OnTick;
    }

    private void TimeTickSystem_OnTick(object sender, TimeTickSystem.OnTickEventArgs e)
    {
        for (int i = activeMessages.Count - 1; i >= 0; i--)
        {
            activeMessages[i].GameObject.transform.position = cam.WorldToScreenPoint(player.position);

            if (activeMessages[i].IsExpired)
            {
                Destroy(activeMessages[i].GameObject);
                activeMessages.RemoveAt(i);
                return;
            }
        }
    }

    public void Broadcast(PlayerMessageSO message)
    {
        // If there are already max messages, remove the oldest one
        if (activeMessages.Count >= maxMessages)
        {
            Destroy(activeMessages[0].GameObject);
            activeMessages.RemoveAt(0);
        }

        // Instantiate a new message prefab
        GameObject newMessage = Instantiate(messageSpritePrefab, player.position, Quaternion.identity, transform);

        // Initialize the message's text
        var textComponent = newMessage.GetComponentInChildren<TextMeshProUGUI>();
        if (textComponent != null)
        {
            textComponent.text = message.message;
        }

        // Add the new message to the list
        activeMessages.Add(new Message(newMessage, message.showTime));
    }

    private class Message
    {
        public GameObject GameObject { get; private set; }
        private float expirationTime;

        public bool IsExpired => Time.time > expirationTime;

        public Message(GameObject gameObject, float lifetime)
        {
            GameObject = gameObject;
            expirationTime = Time.time + lifetime;
        }
    }
}
