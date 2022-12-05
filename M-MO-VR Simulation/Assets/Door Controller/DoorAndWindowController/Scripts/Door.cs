using UnityEngine;

namespace WaveCaveGames{

	public class Door : MonoBehaviour
	{
		public float startValue;
		public float finishValue;
		[Tooltip("0 = Rotation X\n1 = Rotation Y\n2 = Rotation Z\n3 = Position X\n4 = Position Y\n5 = Position Z\nAll in local space.")]
		public int movingType;
		public bool locked;
		[Tooltip("Not Controlled By Player")]
		public bool notControlledByPlayer;
		[HideInInspector] public bool open;
		float t;

		void Awake(){
			if (movingType < 0 || movingType > 5) {
				locked = true;
				Debug.LogWarning("Door object '" + name + "' has invalid moving type, this door will be locked.");
			}
			//for custom scripting convenience, door should open in behaviour but not in the scene
			if (locked) open = true;
			//disable on awake. since it handles during the enabling action
			enabled = false;
		}
		void Update(){
			t += Time.deltaTime;
			Vector3 v = (movingType < 3) ? transform.localEulerAngles : transform.localPosition;
			v[movingType % 3] = Mathf.Lerp(open ? finishValue : startValue, open ? startValue : finishValue, t);
			if (movingType < 3) transform.localEulerAngles = v;
			else transform.localPosition = v;
			if (t > 1f) {open = !open; enabled = false; t = 0f;}
		}
	}
}
