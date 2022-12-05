using UnityEngine;
using WaveCaveGames;

namespace WaveCaveGames{

	public class FPSTrigger : MonoBehaviour {

		public KeyCode openDoorKey = KeyCode.Space;
		private Door door;

		void Update () {
			//door
			if (Input.GetKeyDown(openDoorKey) && door != null) {
				if (!door.locked && !door.notControlledByPlayer) {
					if (door.GetComponentInParent<DoorGroup>() != null) door.GetComponentInParent<DoorGroup>().OpenTheDoors();
					else door.enabled = true;
				}
			}
		}
		private void OnTriggerStay(Collider other){
			if (door == null || other.GetComponent<Door>() != null && !other.GetComponent<Door>().notControlledByPlayer) {
				door = other.GetComponent<Door>();
			}
		}
		private void OnTriggerExit(Collider other){
			door = null;
		}
	}
}
