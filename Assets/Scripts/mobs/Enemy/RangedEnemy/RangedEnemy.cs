using UnityEngine;
using System.Collections;

public class RangedEnemy : Mob {

    private int speed = 40, followDistance = 8;
    private Vector3 playerPosition;

    public Sprite[] spriteAttack, spriteWalk, spriteIdle;
    private int walkingFrame;
    private int idleFrame;

    public override string getName() {
        return "RangedEnemy";
    }

    void create() {
        body = new GameObject("PlayerHead");
        body.transform.SetParent(transform);
        body.transform.position = transform.position;
        body.AddComponent<SpriteRenderer>();
        body.GetComponent<SpriteRenderer>().material = (Material)Resources.Load("MapMaterial");
        body.GetComponent<SpriteRenderer>().sortingOrder = 2;
        stats.baseStrength = 15;
        stats.baseDexterity = 5;
        // Z exp = 100
        stats.exp = 100;
    }

    // Use this for initialization
    public override void OnStart() {
        setDropRate(30);
        setArmourDrops(new int[] {3,4,5,6});
        setSkillDrops(new int[] {0,1,2,3});
        create();
        transform.rotation = new Quaternion(0, 0, 0, 0);
        spriteAttack = Resources.LoadAll<Sprite>("Sprite/zombieAttack");
        spriteWalk = Resources.LoadAll<Sprite>("Sprite/zombieWalk");
        spriteIdle = Resources.LoadAll<Sprite>("Sprite/zombieIdle");
        replaceSkill(0, new SkillBasicAttack());
        skills[0].properties["attackSpeed"] = .8f;
    }

    // Update is called once per frame
    public override void OnUpdate() {
        if (isAttacking)
            return;
        if (GameObject.FindWithTag("Player")) {
            float distance = Mathf.Sqrt(Mathf.Pow(playerPosition.x - transform.position.x, 2) + Mathf.Pow(playerPosition.y - transform.position.y, 2));
            RaycastHit2D hitPlayer = Physics2D.Raycast(body.transform.position + (playerPosition - body.transform.position).normalized * GetComponent<CircleCollider2D>().radius * 1.1f, playerPosition - body.transform.position);
            if (distance <= 12 && hitPlayer.collider.CompareTag("Player"))
                skills[0].useSkill(this);
        }
    }

    public override Transform headTransform {
        get {
            return body.transform;
        }
    }

    private float timer;
    public override void movement() {
        if (isAttacking)
            return;
        if (GameObject.FindWithTag("Player")) {
            GameObject player = GameObject.FindWithTag("Player");
            playerPosition = player.transform.position;
            Vector3 diff = (getTargetLocation() - feetTransform.position).normalized;
            float rot_z = Mathf.Atan2(diff.y, diff.x) * Mathf.Rad2Deg;
            headTransform.rotation = Quaternion.Euler(0f, 0f, rot_z - 90);
            GetComponent<Rigidbody2D>().AddForce(feetTransform.up * speed);
        }
        if (gameObject.GetComponent<Rigidbody2D>().velocity.magnitude > 0.05f) {
            if (timer + (1.20 - Mathf.Pow(gameObject.GetComponent<Rigidbody2D>().velocity.magnitude, 0.1f)) <= Time.fixedTime) {
                if (walkingFrame >= spriteWalk.Length)
                    walkingFrame = 0;
                body.GetComponent<SpriteRenderer>().sprite = spriteWalk[walkingFrame];
                walkingFrame++;
                timer = Time.fixedTime;
            }
        }
    }

    public override IEnumerator playAttackAnimation(Skill skill, float attackTime) {
        int frame = 0;
        walkingFrame = 0;
        do {
            if (!isStunned()) {
                body.GetComponent<SpriteRenderer>().sprite = spriteAttack[frame];
                if (frame == 6)
                    skill.skillLogic(this, stats);
                frame++;
            }
            yield return new WaitForSeconds(attackTime / spriteAttack.Length);
        } while (frame < spriteAttack.Length);
        isAttacking = false;
    }
}
