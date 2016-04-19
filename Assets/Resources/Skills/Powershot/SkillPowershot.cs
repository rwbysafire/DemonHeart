using UnityEngine;
using System.Collections;

public class SkillPowershot : Skill
{
	int maxDistance = 20;

	public SkillPowershot() : base() { }

	public override string getName () {
		return "Powershot";
	}
	
	public override Sprite getImage () {
		return Resources.Load<Sprite>("Skills/Powershot/powershot");
	}
	
	public override float getAttackSpeed () {
		return 1.5f;
	}
	
	public override float getMaxCooldown () {
		return 1.5f;
	}
	
	public override float getManaCost () {
		return 20;
	}
	
	public override void skillLogic(Mob mob) {
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
        powershot.transform.localScale = new Vector2(1f, Vector2.Distance(startLocation, targetLocation));
        BoxCollider2D collider = powershot.AddComponent<BoxCollider2D>();
		collider.isTrigger = true;
        collider.size = new Vector2(0.5f, 1f);
        PowershotEffect p = powershot.AddComponent<PowershotEffect>();
		p.mob = mob;
		AudioSource.PlayClipAtPoint(Resources.Load<AudioClip>("Skills/Powershot/sniperShot"), mob.headTransform.position);
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
		alpha -= 0.03f;
		if (alpha <= 0)
			Destroy(gameObject);
		else if (alpha <= 0.5f)
			gameObject.GetComponent<BoxCollider2D>().enabled = false;
	}

	void OnTriggerEnter2D(Collider2D collider) {
		if (collider.CompareTag(enemyTag))
			collider.GetComponent<Mob>().hurt ((2 * mob.stats.basicAttackDamage) + (0.2f * mob.stats.attackDamage));
	}
}