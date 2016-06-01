using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

public class Boss : Mob {

    private int speed = 110, followDistance = 8;
    private Vector3 playerPosition;
    private float distance;
    private RaycastHit2D lineOfSight;

    public Sprite[] spriteAttack, spriteWalk, spriteIdle;
    private int walkingFrame;
    private int idleFrame;
    private bool halfHealth;

    private Dictionary<int, int> skillPriorities = new Dictionary<int, int>();

    private Vector3 overrideTarget = Vector3.zero;

    public override string getName() {
        return "Boss";
    }

    void create() {
        body = new GameObject("Boss");
        body.transform.SetParent(transform);
        body.transform.localScale = Vector3.one;
        body.transform.position = transform.position;
        body.AddComponent<SpriteRenderer>();
        body.GetComponent<SpriteRenderer>().material = (Material)Resources.Load("MapMaterial");
        body.GetComponent<SpriteRenderer>().sortingOrder = 2;
        stats.baseHealth = 50000;
        stats.baseStrength = 20;
        // Z exp = 100
        stats.exp = 100;
    }

    class GemDoubleAoe : Gem {
        public GemDoubleAoe() {
            properties.Add("areaOfEffect", new property(2f, "*"));
            this.itemDescription = "Double AOE\n*Only to be used by Boss*";
        }
    }

    // Use this for initialization
    public override void OnStart() {
        setDropRate(100);
        setArmourDrops(new int[] {7});
        setSkillDrops(new int[] {4,7});
        create();
        transform.rotation = new Quaternion(0, 0, 0, 0);
        spriteAttack = Resources.LoadAll<Sprite>("Sprite/zombieAttack");
        spriteWalk = Resources.LoadAll<Sprite>("Sprite/zombieWalk");
        spriteIdle = Resources.LoadAll<Sprite>("Sprite/zombieIdle");
        halfHealth = false;
        replaceSkill(0, new SkillFireBolt());
        skills[0].properties["manaCost"] = 0;
        skills[0].properties["projectileCount"] = 3;
        skills[0].properties["attackSpeed"] = 0.75f;
        skills[0].properties["cooldown"] = 1;
        replaceSkill(1, new SkillMortar());
        skills[1].properties["manaCost"] = 0;
        skills[1].properties["cooldown"] = 1.5f;
        replaceSkill(2, new SkillRighteousFire());
        skills[2].addGem(new GemDoubleAoe());
        skills[2].properties["manaCost"] = 0;
        replaceSkill(3, new SkillCombatRoll());
        skills[3].properties["manaCost"] = 0;
        skills[3].properties["cooldown"] = 1.5f;
        replaceSkill(4, new BossRecklessShot());
        skills[4].properties["manaCost"] = 0;
        skills[4].properties["cooldown"] = 10f;

        skillPriorities.Add(0, 100); //FireBolt
        skillPriorities.Add(1, 10); //Mortar
        skillPriorities.Add(3, 2); //CombatRoll
        skillPriorities.Add(4, 1); //RecklessShot
        skillPriorities.Add(5, 75); //DoNothing

        lineOfSight = Physics2D.Raycast(body.transform.position + (playerPosition - body.transform.position).normalized * GetComponent<CircleCollider2D>().radius * transform.localScale.x * 1.1f, playerPosition - body.transform.position);
    }

    // Update is called once per frame
    public override void OnUpdate() {
        if (isAttacking)
            return;
        if (stats.health < stats.baseHealth / 2 && !halfHealth) {
            skills[0].properties["attackSpeed"] = 0.56f;
            skills[0].properties["cooldown"] = 0.75f;
            skills[2].useSkill(this);
            halfHealth = true;
        }
        GameObject player = GameObject.FindWithTag("Player");
        if (player) {
            playerPosition = player.transform.position;
            Vector3 diff = (getTargetLocation() - feetTransform.position).normalized;
            float rot_z = Mathf.Atan2(diff.y, diff.x) * Mathf.Rad2Deg;
            headTransform.rotation = Quaternion.Euler(0f, 0f, rot_z - 90);

            distance = Mathf.Sqrt(Mathf.Pow(playerPosition.x - transform.position.x, 2) + Mathf.Pow(playerPosition.y - transform.position.y, 2));
            lineOfSight = Physics2D.Raycast(body.transform.position + (playerPosition - body.transform.position).normalized * GetComponent<CircleCollider2D>().radius * transform.localScale.x * 1.1f, playerPosition - body.transform.position);

            List<int> availableSkills = new List<int>();
            if (lineOfSight.collider.CompareTag("Player")) {
                if (distance <= 9 && skills[0].remainingCooldown() <= 0)
                    availableSkills.Add(0);
                if (distance >= 6 && distance <= 15 && skills[1].remainingCooldown() <= 0)
                    availableSkills.Add(1);
                if (stats.health < stats.baseHealth / 2) {
                    if (distance <= 13 && skills[4].remainingCooldown() <= 0)
                        availableSkills.Add(4);
                }
            }
            if (halfHealth) {
                if (distance >= 3 && skills[3].remainingCooldown() <= 0)
                    availableSkills.Add(3);
            }

            int num = Random.Range(0, availableSkills.Sum(x => skillPriorities[x]));
            int total = 0;
            foreach (int x in availableSkills) {
                if (num < skillPriorities[x] + total) {
                    if (x == 1) {
                        Vector3 velocity = player.GetComponent<Rigidbody2D>().velocity;
                        Vector3 target = player.transform.position + (velocity * Random.Range(0.5f, 0.8f));
                        overrideTarget = target;
                    }
                    skills[x].useSkill(this);
                    break;
                }
                total += skillPriorities[x];
            }
        }
    }

