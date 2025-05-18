using UnityEngine;
using UnityEngine.UI;

public class SlideMovementButton : MonoBehaviour
{
    private enum Direction
    {
        Left,
        Right
    }

    [SerializeField] private PlayerInputHandler input;
    [SerializeField] private Direction moveDirectionEnum;
    [SerializeField] private Canvas canvas;

    private RectTransform rect;

    private Vector2 moveDirection => moveDirectionEnum switch
    {
        Direction.Left => new Vector2(-1f, 0f),
        Direction.Right => new Vector2(1f, 0f),
        _ => Vector2.zero
    };

    private void Awake()
    {
        rect = GetComponent<RectTransform>();
    }

    private void Update()
    {
        foreach (Touch t in Input.touches)
        {
            Vector2 tPos = t.position;
            Vector3[] posList = new Vector3[4];
            rect.GetWorldCorners(posList);

            Vector3 pos1 = posList[0];
            Vector3 pos2 = posList[0];

            foreach (Vector3 corner in posList)
            {
                if (corner.x < pos1.x || (corner.x == pos1.x && corner.y < pos1.y))
                {
                    pos1 = corner;
                }

                if (corner.x > pos2.x || (corner.x == pos2.x && corner.y > pos2.y))
                {
                    pos2 = corner;
                }
            }

            if (tPos.x > pos1.x && tPos.x < pos2.x &&
                tPos.y > pos1.y && tPos.y < pos2.y)
            {
                input.Move(moveDirection);
            }
        }
    }
}
