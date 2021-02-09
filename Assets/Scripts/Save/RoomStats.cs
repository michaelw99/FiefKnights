using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoomStats
{
    TotalRoomSave save;

    public RoomStats(TotalRoomSave totalRoomSave)
    {
        save = totalRoomSave;
    }
    
    public RoomSave getRoomData(string sceneName)
    {
        return save.roomSaves[sceneName];
    }

    public void updateRoomData(string sceneName, int playerSpawnIndex)
    {
        RoomSave temp = save.roomSaves[sceneName];
        temp.playerSpawnIndex = playerSpawnIndex;

        save.roomSaves[sceneName] = temp;
    }

}
