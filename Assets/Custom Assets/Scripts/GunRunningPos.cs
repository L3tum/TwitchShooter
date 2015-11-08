using UnityEngine;
using System.Collections;
using UnityStandardAssets.Characters.FirstPerson;

public class GunRunningPos : MonoBehaviour {

	public RigidbodyFirstPersonController fpsController;
	public GameObject weaponAnchor;
	public Vector3 gunRunAngle;

	KeyCode runButton;

	// Use this for initialization
	void Start () {
	runButton = fpsController.movementSettings.RunKey;
	}
	
	// Update is called once per frame
	void Update () {
		if(gameObject.GetComponent<NetworkView>().isMine){
			if(Input.GetKey (runButton)){
				weaponAnchor.transform.localEulerAngles = gunRunAngle;
			}
			if(Input.GetKeyUp (runButton)){
				weaponAnchor.transform.localEulerAngles = Vector3.zero;
			}
		}

	}
}
