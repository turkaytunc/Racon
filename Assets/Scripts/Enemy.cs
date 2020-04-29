using UnityEngine;

public class Enemy : MonoBehaviour
{
    [SerializeField] private float health = 500;
    [SerializeField] [Range(100, 500)] private float scoreAmount = 100;
    [SerializeField] private GameObject explosionPrefab;

    [Header("Shooting Variables")]
    [SerializeField] private GameObject laserPrefab;
    [SerializeField] [Range(1, 3)] private float maximumFireRate = 2f;
    [SerializeField] [Range(0.25f, 1)] private float minimumFireRate = 1f;
    [SerializeField] private AudioClip explosionSound;

    [Header("Item Drop")]
    [SerializeField] private GameObject powerUpPrefab;
    [SerializeField] private float powerUpChance;

    private GameManager gameManager;

    private float setShootTimer;
    private float timeToShoot = 0;
    private float laserOffset;

    private void Start()
    {
        SetInitialValuesToPrivateVariables();
    }

    private void SetInitialValuesToPrivateVariables()
    {
        laserOffset = -1f;
        setShootTimer = 1 / maximumFireRate;
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
            setShootTimer = 1 / Random.Range(minimumFireRate,maximumFireRate);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        int damage = collision.gameObject.GetComponent<DamageDealer>().GetDamage();
        health -= damage;

        CheckCurrentHealth();
    }

    private void CheckCurrentHealth()
    {
        if (health <= 0)
        {
            gameManager = GameObject.FindGameObjectWithTag("GameManager").GetComponent<GameManager>();

            Instantiate(explosionPrefab, transform.position, Quaternion.identity);
            if (Random.Range(powerUpChance, 100f) > 80)
            {
                Instantiate(powerUpPrefab, transform.position, Quaternion.identity);
            }

            gameManager.SetScore(scoreAmount);
            Destroy(gameObject);

            AudioSource.PlayClipAtPoint(explosionSound, Camera.main.transform.position, 0.05f);
        }
    }
}
