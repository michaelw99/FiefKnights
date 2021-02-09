using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class GameStateManager
{

    static RoomStats roomStats;
    static PlayerStats playerStats;

    public static void loadRoomStats()
    {
        TotalRoomSave totalRoomSave = SaveManager.loadTotalRoomSave();
        roomStats = new RoomStats(totalRoomSave);
    }

    public static void loadPlayerStats()
    {

    }

    /*
     * Returns the save data for 'sceneIndex' room (found in file -> build settings)
     */
    public static RoomSave getRoomSave(string sceneName) 
    {
        return roomStats.getRoomData(sceneName);
    }

    /*
     * Updates the save data for 'sceneIndex' room (found in file -> build settings)
     */
    public static void updateRoomSave(string sceneName, int playerSpawnIndex)
    {
        roomStats.updateRoomData(sceneName, playerSpawnIndex);
    }

    /*
     * Serializes all state
     */
    public static void saveAll()
    {

    }

}
