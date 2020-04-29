using UnityEngine;

public class DamageDealer : MonoBehaviour
{
    [SerializeField] private int damageNumber = 100;

    public int GetDamage()
    {
        return damageNumber;
    }
}
