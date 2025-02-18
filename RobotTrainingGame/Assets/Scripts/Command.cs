using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Command", menuName = "Robot/Command")]
public class Command : ScriptableObject {
	public string Name;
	public List<Action> MainActions;
	public List<Action> SideEffects;
}