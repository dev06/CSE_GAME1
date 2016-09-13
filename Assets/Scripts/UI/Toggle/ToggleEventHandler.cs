//Devan Patel
//Applications and Scripting
//Sep.12.2016
using UnityEngine;
using System.Collections;
using UnityEngine.EventSystems;
using UnityEngine.UI;
public class ToggleEventHandler : MonoBehaviour, IPointerClickHandler {

	#region---- PUBLIC MEMBERS----
	public bool isOn;
	public Sprite ActiveSprite;
	public Sprite RestSprite;
	public Color ActiveColor;
	public Color RestColor;
	#endregion---- /PUBLIC MEMBERS----

	#region---- PROTECTED MEMBERS----
	protected Image _image;
	protected Image _outLineImage;
	protected GameController _gameController;
	#endregion---- /PROTECTED MEMBERS----


	void OnEnable()
	{
		Init();
		_image.sprite = (isOn) ? ActiveSprite : RestSprite;
		_image.color = (isOn) ? ActiveColor : RestColor;
	}

	void Start ()
	{
		Init();
	}

	/// <summary>
	/// Init all the components
	/// </summary>
	protected void Init()
	{
		_image = transform.GetChild(1).GetComponent<Image>();
		_outLineImage = GetComponent<Image>();
		_gameController = GameObject.FindGameObjectWithTag("GameController").GetComponent<GameController>();
	}

	/// <summary>
	/// Overrides the on pointer click from the base class
	/// </summary>
	/// <param name="data"></param>
	public virtual void OnPointerClick(PointerEventData data)
	{
		isOn = !isOn;
		_gameController.ToggleMouseControl = isOn;
		_image.sprite = (isOn) ? ActiveSprite : RestSprite;
		_image.color = (isOn) ? ActiveColor : RestColor;
	}
}
