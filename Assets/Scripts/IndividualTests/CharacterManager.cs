using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using FayvitMove;
using FayvitCommandReader;
using FayvitCam;

public class CharacterManager : MonoBehaviour {

	[SerializeField] private BasicMove mov;

	// Use this for initialization
	void Start () {
		mov.StartFields(transform);
	}

	ICommandReader CR
	{
		get { return CommandReader.GetCR(Controlador.teclado); } 
	}
	
	// Update is called once per frame
	void Update () {
		mov.MoveApplicator(
			CR.DirectionalVector(),
			CR.GetButton(CommandConverterInt.run),
			CR.GetButtonDown(CommandConverterInt.jump),
			CR.GetButton(CommandConverterInt.jump));

		CameraApplicator.cam.ValoresDeCamera(
			Input.GetAxis("Mouse X"), 
			Input.GetAxis("Mouse Y"), Input.GetKeyDown(KeyCode.Q), mov.Controller.velocity.magnitude > 0);
	}
}
