using UnityEngine;

public class PowerUp : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.tag == "PlayerShip")
        {
            collision.transform.GetComponent<Player>().SetTripleShoot(true);
            Destroy(gameObject);
        }
    }
}
