using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SettingsInputManager : MonoBehaviour
{
    //Used for singleton
    public static SettingsInputManager SIM;

    //Create Keycodes that will be associated with each of our commands.
    //These can be accessed by any other script in our game
    public KeyCode jump { get; set; }
    public KeyCode forward { get; set; }
    public KeyCode backward { get; set; }
    public KeyCode left { get; set; }
    public KeyCode right { get; set; }
    public KeyCode up { get; set; }
    public KeyCode down { get; set; }
    public KeyCode lightattack { get; set; }
    public KeyCode heavyattack { get; set; }
    public KeyCode dash { get; set; }
    public KeyCode skill { get; set; }
    public KeyCode mask { get; set; }
    public KeyCode finisher { get; set; }


    void Awake()
    {
        //Singleton pattern
        if (SIM == null)
        {
            DontDestroyOnLoad(gameObject);
            SIM = this;
        }
        else if (SIM != this)
        {
            Destroy(gameObject);
        }
        /*Assign each keycode when the game starts.
         * Loads data from PlayerPrefs so if a user quits the game,
         * their bindings are loaded next time. Default values
         * are assigned to each Keycode via the second parameter
         * of the GetString() function
         */
        jump = (KeyCode)System.Enum.Parse(typeof(KeyCode), PlayerPrefs.GetString("jumpKey", "Space"));
        left = (KeyCode)System.Enum.Parse(typeof(KeyCode), PlayerPrefs.GetString("leftKey", "LeftArrow"));
        right = (KeyCode)System.Enum.Parse(typeof(KeyCode), PlayerPrefs.GetString("rightKey", "RightArrow"));
        up = (KeyCode)System.Enum.Parse(typeof(KeyCode), PlayerPrefs.GetString("upKey", "UpArrow"));
        down = (KeyCode)System.Enum.Parse(typeof(KeyCode), PlayerPrefs.GetString("downKey", "DownArrow"));
        lightattack = (KeyCode)System.Enum.Parse(typeof(KeyCode), PlayerPrefs.GetString("lightAttackKey", "X"));
        heavyattack = (KeyCode)System.Enum.Parse(typeof(KeyCode), PlayerPrefs.GetString("heavyAttackKey", "C"));
        dash = (KeyCode)System.Enum.Parse(typeof(KeyCode), PlayerPrefs.GetString("dashKey", "V"));
        skill = (KeyCode)System.Enum.Parse(typeof(KeyCode), PlayerPrefs.GetString("skillKey", "A"));
        mask = (KeyCode)System.Enum.Parse(typeof(KeyCode), PlayerPrefs.GetString("maskKey", "S"));
        finisher = (KeyCode)System.Enum.Parse(typeof(KeyCode), PlayerPrefs.GetString("finisherKey", "D"));
    }

    void Start()
    {

    }

    void Update()
    {

    }
}
