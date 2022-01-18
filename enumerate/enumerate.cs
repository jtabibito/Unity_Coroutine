using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface enumerate {
	object current { get; }
	bool move_next();
	void reset();
}
