using UnityEngine;

public class Enemy : MonoBehaviour
{
    [SerializeField] private float health = 500;
    [SerializeField] [Range(100, 500)] private float scoreAmount = 100;
    [SerializeField] private GameObject explosionPrefab;

    [SerializeField] private GameObject laserPrefab;
    [SerializeField] private float fireRate = 2f;
    [SerializeField] private AudioClip explosionSound;

    private GameManager gameManager;

    private float setShootTimer;
    private float timeToShoot = 0;
    private float laserOffset;

    private void Start()
    {
        laserOffset = -1f;
        setShootTimer = 1 / fireRate;
    }

    private void Update()
    {
        Shoot();
    }

    private void Shoot()
    {
        timeToShoot -= Time.deltaTime;

        if (timeToShoot <= 0)
        {
            Vector3 laserPosition = new Vector3(transform.position.x, transform.position.y + laserOffset, transform.position.z);
            Instantiate(laserPrefab, laserPosition, Quaternion.identity);
            timeToShoot = setShootTimer;
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {     
        int damage = collision.gameObject.GetComponent<DamageDealer>().GetDamage();
        health -= damage;

        if (health <= 0)
        {
            gameManager = GameObject.FindGameObjectWithTag("GameManager").GetComponent<GameManager>();

            Instantiate(explosionPrefab, transform.position, Quaternion.identity);

            gameManager.SetScore(this.scoreAmount);
            Destroy(gameObject);

            AudioSource.PlayClipAtPoint(explosionSound, Camera.main.transform.position, 0.05f);
        }      
    }
}
