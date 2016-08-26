using UnityEngine;
using System.Collections;

public class ControllerConfiguration : MonoBehaviour {

	Animation _animation;

	void OnEnable()
	{
		_animation = GetComponent<Animation>();
		Debug.Log(_animation.clip.name);
		_animation.Play(_animation.clip.name);

	}

	void Start () {
	}

	// Update is called once per frame
	void Update () {

	}
}
