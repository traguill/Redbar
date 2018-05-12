using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using XInputDotNetPure;

public class GamePadController : MonoBehaviour {

    public static GamePadController instance;

    bool gamepadFound = false;
    PlayerIndex playerIndex;

    private void Awake()
    {
        instance = this;
    }

    // Use this for initialization
    void Start () {
        if (!gamepadFound)
        {
            for (int i = 0; i < 4 && gamepadFound; ++i)
            {
                GamePadState gamepadCurrentState = GamePad.GetState((PlayerIndex)i);
                if (gamepadCurrentState.IsConnected)
                {
                    playerIndex = (PlayerIndex)i;
                    gamepadFound = true;
                }
            }
        }
    }

    public void Vibrate(float intensity)
    {
        GamePad.SetVibration(playerIndex, intensity, intensity);
    }

}
