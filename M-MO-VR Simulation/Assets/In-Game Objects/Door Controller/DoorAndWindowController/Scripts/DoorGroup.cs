using UnityEngine;

namespace WaveCaveGames{
	
	public class DoorGroup : MonoBehaviour{
		//attach this component to a group of door so they can be controlled by FPSTrigger
		public void OpenTheDoors(){
			Door[] doors = GetComponentsInChildren<Door>();
			for (int i = 0; i < doors.Length; i++) doors[i].enabled = true;
		}
	}
}
