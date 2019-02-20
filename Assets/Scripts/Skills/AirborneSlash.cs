using UnityEngine;

namespace Skills
{
    public class AirborneSlash : Skill
    {
        [SerializeField] private float damage = 10f;
        public AirborneSlashEnemyDetector EnemyDetector;

        public Vector3 enemyKnockdownForce;

        [SerializeField] private float stiffTimeWhenHit = 1f;

        public override void Start()
        {
            base.Start();
            playerController.onFacingChangeCallback += CreateAirborneSlashCollider;
        }

        private void Update()
        {
            if (!_skillNotOnCooldown)
            {
                if (TimePlayed + cooldown <= Time.time) _skillNotOnCooldown = true;
            }
        }

        public override void Play()
        {
            if (_skillNotOnCooldown)
            {
                _skillNotOnCooldown = false;
                base.Play();

                print("slash!");
                var enemies = EnemyDetector._enemiesInRange;
                foreach (var enemy in enemies)
                {
                    enemy.GetComponent<Rigidbody>().velocity = Vector3.zero;
                    enemy.GetComponent<Rigidbody>().AddForce(enemyKnockdownForce);
                    enemy.GetComponent<Enemy>().TakeDamage(damage, stiffTimeWhenHit, true);
                }
            }
            else
            {
                print("Skill is on cooldown");
            }
        }

        public void CreateAirborneSlashCollider(bool isFacingRight)
        {
            enemyKnockdownForce = new Vector3(-enemyKnockdownForce.x,enemyKnockdownForce.y,enemyKnockdownForce.z);
        }
    }
}