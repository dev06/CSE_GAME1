using UnityEngine;
using System.Collections;

public class Constants  {

	//Control Members
	public const string FORWARD = "Vertical";
	public const string STRAFE = "Horizontal";
	public const string X_LOOK = "Mouse X";
	public const string Y_LOOK = "Mouse Y";

	//Game mech
	public const int StartBotSpawningDelay = 0; //bots will spawn after x seconds (starting)
	public const int BotSpawnDelay = 2; //spawn bots every x secs
	public const int MaxBotAtTime = 1;


	//Enemy Members
	public const float BotMaxHealth = 200.0f;
	public const float BotMovementSpeed = 4.5f;
	public const float BotInitalDamage = 5.0f;

	//Player/Camera members
	public const float PlayerMaxHealth = 100.0f;
	public const float SmallProjectileDamage = 10.0f;
	public const float CameraVerticalFOV = 60.0f;
	public const float PlayerForwardAcc = .5f;
	public const float PlayerStrafeAcc = .45f;
	public const float PlayerRotationVerticalDelay = 8f; // uses lerp
	public const float PlayerRotationHorizontalDelay = .25f; // uses damp therefore big difference in values.



	//Projectile Members
	public const float PlayerMovementSpeed = 10.0f;
	public const float PlayerSprintSpeed = 8.0f;
	public const float LargeProjectileDamage = 20.0f;

	//HEALTH MECH
	public const float HealthRepletionTimer = 10.0f;  //units in seconds.
	public const float HealthRepletionPoints = 2.0f;  // Repletes x points every second.

	//UI
	public const float HideControlUITimer = 5.0f;


	//Entity
	public const float EntityRotationSpeed = 25f;
	public const float EntityHoverAmp = .05f;
	public const float EntityHoverFreq = .1f;


	public static Material Character_Blue_Mat = (Material)Resources.Load("Materials/Entity/Character/character_blue_mat/character_blue_mat");
	public static Material Character_Purple_Mat = (Material)Resources.Load("Materials/Entity/Character/character_purple_mat/character_purple_mat");
	public static Material Character_Yellow_Mat = (Material)Resources.Load("Materials/Entity/Character/character_yellow_mat/character_yellow_mat");


	public static Material Character_Blue_Hover_Mat = (Material)Resources.Load("Materials/Particles/Hover/blue_hover");
	public static Material Character_Yellow_Hover_Mat = (Material)Resources.Load("Materials/Particles/Hover/yellow_hover");
	public static Material Character_Purple_Hover_Mat = (Material)Resources.Load("Materials/Particles/Hover/purple_hover");

	public static Material Character_Blue_Shoot_Mat = (Material)Resources.Load("Materials/Entity/EntityParticles/shoot_particles/blue_effect_shoot");
	public static Material Character_Yellow_Shoot_Mat = (Material)Resources.Load("Materials/Entity/EntityParticles/shoot_particles/yellow_effect_shoot");
	public static Material Character_Purple_Shoot_Mat = (Material)Resources.Load("Materials/Entity/EntityParticles/shoot_particles/purple_effect_shoot");


	public static KeyCode[] QuickItemKeys = new KeyCode[4] {KeyCode.Alpha1, KeyCode.Alpha2, KeyCode.Alpha3, KeyCode.Alpha4};
}
