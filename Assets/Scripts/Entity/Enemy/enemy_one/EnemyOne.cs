using UnityEngine;
using System.Collections;

public class EnemyOne : Mob {

	private GameObject _hover;
	public GameObject Target;
	void Start ()
	{
		Init();
		MaxHealth = Constants.BotMaxHealth;
		Health = MaxHealth;
		_hover = transform.FindChild("HoverEffect").gameObject;
	}

	// Update is called once per frame
	void Update ()
	{
		ManageHoverEffect();
		_agent.SetDestination(Target.transform.position);
	}

	private void ManageHoverEffect()
	{
		_hover.transform.Rotate(new Vector3(0, 0,  Time.deltaTime * 150.0f));
	}
}