    public override void OnFixedUpdate() {
        foreach (Skill skill in skills) {
            if (skill != null)
                skill.skillFixedUpdate(this);
        }
    }

    public override Vector3 getTargetLocation() {
        if (overrideTarget == Vector3.zero) {
            GameObject player = GameObject.FindWithTag("Player");
            float radius = GetComponent<CircleCollider2D>().radius * transform.localScale.x * 1.1f;
            //Debug.DrawLine(body.transform.position, body.transform.position + (player.transform.position - body.transform.position).normalized * 2, Color.red);
            foreach (RaycastHit2D lineCast in Physics2D.LinecastAll(body.transform.position + (player.transform.position - body.transform.position).normalized * radius, body.transform.position + (player.transform.position - body.transform.position).normalized * 2)) {
                if (lineCast.collider.CompareTag("Wall") || lineCast.collider.CompareTag("Enemy")) {
                    //Debug.DrawLine(body.transform.position, (body.transform.position + body.transform.up * 2), Color.blue);
                    foreach (RaycastHit2D directionCast in Physics2D.LinecastAll(body.transform.position + body.transform.up * radius, (body.transform.position + body.transform.up * 2))) {
                        if (directionCast.collider.CompareTag("Wall") || directionCast.collider.CompareTag("Enemy")) {
                            //Debug.DrawLine(body.transform.position, Physics2D.Raycast(body.transform.position + body.transform.TransformDirection(new Vector3(0.5f, 0.5f, 0)).normalized * radius, body.transform.TransformDirection(new Vector3(0.5f, 0.5f, 0))).point, Color.green);
                            //Debug.DrawLine(body.transform.position, Physics2D.Raycast(body.transform.position + body.transform.TransformDirection(new Vector3(-0.5f, 0.5f, 0)).normalized * radius, body.transform.TransformDirection(new Vector3(-0.5f, 0.5f, 0))).point, Color.green);
                            if (Physics2D.Raycast(body.transform.position + body.transform.TransformDirection(new Vector3(0.5f, 0.5f, 0)).normalized * radius, body.transform.TransformDirection(new Vector3(0.5f, 0.5f, 0))).distance > Physics2D.Raycast(body.transform.position + body.transform.TransformDirection(new Vector3(-0.5f, 0.5f, 0)).normalized * radius, body.transform.TransformDirection(new Vector3(-0.5f, 0.5f, 0))).distance)
                                return body.transform.position + body.transform.TransformDirection(new Vector3(0.1f, 1, 0).normalized);
                            else
                                return body.transform.position + body.transform.TransformDirection(new Vector3(-0.1f, 1, 0).normalized);
                        }
                    }
                    return body.transform.position + body.transform.up;
                }
            }
            return player.transform.position;
        }
        else {
            return overrideTarget;
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
        overrideTarget = Vector3.zero;
        GameObject player = GameObject.FindWithTag("Player");
        if (player) {
            playerPosition = player.transform.position;
            Vector3 diff = (getTargetLocation() - feetTransform.position).normalized;
            float rot_z = Mathf.Atan2(diff.y, diff.x) * Mathf.Rad2Deg;
            headTransform.rotation = Quaternion.Euler(0f, 0f, rot_z - 90);
            if (halfHealth) {
                if (distance >= 6 || !lineOfSight.collider.CompareTag("Player"))
                    GetComponent<Rigidbody2D>().AddForce(feetTransform.up * speed);
            }
            else {
                if (distance >= 2 || !lineOfSight.collider.CompareTag("Player"))
                    GetComponent<Rigidbody2D>().AddForce(feetTransform.up * speed * 1.4f);
            }
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
