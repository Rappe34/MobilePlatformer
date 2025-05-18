using UnityEngine;

[CreateAssetMenu(menuName = "ScriptableObject/PlayerMessage")]
public class PlayerMessageSO : ScriptableObject
{
    [TextArea]
    public string message;
    public float showTime = 2f;
}
