using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//Class này dùng để chứa các thông tin về lượng sát thương mà đối tượng va chạm với người chơi sẽ gây ra
public class DamageDealer : MonoBehaviour
{
    [SerializeField] private int damage = 100;

    public int GetDamage()
    {
        return damage;
    }

    public void Hit()
    {
        Destroy(gameObject);
    }

    public void SetDamage(int damage)
    {
        this.damage = damage;
    }
}
