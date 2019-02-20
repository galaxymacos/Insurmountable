using UnityEngine;

public class Enemy : Character
{
    public EnemyState _enemyCurrentState;

    // Private 
    private GameObject _player;
    public int atk = 5;
    public float attackRange = 2f;
    public float attackSpeed = 1f; // 1 hit in 1 sec

    // Ability
    public bool deadDueToDeadZone;
    public int defense = 1;
    public GameObject floatingHpPlace;
    public float HP = 200;
    public InGameUi InGameUi;
    private bool isLayedOnGround;

    // buff and 
    private bool isStiffed;

    public float laySec = 2f; // How many seconds the enemy will stay on ground if it got hit to the air
    public float laySecLeft;
    public float moveSpeed = 5f;
    public float nextAttackTime;
    public bool playerInRange;
    public Rigidbody rb;


    internal float StiffTimeRemain;


    // Start is called before the first frame update
    private void Start()
    {
        _enemyCurrentState = EnemyState.Standing;
        GameManager.Instance.gameObjects.Add(gameObject);
        _player = GameManager.Instance.player;
        rb = GetComponent<Rigidbody>();
        nextAttackTime = 0;
    }


    public void TakeDamage(float damage, float stiffTime = 0, bool knockInAir = false)
    {
        HP -= damage - defense;
        if (HP <= 0) Destroy(gameObject);
        StiffTimeRemain = stiffTime;

        InGameUi.DisplayFloatingDamage(floatingHpPlace.transform, damage);
        if (knockInAir)
        {
            _enemyCurrentState = EnemyState.GotHitToAir;
        }
    }

    private void Update()
    {
        if (StiffTimeRemain > 0) isStiffed = true;
        if (isStiffed)
        {
            StiffTimeRemain -= Time.deltaTime;
            if (StiffTimeRemain <= 0) isStiffed = false;
        }

        if (laySecLeft > 0) isLayedOnGround = true;

        if (_enemyCurrentState == EnemyState.LayOnGround)
        {
            laySecLeft -= Time.deltaTime;
            if (laySecLeft <= 0) _enemyCurrentState = EnemyState.Standing;
        }
    }

    private void FixedUpdate()
    {
        if (!isStiffed && _enemyCurrentState == EnemyState.Standing)
        {
            var position = transform.position;
            if ((position - _player.transform.position).magnitude > attackRange)
            {
                playerInRange = false;
                var chasingDirection = (_player.transform.position - position).normalized;
                rb.MovePosition(position + chasingDirection * moveSpeed * Time.fixedDeltaTime);
            }
            else
            {
                playerInRange = true;
            }

            if (playerInRange)
                if (Time.time >= nextAttackTime)
                {
                    Attack();
                    nextAttackTime = Time.time + 1 / attackSpeed;
                }
        }
    }


    private void Attack()
    {
        if (_player.GetComponent<PlayerMovement>().playerCurrentState != PlayerMovement.PlayerState.Block)
        {
            _player.GetComponent<Player>().TakeDamage(atk);            
        }
        else
        {
            print("Attack was blocked");
        }
    }

    private void OnCollisionEnter(Collision other)
    {
        if (deadDueToDeadZone && other.gameObject.layer == LayerMask.NameToLayer("Deadly")) Destroy(gameObject);
        if (deadDueToDeadZone && other.gameObject.layer == LayerMask.NameToLayer("Ground"))
            if (_enemyCurrentState == EnemyState.GotHitToAir)
            {
                laySecLeft = laySec;
                _enemyCurrentState = EnemyState.LayOnGround;
            }
    }


    // State
    public enum EnemyState
    {
        Standing,
        GotHitToAir,
        LayOnGround
    }
}