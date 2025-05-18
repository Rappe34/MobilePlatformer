using UnityEngine;
using UnityEngine.Events;

public class LoseEvents : MonoBehaviour
{
    [SerializeField] private float killHeight;

    public UnityEvent OnPlayerFall;

    private Transform player;

    private void Start()
    {
        player = GameObject.Find("Player").transform;
    }

    private void Update()
    {
        if (player == null) return;

        if (player.position.y < killHeight)
        {
            OnPlayerFall?.Invoke();
        }
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawLine(new Vector3(-1000f, killHeight, 0f), new Vector3(1000f, killHeight, 0f));
    }
}
