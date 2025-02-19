using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CommandGenerator : MonoBehaviour {
    [SerializeField] CommandReceiver receiver;
    [SerializeField] List<Command> commands;

    public void SendCommand(int index) {
        receiver.ReceiveCommand(commands[index]);
	}

	public void TuneDown() {
		receiver.TuneDown();
	}

	public void TuneUp() {
		receiver.TuneUp();
	}
}
