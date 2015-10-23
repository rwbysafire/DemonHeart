using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class Skill_BasicAttack : MonoBehaviour {

    public float speed;
    public float cooldown;
    float remainingCD;
    GameObject player;
    Slider slider;
    Text text;

    void Start()
    {
        slider = GetComponent<Slider>();
        text = GetComponentInChildren<Text>();
        player = GameObject.FindWithTag("Player");
    }

    // Update is called once per frame
    void Update ()
    {
        if (Input.GetKey(KeyCode.Mouse1) && remainingCD <= 0 && GameObject.FindWithTag("Player"))
        {
            fire();
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
    void fire()
    {
        //Instantiates the projectile with some speed
        GameObject basicArrow = Instantiate(Resources.Load("Arrow_Placeholder")) as GameObject;
        basicArrow.GetComponent<basic_projectile>().speed = speed;

        //Initiates the projectile's position and rotation
        GameObject player = GameObject.FindWithTag("Player");
        basicArrow.transform.position = player.transform.position;
        basicArrow.transform.rotation = player.transform.rotation;
        basicArrow.transform.Translate(Vector3.up * 0.7f);
    }
}
