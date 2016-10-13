using UnityEngine;
using System.Collections;

public class TeleportProjectile : MonoBehaviour {

	private GameObject _player;
	void Start () {
		_player = GameObject.FindWithTag("GameController").GetComponent<GameController>().Player;
	}

	void Update () {

	}

	void OnTriggerEnter(Collider col)
	{
		if (col.gameObject.name == "Floor")
		{
			Vector3 location = transform.position;
			_player.transform.position = location;
			Destroy(gameObject);
		}
	}
}
