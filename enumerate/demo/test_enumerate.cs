using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class test_enumerate : MonoBehaviour {
	public Dictionary<int, int> lock_section_dict = new Dictionary<int, int>();

	coroutine m_coroutine = new coroutine();

	// Use this for initialization
	void Start () {
		for (int i = 0; i < 500; ++i) {
			lock_section_dict.Add(i, i);
		}

		// m_coroutine.start(remove_dict_enumerate(0, 200, "200 remove coroutine"));
		// m_coroutine.start(remove_dict_enumerate(200, 500, "500 remove coroutine"));

		m_coroutine.start(new remove_dict_enumerate(lock_section_dict, 300, 500, "300 remove coroutine"));
		m_coroutine.start(new remove_dict_enumerate(lock_section_dict, 0, 200, "200 remove coroutine"), true);
		m_coroutine.start(new remove_dict_enumerate(lock_section_dict, 200, 300, "100 remove coroutine"));
	}
	
	// Update is called once per frame
	void Update () {
		m_coroutine.update();
	}

	object lock_obj = new object();

	IEnumerator remove_dict_enumerate(int start, int length, string key) {
		lock (lock_obj) {
			for (int i = start; i < length; ++i) {
				print($"{key} remove {i}:{lock_section_dict[i]}");
				lock_section_dict.Remove(i);
				yield return new wait_for_seconds(0.01f);
			}
			print($"{key} remove_dict_enumerate done");
		}
	}

}
