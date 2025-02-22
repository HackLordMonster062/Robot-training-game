using UnityEngine;

public class GameManager : MonoBehaviour {
    public static GameManager instance;

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
    }

    void Lose() {
        print("lose");
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
}
