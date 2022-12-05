using UnityEngine;

namespace WaveCaveGames{
	
	public class AutomaticDoor : MonoBehaviour{
		//attach this component to a group of door so they can be controlled by FPSTrigger
		public Door[] doors;
		[HideInInspector] public bool opening;
		private float t;

		void Update(){
			if (opening) t += Time.deltaTime;
			else t -= Time.deltaTime;
			if (t > 1f) t = 1f;
			if (t < 0f) t = 0f;
			for (int i = 0; i < doors.Length; i++) {
				Vector3 v = (doors[i].movingType < 3) ? doors[i].transform.localEulerAngles : doors[i].transform.localPosition;
				v[doors[i].movingType % 3] = Mathf.Lerp(doors[i].startValue, doors[i].finishValue, t);
				if (doors[i].movingType < 3) doors[i].transform.localEulerAngles = v;
				else doors[i].transform.localPosition = v;
				doors[i].open = t > 0.5f;
			}
		}
		private void OnTriggerEnter(Collider other){
			if (other.tag == "Player") opening = true;
		}
		private void OnTriggerExit(Collider other){
			if (other.tag == "Player") opening = false;
		}
	}
}
