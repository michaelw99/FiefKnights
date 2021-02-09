using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;

public class SceneLoader : MonoBehaviour
{

    void Awake()
    {
        string sceneName = SceneManager.GetActiveScene().name;
        RoomSave roomSave = GameStateManager.getRoomSave(sceneName);
        instantiateRoomPrefabs(roomSave);
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void instantiateRoomPrefabs(RoomSave roomSave)
    {
        // spawn player prefab
        Vector3 playerSpawnPoint = roomSave.playerSpawnLocations[roomSave.playerSpawnIndex]; // will need direction of entry for animation, possibly also player state
        Instantiate(Resources.Load("player"), playerSpawnPoint, Quaternion.identity);
    }
}
