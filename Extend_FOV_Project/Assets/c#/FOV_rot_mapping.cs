using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FOV_rot_mapping : MonoBehaviour {

	public GameObject HMD, Body, Cam;
	public AnimationCurve curve;
	private Vector3 Head_rotation, Body_rotation;
	private float Head_Body_rot_y, curve_mapping_value, angle;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
		/*transform.position = Head.transform.position;
		Cam.transform.localPosition = Head.transform.position;*/

		//Get localRotation of HMD and tracker(Body)
		Head_rotation = HMD.transform.localRotation.eulerAngles;
		Body_rotation = Body.transform.localRotation.eulerAngles;

		//Get the rotation y different of HMD and tracker
		//And turn the angle to (-180,180)
		//Then find the mapping value of curve
		Head_Body_rot_y = Head_rotation.y - Body_rotation.y;
		angle = Head_Body_rot_y % 360;
		angle= angle>180 ? angle-360 : angle;
		curve_mapping_value = curve.Evaluate(angle); //Head_Body_rot_y range??

		//Assign the rotation
		transform.localRotation = Quaternion.Euler(0, curve_mapping_value - Head_rotation.y, 0);
		//transform.localRotation = Quaternion.Euler(0, curve_mapping_value, 0);


		//Debug 
		if(Input.GetKeyDown(KeyCode.Space)){
			Debug.Log("Mapping "+ angle + " , " + curve_mapping_value + " , " + Head_rotation.y);
			/*Other_Head.transform.position = HMD.transform.position;
			Cam.transform.parent = Other_Head.transform;*/
		}
			

		if(Input.GetKeyDown(KeyCode.RightArrow))
			HMD.transform.localRotation = Quaternion.Euler(Head_rotation.x, Head_rotation.y+5, Head_rotation.z);
		if(Input.GetKeyDown(KeyCode.LeftArrow))
			HMD.transform.localRotation = Quaternion.Euler(Head_rotation.x, Head_rotation.y-5, Head_rotation.z);
	}
}
