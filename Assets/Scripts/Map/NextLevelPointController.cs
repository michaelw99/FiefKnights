using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class NextLevelPointController : MonoBehaviour
{

    public string nextSceneName;
    public int nextScenePlayerSpawnIndex;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.layer == LayerMask.NameToLayer("Player"))
        {
            // update next room state with proper player spawn index
            GameStateManager.updateRoomSave(nextSceneName, nextScenePlayerSpawnIndex);

            // load next room
            Debug.Log("entering next scene");
            Debug.Log(nextSceneName);
            Debug.Log("with spawn index");
            Debug.Log(nextScenePlayerSpawnIndex);
            SceneManager.LoadSceneAsync(nextSceneName);
        }
    }
}
