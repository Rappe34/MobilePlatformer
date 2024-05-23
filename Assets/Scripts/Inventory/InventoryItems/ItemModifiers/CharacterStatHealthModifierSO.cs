using UnityEngine;
using HealthSystem;

[CreateAssetMenu(menuName = "ScriptableObject/Inventory/CharacterStatHealthModifier")]
public class CharacterStatHealthModifierSO : CharacterStatModifierSO
{
    public override void AffectCharacter(GameObject character, float val)
    {
        Health health = character.GetComponent<Health>();
        if (health != null)
            health.AddHealth((int)val);
    }
}
