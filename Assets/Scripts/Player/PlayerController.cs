using System.Collections;
using System.Collections.Generic;
using UnityEditor.Rendering.Universal;
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
    public UpgradeHandler upgradeHandler;

    private Rigidbody2D rb;
    private float moveDirectionX;
    private float moveDirectionY;
    Vector3 dupeBulletPos;
    [SerializeField] private float invulnerabilityFrames;
    

    SpriteRenderer sr;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        sr = GetComponent<SpriteRenderer>();
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

    public void InvulnerabilityFramesStart()
    {
        StartCoroutine(InvulnerabilityFrames());
    }

    private IEnumerator InvulnerabilityFrames()
    {
        stats.isInvulnerable = true;
        sr.color = Color.green;

        yield return new WaitForSeconds(invulnerabilityFrames);

        stats.isInvulnerable = false;
        sr.color = Color.white;
        yield return null;
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
        if (!GameController.isGamePaused)
        {
            GameObject obj = BulletPooler.current.GetPooledObject();
            if (obj == null) return;
            obj.transform.position = bulletPosition.position;
            obj.SetActive(true);

            if (upgradeHandler.upgrades[0].isApplied)
            {
                GameObject dobj = BulletPooler.current.GetPooledObject();
                if (dobj == null) return;
                dupeBulletPos = bulletPosition.position;
                dupeBulletPos.x += 0.3f;
                dobj.transform.position = dupeBulletPos;
                dobj.SetActive(true);
            }
        }
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
