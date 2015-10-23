using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class Skill_BasicAttack : MonoBehaviour {
	
	public KeyCode keyBind;
	public float speed, cooldown, damage;
    float remainingCD;
    Slider slider;
    Text text;

    void Start()
    {
        slider = GetComponent<Slider>();
        text = GetComponentInChildren<Text>();
    }

    // Update is called once per frame
    void Update ()
	{
        if (Input.GetKey(keyBind) && remainingCD <= 0 && GameObject.FindWithTag("Player"))
		{
			GameObject player = GameObject.FindWithTag("Player");
            fire(player);
            remainingCD = cooldown;
        }
        remainingCD -= Time.deltaTime;

        // Updates the skill's cooldown on the HUD
        slider.maxValue = cooldown;
        slider.value = remainingCD;
        if (remainingCD >= 0)
            text.text = ((int)remainingCD + 1).ToString();
        else
            text.text = "";


    }

    void fire(GameObject origin)
    {
        //Instantiates the projectile with some speed
        GameObject basicArrow = Instantiate(Resources.Load("Arrow_Placeholder")) as GameObject;
        basicArrow.GetComponent<basic_projectile>().speed = speed;
		basicArrow.GetComponent<basic_projectile>().damage = damage;
        //Initiates the projectile's position and rotation
		basicArrow.transform.position = origin.transform.position;
		basicArrow.transform.rotation = origin.transform.rotation;
        basicArrow.transform.Translate(Vector3.up * 0.7f);
    }
}
