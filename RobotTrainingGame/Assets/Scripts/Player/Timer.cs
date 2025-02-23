using UnityEngine;

public class Timer : MonoBehaviour {
    [SerializeField] float startingTime;

    float _time;

    void Start() {
        _time = startingTime;

        GameManager.OnPause += Pause;
        GameManager.OnResume += Resume;
    }

    void Update() {
        _time -= Time.deltaTime;

        ControlPanel.instance.SetTime(_time);
        
        if (_time < 0) {
            GameManager.instance.TimeOut();
        }
    }

    void Pause() {
        Time.timeScale = 0;
    }

    void Resume() {
        Time.timeScale = 1;
    }
}
