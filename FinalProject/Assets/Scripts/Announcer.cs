using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public delegate void AnnoucerDelegate();

public class Announcer {
	List<AnnoucerDelegate> audience;
	bool announced;

	public Announcer(){
		audience = new List<AnnoucerDelegate> ();
		announced = false;
	}

	public void Register(AnnoucerDelegate del){
		if (announced) {
			del ();
		} else {
			audience.Add (del);
		}
	}

	public void Announce(){
		announced = true;
		foreach (AnnoucerDelegate del in audience) {
			del ();
		}
	}
}
