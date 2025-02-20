using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ControlPanel : MonoBehaviour {
    public static ControlPanel instance;

    [SerializeField] Transform controlPanel;
    [SerializeField] Transform tuningPanel;
    [SerializeField] CommandGenerator generator;

    Button[] _controlButtons;
    Button[] _tuningButtons;


	void Start() {
        if (instance == null) {
            instance = this;

            _controlButtons = controlPanel.GetComponentsInChildren<Button>();
            _tuningButtons = tuningPanel.GetComponentsInChildren<Button>();
            ToggleTuningPanel(false);
		}
    }

    public void SendCommand(int index) {
        generator.SendCommand(index);
	}

	public void TuneDown() {
		generator.TuneDown();
        ToggleTuningPanel(false);
	}

	public void TuneUp() {
		generator.TuneUp();
		ToggleTuningPanel(false);
	}

    public void ToggleControlPanel(bool enabled) {
        foreach (Button button in _controlButtons) {
            button.interactable = enabled;
        }

        if (!enabled) ToggleTuningPanel(false);
    }

    public void ToggleTuningPanel(bool enabled) {
        foreach (Button button in _tuningButtons) {
            button.interactable = enabled;
            // Animation
        }
    }
}
