using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class wait_for_seconds : enumerate {
	private float current_time;

	public wait_for_seconds(float time) {
		current_time = time;
	}

	public bool move_next() {
		return (current_time -= Time.deltaTime) > 0;
	}

	public void reset() {

	}

	public object current {
		get {
			return current_time;
		}
	}
}