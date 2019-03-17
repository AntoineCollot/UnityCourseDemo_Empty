using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class RunAndJumpGameManager : MonoBehaviour {

    public SimpleCharacterControl character;
    public UnityEvent onGameOver = new UnityEvent();

    public static RunAndJumpGameManager Instance;
    float health = 1;
    public float Health
    {
        get
        {
            return health;
        }
        set
        {
            health = value;
            if(health<=0)
            {
                health = 0;
                GameOver();
            }
        }
    }

    public int Score
    {
        get
        {
            return Mathf.FloorToInt(character.transform.position.z);
        }
    }

    private void Awake()
    {
        Instance = this;
    }

    public void GameOver()
    {
        print("GameOver ! Score : " + Score);
        character.enabled = false;
        onGameOver.Invoke();
    }
}
