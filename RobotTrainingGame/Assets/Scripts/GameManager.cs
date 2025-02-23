using UnityEngine;

public class GameManager : MonoBehaviour {
    public static GameManager instance;

    public static event System.Action OnPause;
    public static event System.Action OnResume;

    [SerializeField] float initialHealth;
    [SerializeField] float damageThreshold;

    public float RobotHealth { get; private set; }

	private void Awake() {
		if (instance != null) {
            Destroy(gameObject);
            return;
        }

        instance = this;
        DontDestroyOnLoad(gameObject);
	}

	void Start() {
        RobotHealth = initialHealth;
    }

    void Update() {
        
    }

    void Win() {
        print("win");
        OnPause?.Invoke();
    }

    void Lose() {
        print("lose");
        OnPause?.Invoke();
    }

    public void HitWalls(float force) {
        RobotHealth -= force;

        if (RobotHealth < 0) {
            Lose();
        }
    }

    public void HitEnvironemnt() {
        Lose();
    }

    public void FallInHole() {
        Lose();
    }

    public void DropBoxInHole() {
        Win(); // Maybe delay win?
    }

    public void DestroyBox() {
        Lose();
    }

    public void TimeOut() {
        Lose();
    }
}
