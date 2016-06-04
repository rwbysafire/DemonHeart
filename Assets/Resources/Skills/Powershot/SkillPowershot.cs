using UnityEngine;
using System.Collections;

public class SkillPowershot : Skill
{
	int maxDistance = 20;
    AoeSkill aoeSkill = new AoeSkill();

    public SkillPowershot() : base() {
        addSkillType(aoeSkill);
    }

	public override string getName () {
		return "Powershot";
	}
	
	public override Sprite getImage () {
		return Resources.Load<Sprite>("Skills/Powershot/powershot");
	}
	
	public override float getAttackSpeed () {
		return 1.25f;
	}
	
	public override float getMaxCooldown () {
		return 1f;
	}
	
	public override float getManaCost () {
		return 15;
	}
	
	public override void skillLogic(Entity mob, Stats stats) {
		Vector2 targetLocation = mob.headTransform.up * maxDistance + mob.headTransform.position;
		Vector2 startLocation = mob.headTransform.position + mob.headTransform.up;
		foreach (RaycastHit2D linecast in Physics2D.LinecastAll(startLocation, targetLocation)) {
			if (linecast.collider.CompareTag("Wall")) {
				targetLocation = linecast.point;
				break;
			}
		}
		GameObject powershot = new GameObject();
        SpriteRenderer spriteRenderer = powershot.AddComponent<SpriteRenderer>();
        spriteRenderer.sprite = Resources.Load<Sprite>("Skills/Powershot/PowershotLaser");
        spriteRenderer.sortingOrder = 4;
        powershot.transform.position = new Vector2(startLocation.x + (targetLocation.x - startLocation.x)/2, startLocation.y + (targetLocation.y - startLocation.y)/2);
		powershot.transform.rotation = mob.headTransform.rotation;
        powershot.transform.localScale = new Vector3(1f * properties["areaOfEffect"], Vector2.Distance(startLocation, targetLocation), 1f);
        BoxCollider2D collider = powershot.AddComponent<BoxCollider2D>();
		collider.isTrigger = true;
        collider.size = new Vector2(0.5f, 1f);
        PowershotEffect p = powershot.AddComponent<PowershotEffect>();
		p.mob = mob.gameObject.GetComponent<Mob>();
		AudioSource.PlayClipAtPoint(Resources.Load<AudioClip>("Skills/Powershot/sniperShot"), mob.headTransform.position);
        for (int i = 0; i < Vector3.Distance(startLocation, targetLocation); i++) {
            GameObject arrowGlow = MonoBehaviour.Instantiate(Resources.Load("ShotGlow")) as GameObject;
            arrowGlow.GetComponent<Light>().intensity = 3;
            arrowGlow.transform.SetParent(powershot.transform);
            Vector3 position = startLocation + ((targetLocation - startLocation).normalized * i);
            arrowGlow.transform.position = new Vector3(position.x, position.y, -2);
        }
    }
}

class PowershotEffect : MonoBehaviour {
	
	float alpha = 0.8f;
	Color c = Color.white;
	string enemyTag;
	public Mob mob;

	void Start() {
		if (mob.gameObject.tag == "Player" || mob.gameObject.tag == "Ally")
			enemyTag = "Enemy"; 
		else
			enemyTag = "Player";
	}

	void FixedUpdate () {
		c.a = alpha;
		gameObject.GetComponent<SpriteRenderer>().color = c;
        foreach (Transform child in transform) {
            child.GetComponent<Light>().intensity -= 0.1f;
        }
		alpha -= 0.03f;
		if (alpha <= 0)
			Destroy(gameObject);
		else if (alpha <= 0.5f)
			gameObject.GetComponent<BoxCollider2D>().enabled = false;
	}

	void OnTriggerEnter2D(Collider2D collider) {
		if (collider.CompareTag(enemyTag))
			collider.GetComponent<Mob>().hurt ((2.5f * mob.stats.basicAttackDamage) + (0.4f * mob.stats.attackDamage));
	}
}