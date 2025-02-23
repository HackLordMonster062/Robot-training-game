using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ControlPanel : MonoBehaviour {
    public static ControlPanel instance;

    [SerializeField] Transform controlPanel;
    [SerializeField] Transform tuningPanel;
    [SerializeField] TMP_Text timer;
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

	private void OnDestroy() {
        if (instance == this) instance = null;
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

    public void SetTime(float time) {
        int minutes = (int)(time / 60);
        int seconds = (int)(time % 60);

        timer.text = minutes + ":" + seconds.ToString("D2");
    }
}
