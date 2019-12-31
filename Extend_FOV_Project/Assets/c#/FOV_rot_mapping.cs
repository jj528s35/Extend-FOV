using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SpatialTracking;

public class FOV_rot_mapping : MonoBehaviour {

	public GameObject HMD, Body, Cam;
	public AnimationCurve curve;
	public bool remapping = true, Visualzation = false;
	public Image Body_Bar, Rot_mapping_Bar;
	private Vector3 Head_rotation, Body_rotation;
	private float Head_Body_rot_y, curve_mapping_value, angle;

	// Use this for initialization
	void Start () {
		//Disable Camera eye position and rotation tracking
		Cam.GetComponent<TrackedPoseDriver>().enabled = false;
	}
	
	// Update is called once per frame
	void Update () {
		//Assign position same as HMD
		transform.position = HMD.transform.position;

		//Get localRotation of HMD and tracker(Body)
		Head_rotation = HMD.transform.localRotation.eulerAngles;
		//Head_rotation = HMD.transform.rotation.eulerAngles;//there are rotation offset between HMD and Tracker
		Body_rotation = Body.transform.localRotation.eulerAngles;

		
		if(remapping)
		{
			/* Get the rotation y different of HMD and tracker
			And turn the angle to (-180,180)
			Then find the mapping value of curve*/
			Head_Body_rot_y = Head_rotation.y - Body_rotation.y;
			angle = Head_Body_rot_y % 360;
			angle= angle>180 ? angle-360 : angle;
			curve_mapping_value = curve.Evaluate(angle); //Head_Body_rot_y range??

			//Assign the rotation
			//Body rotation as Based , Add rotation of HMD (start from 1:1)
			//身體面相方向為基準，加上rotation mapping的值
			transform.localRotation = Quaternion.Euler(Head_rotation.x, curve_mapping_value + Body_rotation.y, Head_rotation.z);
		}
		else
		{
			transform.localRotation = Quaternion.Euler(Head_rotation.x, Head_rotation.y, Head_rotation.z);
		}
		

		if(Visualzation)
		{
			if(curve_mapping_value > 0)
			{
				Body_Bar.fillClockwise = true;
				Rot_mapping_Bar.fillClockwise = true;
			}
			else
			{
				Body_Bar.fillClockwise = false;
				Rot_mapping_Bar.fillClockwise = false;
			}
			
			Body_Bar.fillAmount = angle / 360;
			Rot_mapping_Bar.fillAmount = curve_mapping_value / 360;
		}


		//Debug 
		if(Input.GetKeyDown(KeyCode.Space)){
			Debug.Log("Mapping "+ angle + " , " + curve_mapping_value + " , " + Head_rotation.y);
		}


		
	}
}
