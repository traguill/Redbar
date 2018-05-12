using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using XInputDotNetPure;

public class GamePadController : MonoBehaviour {

    public static GamePadController instance;

    float initialTimer = 1.0f;
    float timer = 1.0f;
    bool gamepadFound = false;
    PlayerIndex playerIndex;
    float intensity = 0.5F;

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

    public void SetTimer(float time)
    {
        initialTimer = timer = time;
    }

    public IEnumerator Vibrate()
    {
        
        while (timer >= 0)
        {
            GamePad.SetVibration(playerIndex, intensity * timer, intensity * timer);
            timer -= Time.deltaTime;
            yield return null;
        }

        yield return null;
        timer = initialTimer;
    }

}
