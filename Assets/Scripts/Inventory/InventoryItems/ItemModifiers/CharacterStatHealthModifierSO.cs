using UnityEngine;

[CreateAssetMenu(menuName = "ScriptableObject/Inventory/CharacterStatHealthModifier")]
public class CharacterStatHealthModifierSO : CharacterStatModifierSO
{
    public override void AffectCharacter(GameObject character, float val)
    {
        EnemyHealth health = character.GetComponent<EnemyHealth>();
        if (health != null)
            health.AddHealth((int)val);
    }
}
