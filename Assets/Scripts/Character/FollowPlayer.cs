using UnityEngine;

public class FollowPlayer : MonoBehaviour {
	public GameObject headBone;

	// Update is called once per frame
	void LateUpdate() {
		if (headBone == null) {
			Debug.Log("headBone GameObject not set in Camera Follower Script... why?!??!?");
			return;
		} else {
			float speed = 1.0f;
			if (Input.GetAxis("Vertical") > -0.1f) {
				transform.LookAt(headBone.transform);
			} else {
				speed = 10.0f;
			}
			gameObject.transform.position += (Vector3.back + headBone.transform.position - transform.position) * Time.deltaTime * speed;
		}
	}
}

