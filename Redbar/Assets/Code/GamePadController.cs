using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using XInputDotNetPure;

public class GamePadController : MonoBehaviour {

    public static GamePadController instance;

    float initialTimer = 1.0f;
    float timer = 0.5f;
    bool gamepadFound = false;
    PlayerIndex playerIndex;
    float intensity = 0.5f;

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
        initialTimer = time;
        timer = time;
    }

    public IEnumerator Vibrate()
    {
        if(timer == initialTimer || timer  <= 0)
        {
            while (timer >= 0)
            {
                float power = intensity * timer;
                Mathf.Clamp(power, 0, 1);
                GamePad.SetVibration(playerIndex, power, power);
                timer -= Time.deltaTime;
                yield return null;
            }

            yield return new WaitForSeconds(0.5f);
            timer = initialTimer;
        }
        
    }

}
