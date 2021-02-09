using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class MenuScript : MonoBehaviour
{

    Transform menuPanel;
    Event keyEvent;
    Text buttonText;
    KeyCode newKey;

    bool waitingForKey;

    void Start()
    {
        //Assign menuPanel to the Panel object in our Canvas
        //Make sure it's not active when the game starts
        menuPanel = transform.Find("Panel");
        menuPanel.gameObject.SetActive(false);
        waitingForKey = false;

        /*iterate through each child of the panel and check
         * the names of each one. Each if statement will
         * set each button's text component to display
         * the name of the key that is associated
         * with each command. Example: the ForwardKey
         * button will display "W" in the middle of it
         */
        for (int i = 0; i < menuPanel.childCount; i++)
        {
            if (menuPanel.GetChild(i).name == "LeftKey")
                menuPanel.GetChild(i).GetComponentInChildren<Text>().text = SettingsInputManager.SIM.left.ToString();
            else if (menuPanel.GetChild(i).name == "RightKey")
                menuPanel.GetChild(i).GetComponentInChildren<Text>().text = SettingsInputManager.SIM.right.ToString();
            else if (menuPanel.GetChild(i).name == "JumpKey")
                menuPanel.GetChild(i).GetComponentInChildren<Text>().text = SettingsInputManager.SIM.jump.ToString();
            else if (menuPanel.GetChild(i).name == "UpKey")
                menuPanel.GetChild(i).GetComponentInChildren<Text>().text = SettingsInputManager.SIM.up.ToString();
            else if (menuPanel.GetChild(i).name == "DownKey")
                menuPanel.GetChild(i).GetComponentInChildren<Text>().text = SettingsInputManager.SIM.down.ToString();
            else if (menuPanel.GetChild(i).name == "LightAttackKey")
                menuPanel.GetChild(i).GetComponentInChildren<Text>().text = SettingsInputManager.SIM.lightattack.ToString();
            else if (menuPanel.GetChild(i).name == "HeavyAttackKey")
                menuPanel.GetChild(i).GetComponentInChildren<Text>().text = SettingsInputManager.SIM.heavyattack.ToString();
            else if (menuPanel.GetChild(i).name == "DashKey")
                menuPanel.GetChild(i).GetComponentInChildren<Text>().text = SettingsInputManager.SIM.dash.ToString();
            else if (menuPanel.GetChild(i).name == "SkillKey")
                menuPanel.GetChild(i).GetComponentInChildren<Text>().text = SettingsInputManager.SIM.skill.ToString();
            else if (menuPanel.GetChild(i).name == "MaskKey")
                menuPanel.GetChild(i).GetComponentInChildren<Text>().text = SettingsInputManager.SIM.mask.ToString();
            else if (menuPanel.GetChild(i).name == "FinisherKey")
                menuPanel.GetChild(i).GetComponentInChildren<Text>().text = SettingsInputManager.SIM.finisher.ToString();
        }
    }

    void Update()
    {
        //Escape key will open or close the panel
        if (Input.GetKeyDown(KeyCode.Escape) && !menuPanel.gameObject.activeSelf)
            menuPanel.gameObject.SetActive(true);
        else if (Input.GetKeyDown(KeyCode.Escape) && menuPanel.gameObject.activeSelf)
            menuPanel.gameObject.SetActive(false);
    }

    void OnGUI()
    {
        /*keyEvent dictates what key our user presses
         * bt using Event.current to detect the current
         * event
         */
        keyEvent = Event.current;

        //Executes if a button gets pressed and
        //the user presses a key
        if (keyEvent.isKey && waitingForKey)
        {
            newKey = keyEvent.keyCode; //Assigns newKey to the key user presses
            waitingForKey = false;
        }
    }

    /*Buttons cannot call on Coroutines via OnClick().
     * Instead, we have it call StartAssignment, which will
     * call a coroutine in this script instead, only if we
     * are not already waiting for a key to be pressed.
     */
    public void StartAssignment(string keyName)
    {
        if (!waitingForKey)
            StartCoroutine(AssignKey(keyName));
    }

    //Assigns buttonText to the text component of
    //the button that was pressed
    public void SendText(Text text)
    {
        buttonText = text;
    }

    //Used for controlling the flow of our below Coroutine
    IEnumerator WaitForKey()
    {
        while (!keyEvent.isKey)
            yield return null;
    }

    /*AssignKey takes a keyName as a parameter. The
     * keyName is checked in a switch statement. Each
     * case assigns the command that keyName represents
     * to the new key that the user presses, which is grabbed
     * in the OnGUI() function, above.
     */
    public IEnumerator AssignKey(string keyName)
    {
        waitingForKey = true;
        yield return WaitForKey(); //Executes endlessly until user presses a key

        switch (keyName)
        {
            case "left":
                SettingsInputManager.SIM.left = newKey; //set left to new keycode
                buttonText.text = SettingsInputManager.SIM.left.ToString(); //set button text to new key
                PlayerPrefs.SetString("leftKey", SettingsInputManager.SIM.left.ToString()); //save new key to playerprefs
                break;
            case "right":
                SettingsInputManager.SIM.right = newKey; //set right to new keycode
                buttonText.text = SettingsInputManager.SIM.right.ToString(); //set button text to new key
                PlayerPrefs.SetString("rightKey", SettingsInputManager.SIM.right.ToString()); //save new key to playerprefs
                break;
            case "jump":
                SettingsInputManager.SIM.jump = newKey; //set jump to new keycode
                buttonText.text = SettingsInputManager.SIM.jump.ToString(); //set button text to new key
                PlayerPrefs.SetString("jumpKey", SettingsInputManager.SIM.jump.ToString()); //save new key to playerprefs
                break;
            case "up":
                SettingsInputManager.SIM.jump = newKey; //set up to new keycode
                buttonText.text = SettingsInputManager.SIM.up.ToString(); //set button text to new key
                PlayerPrefs.SetString("upKey", SettingsInputManager.SIM.up.ToString()); //save new key to playerprefs
                break;
            case "down":
                SettingsInputManager.SIM.down = newKey; //set down to new keycode
                buttonText.text = SettingsInputManager.SIM.down.ToString(); //set button text to new key
                PlayerPrefs.SetString("downKey", SettingsInputManager.SIM.down.ToString()); //save new key to playerprefs
                break;
            case "lightattack":
                SettingsInputManager.SIM.lightattack = newKey; //set lightattack to new keycode
                buttonText.text = SettingsInputManager.SIM.lightattack.ToString(); //set button text to new key
                PlayerPrefs.SetString("lightAttackKey", SettingsInputManager.SIM.lightattack.ToString()); //save new key to playerprefs
                break;
            case "heavyattack":
                SettingsInputManager.SIM.heavyattack = newKey; //set heavyattack to new keycode
                buttonText.text = SettingsInputManager.SIM.heavyattack.ToString(); //set button text to new key
                PlayerPrefs.SetString("heavyAttackKey", SettingsInputManager.SIM.heavyattack.ToString()); //save new key to playerprefs
                break;
            case "dash":
                SettingsInputManager.SIM.dash = newKey; //set dash to new keycode
                buttonText.text = SettingsInputManager.SIM.dash.ToString(); //set button text to new key
                PlayerPrefs.SetString("dashKey", SettingsInputManager.SIM.dash.ToString()); //save new key to playerprefs
                break;
            case "skill":
                SettingsInputManager.SIM.skill = newKey; //set skill to new keycode
                buttonText.text = SettingsInputManager.SIM.skill.ToString(); //set button text to new key
                PlayerPrefs.SetString("skillKey", SettingsInputManager.SIM.skill.ToString()); //save new key to playerprefs
                break;
            case "mask":
                SettingsInputManager.SIM.mask = newKey; //set mask to new keycode
                buttonText.text = SettingsInputManager.SIM.mask.ToString(); //set button text to new key
                PlayerPrefs.SetString("maskKey", SettingsInputManager.SIM.mask.ToString()); //save new key to playerprefs
                break;
            case "finisher":
                SettingsInputManager.SIM.finisher = newKey; //set finisher to new keycode
                buttonText.text = SettingsInputManager.SIM.finisher.ToString(); //set button text to new key
                PlayerPrefs.SetString("finisherKey", SettingsInputManager.SIM.finisher.ToString()); //save new key to playerprefs
                break;
        }
        yield return null;
    }
}
