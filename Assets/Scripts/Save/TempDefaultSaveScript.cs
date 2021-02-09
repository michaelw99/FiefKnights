using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using SerializableTypes;

public class TempDefaultSaveScript : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.L))
        {
            Debug.Log("creating default save");
            loadDefaultSave();
        }
    }

    private void loadDefaultSave()
    {
        TotalRoomSave totalRoomSave = new TotalRoomSave();
        Dictionary<string, RoomSave> tempDict = new Dictionary<string, RoomSave>();

        RoomSave room1 = new RoomSave();
        room1.playerSpawnIndex = 0;
        room1.playerSpawnLocations = new SVector3[] {
            new SVector3(-10.3f ,12.67f ,0),
            new SVector3(-8.72f, 1.67f, 0f),
            new SVector3(34.84f, -31.3f, 0),
            new SVector3(37.13f, 7.65f ,0),
            new SVector3(5.07f ,19.61f ,0)};
        tempDict.Add("Arboretum0", room1);

        RoomSave room2 = new RoomSave();
        room2.playerSpawnIndex = 0;
        room2.playerSpawnLocations = new SVector3[] {
            new SVector3(-7.88f, 1.65f, 0),
            new SVector3(72.44f, -0.31f, 0)};
        tempDict.Add("Arboretum1", room2);

        RoomSave room3 = new RoomSave();
        room3.playerSpawnIndex = 0;
        room3.playerSpawnLocations = new SVector3[] {
            new SVector3(-7.36f, 1.7f, 0),
            new SVector3(-7.75f, -32.33f, 0),
            new SVector3(0.46f, -48.33f, 0),
            new SVector3(67.26f, -0.34f, 0)};
        tempDict.Add("Arboretum2", room3);

        RoomSave room4 = new RoomSave();
        room4.playerSpawnIndex = 0;
        room4.playerSpawnLocations = new SVector3[] {
            new SVector3(-4.88f, 1.66f, 0),
            new SVector3(63.68f, 3.30f, 0),
            new SVector3(66.12f, 8.71f, 0)};
        tempDict.Add("Arboretum3", room4);

        RoomSave room5 = new RoomSave();
        room5.playerSpawnIndex = 4;
        room5.playerSpawnLocations = new SVector3[] {
            new SVector3(-11.26f, 1.67f, 0)};
        tempDict.Add("Arboretum4", room5);

        RoomSave room6 = new RoomSave();
        room6.playerSpawnIndex = 0;
        room6.playerSpawnLocations = new SVector3[] {
            new SVector3(-7.84f, 1.65f, 0)};
        tempDict.Add("Arboretum5", room6);

        totalRoomSave.roomSaves = tempDict;
        SaveManager.saveTotalRoomSave(totalRoomSave);

        // initiate gamestateManager
        GameStateManager.loadRoomStats();
    }
}
