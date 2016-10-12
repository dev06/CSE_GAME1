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

	//Patrol Enemy

	public const float PatrolEnemyMaxHealth = 400.0f;
	public const float PatrolEnemyDamage = 10.0f;
	public const float PatrolEnemySpeed = 10.0f;


	//Guard Enemy
	public const float GuardEnemyMaxHealth = 500.0f;
	public const float GuardEnemyShootDelay = 0.75f;
	public const float GuardEnemySpeed = 7;


	#region---FRIENDLY---
	//Droid
	public const float DroidDistanceToAttack = 25.0f;
	public const float DroidMovementSpeed = 10.0f;
	public const float DroidMaxHealth = 50.0f;



	#endregion---/FRIENDLY---

	//Player/Camera members
	public const float PlayerMaxHealth = 100.0f;
	public const float SmallProjectileDamage = 10.0f;
	public const float CameraVerticalFOV = 60.0f;
	public const float CameraDistanceFromPlayer = 8.0f;
	public const float CameraRecoilAmount = 0.5f; // amount is being added to the camera distance from the player.

	public const float PlayerForwardAcc = .5f;
	public const float PlayerStrafeAcc = .45f;
	public const float PlayerRotationVerticalDelay = 8f; // uses lerp
	public const float PlayerRotationHorizontalDelay = .25f; // uses damp therefore big difference in values.





	//Projectile Members
	public const float DefaultPlayerMovementSpeed = 10.0f;
	public static float PlayerMovementSpeed = 10.0f;


	public const float Character_BlueProjectileDamage = 2.0f;
	public const float Character_YellowProjectileDamage = 5.0f;
	public const float Character_PurpleProjectileDamage = 15.0f;
	public const float GuardEnemy_ProjectileDamage = 2.0f;
	public const float Droid_ProjectileDamage = 4.0f;


	//HEALTH MECH
	public const float HealthRepletionTimer = 10.0f;  //units in seconds.
	public const float HealthRepletionPoints = 2.0f;  // Repletes x points every second.

	//UI
	public const float HideControlUITimer = 5.0f;


	//Entity
	public const float EntityRotationSpeed = 25f;
	public const float EntityHoverAmp = .05f;
	public const float EntityHoverFreq = .1f;

	public const float BasicHealthRepletion = 5f;
	public const float InterMedHealthRepletion = 10f;
	public const float AdvancedHealthRepletion = 15f;
	public const float SuperHealthRepletion = 75f;

	public const float SpeedBuffAmount = 25.0f;




	public static Material Character_Blue_Mat = (Material)Resources.Load("Materials/Entity/Character/character_blue_mat/character_blue_mat");
	public static Material Character_Purple_Mat = (Material)Resources.Load("Materials/Entity/Character/character_purple_mat/character_purple_mat");
	public static Material Character_Yellow_Mat = (Material)Resources.Load("Materials/Entity/Character/character_yellow_mat/character_yellow_mat");


	public static Material Character_Blue_Hover_Mat = (Material)Resources.Load("Materials/Particles/Hover/blue_hover");
	public static Material Character_Yellow_Hover_Mat = (Material)Resources.Load("Materials/Particles/Hover/yellow_hover");
	public static Material Character_Purple_Hover_Mat = (Material)Resources.Load("Materials/Particles/Hover/purple_hover");

	public static Material Character_Blue_Shoot_Mat = (Material)Resources.Load("Materials/Entity/EntityParticles/shoot_particles/blue_effect_shoot");
	public static Material Character_Yellow_Shoot_Mat = (Material)Resources.Load("Materials/Entity/EntityParticles/shoot_particles/yellow_effect_shoot");
	public static Material Character_Purple_Shoot_Mat = (Material)Resources.Load("Materials/Entity/EntityParticles/shoot_particles/purple_effect_shoot");

	public static GameObject Blue_Bullet = (GameObject)Resources.Load("Prefabs/Projectile/Player/BlueBullet");
	public static GameObject Yellow_Bullet = (GameObject)Resources.Load("Prefabs/Projectile/Player/YellowBullet");
	public static GameObject Purple_Bullet = (GameObject)Resources.Load("Prefabs/Projectile/Player/PurpleBullet");


	public static GameObject Droid_Bullet = (GameObject)Resources.Load("Prefabs/Projectile/Friendly/Droid/Droid_Bullet");


	public static GameObject Enemy_Two_Bullet = (GameObject)Resources.Load("Prefabs/Projectile/Enemy/Enemy_Two/Enemy_Two_Bullet");




	public static KeyCode[] QuickItemKeys = new KeyCode[4] {KeyCode.Alpha1, KeyCode.Alpha2, KeyCode.Alpha3, KeyCode.Alpha4};
}
