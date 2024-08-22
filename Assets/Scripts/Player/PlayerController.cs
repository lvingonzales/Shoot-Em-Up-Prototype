using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public BulletController bulletController;
    public GameEvent onPlayerDamage;
    public GameEvent onPlayerDeath;
    public PlayerStats stats;
    public float firerate;
    public Transform bulletPosition;
    public GameObject bullet;
    public float moveSpeed;
    public GameEvent uiUpdate;
    public GameEvent levelUp;

    private Rigidbody2D rb;
    private float moveDirectionX;
    private float moveDirectionY;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    private void Start()
    {
        InvokeRepeating("Shoot", 0f, firerate);
    }

    private void Update()
    {
        ProcessInputs();
    }

    private void FixedUpdate()
    {
        Move();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        stats.hpValue = stats.hpValue - 2;
        onPlayerDamage.TriggerEvent();
        uiUpdate.TriggerEvent();
        if (stats.hpValue <= 0)
        {
            Debug.Log("IM DEAD");
            onPlayerDeath.TriggerEvent();
            gameObject.SetActive(false);
        }
    }

    public void Experience ()
    {
        if (stats.level != stats.MAXLEVEL)
        {
            stats.expValue++;
            if (stats.expValue >= stats.expMaxValue)
            {
                levelUp.TriggerEvent();
                stats.LevelUp();
            }
        }  
    }

    void Missile()
    {
        GameObject obj = MissilePooler.current.GetPooledObject();
        if (obj == null) return;
        obj.transform.position = bulletPosition.position;
        obj.SetActive(true);
        stats.MissileFired();
        uiUpdate.TriggerEvent();
    }

    void Shoot()
    {
        GameObject obj = BulletPooler.current.GetPooledObject();
        if (obj == null) return;
        obj.transform.position = bulletPosition.position;
        obj.SetActive(true);
    }

    public void StopShooting()
    {
        CancelInvoke("Shoot");
    }

    private void Move()
    {
        rb.velocity = new Vector2(moveDirectionX * moveSpeed, moveDirectionY * moveSpeed);
    }

    private void ProcessInputs()
    {
        if (!GameController.isGamePaused)
        {
            moveDirectionX = Input.GetAxis("Horizontal");
            moveDirectionY = Input.GetAxis("Vertical");
            if (Input.GetMouseButtonDown(0) && stats.missileCount > 0)
            {
                Missile();
            }
        }
    }
}
