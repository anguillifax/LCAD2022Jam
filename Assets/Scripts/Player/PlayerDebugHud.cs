using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class PlayerDebugHud : MonoBehaviour
{
	public Player player;
	public TextMeshProUGUI text;

	private void OnGUI() {
		var output = new List<string>();
		if (player.fire.current > 0) {
			output.Add($"fire: {player.fire.current:.0}\n");
		}
		if (player.paint.current > 0) {
			output.Add($"paint: {player.paint.current:.0}\n");
		}
		if (player.water.current > 0) {
			output.Add($"water: {player.water.current:.0}\n");
		}
		text.text = string.Concat(output);
	}
}
