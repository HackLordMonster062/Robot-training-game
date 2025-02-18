using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(RobotController))]
public class CommandReceiver : MonoBehaviour {
    [SerializeField] float minAmplitude;
    [SerializeField] float maxAmplitude;

    Dictionary<Command, float> _commandAmplitudes;

    RobotController _controller;

    void Start() {
        _controller = GetComponent<RobotController>();

        _commandAmplitudes = new();
    }

    void Update() {
        
    }

    public void ReceiveCommand(Command command) {
        if (_controller.IsActive) return;

        if (!_commandAmplitudes.ContainsKey(command)) {
            _commandAmplitudes.Add(command, Random.Range(minAmplitude, maxAmplitude));
        }

        float amplitude = _commandAmplitudes[command];

        foreach (Action action in command.MainActions) {
            PerformAction(action, amplitude);
        }
    }

    private void PerformAction(Action action, float amplitude) {
        switch (action) {
            case Action.Forward:
                _controller.GoForward(amplitude, amplitude);
                break;
            case Action.Backward:
				break;
            case Action.Left:
				_controller.TurnLeft(amplitude, amplitude);
				break;
            case Action.Right:
				_controller.TurnRight(amplitude, amplitude);
				break;
            case Action.Pickup:
				_controller.Pickup(amplitude);
				break;
            case Action.Drop:
				_controller.Drop(amplitude);
				break;
            default:
                break;
        }
    }
}
