using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class coroutine_unit {
    private Stack<enumerate> coroutine_stack = new Stack<enumerate>();

	public coroutine_unit(bool wait=false) {
		this.wait = wait;
    }

	public coroutine_unit(enumerate enumerator, bool wait=false) : this(wait) {
		coroutine_stack.Push(enumerator);
	}

	public coroutine_unit(coroutine_buffer buffer) : this(buffer.enumerator, buffer.wait) { }

	public bool wait { get; set; }

	public bool move_next() {
		if (coroutine_stack.Count == 0)
			return false;

		var enumerate = coroutine_stack.Peek();
		if (enumerate.move_next()) {
			object result = enumerate.current;
			if (result != null && result is enumerate) {
				coroutine_stack.Push((enumerate)result);
			}
			return true;
		} else {
			if (coroutine_stack.Count > 1) {
				coroutine_stack.Pop();
				return true;
			}
		}
		return false;
	}

	public void reset() {
		coroutine_stack.Clear();
	}

	public bool find(enumerate enumerator) {
		return coroutine_stack.Contains(enumerator);
	}
}

public class coroutine_buffer {
	public enumerate enumerator;
	public bool wait;

	public coroutine_buffer(enumerate enumerator, bool wait) {
		this.enumerator = enumerator;
		this.wait = wait;
    }
}

public class coroutine {
	private List<coroutine_unit> coroutine_list = new List<coroutine_unit>();
	private List<coroutine_buffer> coroutine_buffer = new List<coroutine_buffer>();
	private List<coroutine_unit> remove_buffer = new List<coroutine_unit>();

	public coroutine() {

	}

	public enumerate start(enumerate enumerator, bool wait=false) {
		coroutine_buffer.Add(new coroutine_buffer(enumerator, wait));
		return enumerator;
	}

	public bool find(enumerate enumerator) {
		foreach (var unit in coroutine_list) {
			if (unit.find(enumerator)) {
				return true;
			}
		}
		return false;
	}

	public void update() {
		// coroutine_list.RemoveAll(enumerator => !enumerator.move_next());
		foreach (var unit in coroutine_list) {
			if (!unit.move_next()) {
				remove_buffer.Add(unit);
			}
			if (unit.wait) {
				break;
			}
		}

		foreach (var unit in remove_buffer) {
			coroutine_list.Remove(unit);
		}
		remove_buffer.Clear();

		foreach (var buffer in coroutine_buffer) {
			if (!find(buffer.enumerator)) {
				coroutine_list.Add(new coroutine_unit(buffer));
			}
		}
		coroutine_buffer.Clear();
	}
}
