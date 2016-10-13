using UnityEngine;
using System.Collections;

public class TeleportBuff : Buff {


	void Start ()
	{
		Init();
	}

	public TeleportBuff()
	{
		_duration = 5.0f;

	}



	public override void Tick()
	{

	}


	public override void UseBuff(GameController _gameController)
	{
		_active = true;
		GameObject _projectile = Instantiate(Constants.Teleporation_Projectile, _gameController.Player.transform.position + (_gameController.Player.transform.forward * 5.0f), Quaternion.identity) as GameObject;
		_projectile.GetComponent<Rigidbody>().velocity = Camera.main.transform.forward * 100.0f;
	}
}
