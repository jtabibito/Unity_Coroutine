using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class remove_dict_enumerate : enumerate {
	Dictionary<int, int> lock_section_dict;

	wait_for_seconds my_wait_for_seconds;

	int start;
	int length;
	string key;

	public remove_dict_enumerate(Dictionary<int, int> dict, int start, int length, string key) {
		lock_section_dict = dict;
		this.start = start;
		this.length = length;
		this.key = key;
	}
	
	public bool move_next() {
		Debug.Log($"{key} remove_dict_enumerate remove {start}:{lock_section_dict[start]}");
		lock_section_dict.Remove(start);
		my_wait_for_seconds = new wait_for_seconds(0.01f);
		return ++start < length;
	}

	public void reset() {

	}

	public object current {
		get {
			return my_wait_for_seconds;
		}
	}
}
