using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*測試用，沒有用到 */
public class FOV_pos_rot_mapping : MonoBehaviour {

	public GameObject Head, Body;
	public AnimationCurve curve;
	private Vector3 Head_rotation, Body_rotation;
	private float Head_Body_rot_y, curve_mapping_value, angle;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		//Assign position same as HMD
		transform.position = Head.transform.position;

		//Get localRotation of HMD and tracker(Body)
		Head_rotation = Head.transform.localRotation.eulerAngles;
		Body_rotation = Body.transform.localRotation.eulerAngles;

		//Get the rotation y different of HMD and tracker
		//And turn the angle to (-180,180)
		//Then find the mapping value of curve
		Head_Body_rot_y = Head_rotation.y - Body_rotation.y;
		angle = Head_Body_rot_y % 360;
		angle= angle>180 ? angle-360 : angle;
		curve_mapping_value = curve.Evaluate(angle); //Head_Body_rot_y range??

		//Assign the rotation
		transform.localRotation = Quaternion.Euler(Head_rotation.x, curve_mapping_value, Head_rotation.z);

		//Debug 
		if(Input.GetKeyDown(KeyCode.Space))
			Debug.Log("Mapping "+ angle + " , " + curve_mapping_value);
		if(Input.GetKeyDown(KeyCode.RightArrow))
			Head.transform.localRotation = Quaternion.Euler(Head_rotation.x, Head_rotation.y+5, Head_rotation.z);
		if(Input.GetKeyDown(KeyCode.LeftArrow))
			Head.transform.localRotation = Quaternion.Euler(Head_rotation.x, Head_rotation.y-5, Head_rotation.z);

	}
}
