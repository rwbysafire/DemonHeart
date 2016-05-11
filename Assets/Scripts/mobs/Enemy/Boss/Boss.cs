using UnityEngine;
using System.Collections;

public class Boss : Mob {

    private int speed = 110, followDistance = 8;
    private Vector3 playerPosition;

    public Sprite[] spriteAttack, spriteWalk, spriteIdle;
    private int walkingFrame;
    private int idleFrame;

    public override string getName() {
        return "Boss";
    }

    void create() {
        body = new GameObject("Boss");
        body.transform.SetParent(transform);
        body.transform.position = transform.position;
        body.AddComponent<SpriteRenderer>();
        body.GetComponent<SpriteRenderer>().sortingOrder = 2;
        stats.baseHealth = 100000;
        stats.baseStrength = 20;
        // Z exp = 100
        stats.exp = 100;
    }

    // Use this for initialization
    public override void OnStart() {
        create();
        transform.rotation = new Quaternion(0, 0, 0, 0);
        spriteAttack = Resources.LoadAll<Sprite>("Sprite/zombieAttack");
        spriteWalk = Resources.LoadAll<Sprite>("Sprite/zombieWalk");
        spriteIdle = Resources.LoadAll<Sprite>("Sprite/zombieIdle");
        replaceSkill(0, new SkillBasicAttack());
        skills[0].properties["projectileCount"] = 3;
        replaceSkill(2, new SkillRighteousFire());
        skills[2].properties["manaCost"] = 0;
    }

    // Update is called once per frame
    public override void OnUpdate() {
        if (isAttacking)
            return;
        if (stats.health < stats.baseHealth / 2) {
            skills[2].useSkill(this);
        }
        if (GameObject.FindWithTag("Player")) {
            float distance = Mathf.Sqrt(Mathf.Pow(playerPosition.x - transform.position.x, 2) + Mathf.Pow(playerPosition.y - transform.position.y, 2));
            RaycastHit2D hitPlayer = Physics2D.Raycast(body.transform.position + (playerPosition - body.transform.position).normalized * GetComponent<CircleCollider2D>().radius * 1.1f, playerPosition - body.transform.position);
            if (distance >= 4 && distance <= 9 && hitPlayer.collider.CompareTag("Player")) {
                skills[0].useSkill(this);
            }
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
        GameObject player = GameObject.FindWithTag("Player");
        if (player) {
            playerPosition = player.transform.position;
            Vector3 diff = (getTargetLocation() - feetTransform.position).normalized;
            float rot_z = Mathf.Atan2(diff.y, diff.x) * Mathf.Rad2Deg;
            headTransform.rotation = Quaternion.Euler(0f, 0f, rot_z - 90);
            if (stats.health > stats.baseHealth/2)
                GetComponent<Rigidbody2D>().AddForce(feetTransform.up * speed);
            else
                GetComponent<Rigidbody2D>().AddForce(feetTransform.up * speed * 1.4f);
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
        walkingFrame = 0;
        bool hasntAttacked = true;
        float endTime = Time.fixedTime + attackTime;
        float remainingTime = endTime - Time.fixedTime;
        while (remainingTime > 0) {
            int currentFrame = (int)(((attackTime - remainingTime) / attackTime) * spriteAttack.Length);
            body.GetComponent<SpriteRenderer>().sprite = spriteAttack[currentFrame];
            if (hasntAttacked && currentFrame > 3) {
                hasntAttacked = false;
                skill.skillLogic(this, stats);
            }
            yield return new WaitForSeconds(0);
            remainingTime = endTime - Time.fixedTime;
        }
        body.GetComponent<SpriteRenderer>().sprite = spriteAttack[0];
        if (hasntAttacked)
            skill.skillLogic(this, stats);
        isAttacking = false;
    }
}
