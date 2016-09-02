using UnityEngine;
using System.Collections;

public class Constants  {

	//Control Members
	public const string FORWARD = "Vertical";
	public const string STRAFE = "Horizontal";
	public const string X_LOOK = "Mouse X";
	public const string Y_LOOK = "Mouse Y";

	//Enemy Members
	public const float BotMaxHealth = 500.0f;
	public const float BotMovementSpeed = 5.0f;
	public const float BotInitalDamage = 5.0f;

	//Player/Camera members
	public const float PlayerMaxHealth = 100.0f;
	public const float SmallProjectileDamage = 10.0f;
	public const float CameraVerticalFOV = 60.0f;

	//Projectile Members
	public const float PlayerMovementSpeed = 5.0f;
	public const float LargeProjectileDamage = 20.0f;

	//HEALTH MECH
	public const float HealthRepletionTimer = 10.0f;  //units in seconds.
	public const float HealthRepletionPoints = 2.0f;  // Repletes x points every second.


	//UI
	public const float HideControlUITimer = 2.0f;

}
