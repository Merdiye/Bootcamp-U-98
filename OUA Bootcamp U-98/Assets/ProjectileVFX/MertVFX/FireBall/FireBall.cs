using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireBall : MonoBehaviour
{
    public LayerMask player;
    public float damage = 10f;

    private void OnTriggerEnter(Collider other)
    {
        // �arpt��� nesnenin oyuncu olup olmad���n� kontrol et
        if (((1 << other.gameObject.layer) & player) != 0)
        {
            // Singleton instance'� kullanarak oyuncuya hasar ver
            PlayerHealth.Instance.TakeDamage(damage);
        }

        // �arpmadan sonra fireball'u yok et
        Destroy(gameObject);
    }
}
