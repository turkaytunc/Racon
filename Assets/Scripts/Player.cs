using UnityEngine;


public class Player : MonoBehaviour
{
    [Header("Laser")]
    [SerializeField] private GameObject laserPrefab;
    [SerializeField] private float fireRate = 10f;

    [SerializeField] private float health = 500;
    [SerializeField] private GameObject explosionPrefab;
    [SerializeField] private AudioClip playerDeathSound;
    [SerializeField] private AudioClip laserSound;

    private GameManager gameManager;
    private float moveSpeed;
    private float laserOffset;
    private float timeToShoot;

    #region Player Position Variables
    private float minPlayerXPos;
    private float maxPlayerXPos;
    private float minPlayerYPos;
    private float maxPlayerYPos;

    private float playerPositionOffsetY = .5f;
    private float playerPositionOffsetX = .25f;

    #endregion
    private void Start()
    {
        gameManager = GameObject.FindGameObjectWithTag("GameManager").GetComponent<GameManager>();
        timeToShoot = 1 / fireRate;
        moveSpeed = 7f;
        laserOffset = .5f;
        PlayerMovementBoundaries();
        gameManager.SetPlayerHealth(this.health);
    }


    void Update()
    {
        CalculatePlayerShipMovement();
        ShootLaser();
    }

    private void ShootLaser()
    {
        timeToShoot -= Time.deltaTime;

        if (Input.GetButton("Fire1") && timeToShoot <= 0)
        {
            Vector3 laserPosition = new Vector3(transform.position.x, transform.position.y + laserOffset, transform.position.z);
            Instantiate(laserPrefab, laserPosition, Quaternion.identity);
            AudioSource.PlayClipAtPoint(laserSound, Camera.main.transform.position, 0.3f);
            timeToShoot = 1 / fireRate;
        }
    }

    private void PlayerMovementBoundaries()
    {
        Camera mainCamera = Camera.main;

        minPlayerXPos = mainCamera.ViewportToWorldPoint(new Vector3(0, 0, 0)).x;
        maxPlayerXPos = mainCamera.ViewportToWorldPoint(new Vector3(1, 0, 0)).x;

        minPlayerYPos = mainCamera.ViewportToWorldPoint(new Vector3(0, 0, 0)).y;
        maxPlayerYPos = mainCamera.ViewportToWorldPoint(new Vector3(0, 1, 0)).y;
    }

    private void CalculatePlayerShipMovement()
    {
        Vector2 playerPosition = CalculatePlayerInputValues();

        float newXPos = Mathf.Clamp(playerPosition.x, minPlayerXPos + playerPositionOffsetX, maxPlayerXPos - playerPositionOffsetX);
        float newYPos = Mathf.Clamp(playerPosition.y, minPlayerYPos + playerPositionOffsetY, maxPlayerYPos - playerPositionOffsetY);

        transform.position = Vector3.Lerp(transform.position, new Vector3(newXPos, newYPos, transform.position.z), .9f);
    }

    private Vector2 CalculatePlayerInputValues()
    {
        float xPos, yPos;
        xPos = transform.position.x + Input.GetAxisRaw("Horizontal") * Time.deltaTime * moveSpeed;
        yPos = transform.position.y + Input.GetAxisRaw("Vertical") * Time.deltaTime * moveSpeed;

        return new Vector2(xPos, yPos);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        int damage = collision.gameObject.GetComponent<DamageDealer>().GetDamage();
        health -= damage;

        gameManager.SetPlayerHealth(this.health);

        SelfDestruction();
    }

    private void SelfDestruction()
    {
        if (health <= 0)
        {
            Instantiate(explosionPrefab, transform.position, Quaternion.identity);
            AudioSource.PlayClipAtPoint(playerDeathSound, Camera.main.transform.position, .5f);
            gameManager.GameOver();
            Destroy(gameObject);
        }
    }
}
