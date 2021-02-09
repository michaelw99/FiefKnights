using System.Collections;
using System.Collections.Generic;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;
using System.IO;
using UnityEngine;

public static class SaveManager
{

    private static string ABILITIES_FILE = "/abilities.save"; // track player abilities
    private static string ROOMS_FILE = "/rooms.save"; // track all room states

    public static PlayerStats loadPlayerStatsSave()
    {
        if (File.Exists(Application.persistentDataPath + ABILITIES_FILE))
        {
            BinaryFormatter bf = new BinaryFormatter();
            FileStream file = File.Open(Application.persistentDataPath + ABILITIES_FILE, FileMode.Open);
            PlayerStatsSave save = (PlayerStatsSave)bf.Deserialize(file);
            file.Close();

            PlayerStats playerStats = new PlayerStats();
            playerStats.maxHp = save.maxHp;
            playerStats.currHp = save.currHp;
            playerStats.maxMp = save.maxMp;
            playerStats.currMp = save.currMp;
            playerStats.damageMultiplier = save.damageMultiplier;
            playerStats.hasDash = save.hasDash;
            playerStats.hasDoubleJump = save.hasDoubleJump;
            playerStats.hasWallJump = save.hasWallJump;
            playerStats.hasMask = save.hasMask;
            playerStats.hasWindMask = save.hasWindMask;

            Debug.Log("Game Loaded");
            return playerStats;
        } else
        {
            Debug.Log("Failed to retrieve playerstats save");
            return null;
        }
    }

    public static void savePlayerStatsSave(PlayerStats playerStats)
    {
        // collect save
        PlayerStatsSave save = new PlayerStatsSave();
        save.maxHp = playerStats.maxHp;
        save.currHp = playerStats.currHp;
        save.maxMp = playerStats.maxMp;
        save.currMp = playerStats.currMp;
        save.damageMultiplier = playerStats.damageMultiplier;
        save.hasDash = playerStats.hasDash;
        save.hasDoubleJump = playerStats.hasDoubleJump;
        save.hasWallJump = playerStats.hasWallJump;
        save.hasMask = playerStats.hasMask;
        save.hasWindMask = playerStats.hasWindMask;

        // serialize to path 
        BinaryFormatter bf = new BinaryFormatter();
        FileStream file = File.Create(Application.persistentDataPath + ABILITIES_FILE);
        bf.Serialize(file, save);
        file.Close();

        Debug.Log("abilities saved");
    }

    public static TotalRoomSave loadTotalRoomSave()
    {
        if (File.Exists(Application.persistentDataPath + ROOMS_FILE))
        {
            BinaryFormatter bf = new BinaryFormatter();
            FileStream file = File.Open(Application.persistentDataPath + ROOMS_FILE, FileMode.Open);
            TotalRoomSave save = (TotalRoomSave)bf.Deserialize(file);
            file.Close();
            return save;
        } else
        {
            Debug.Log("failed to load map state");
            return null;
        }
    }

    public static void saveTotalRoomSave(TotalRoomSave save) 
    {
        BinaryFormatter bf = new BinaryFormatter();
        FileStream file = File.Create(Application.persistentDataPath + ROOMS_FILE);
        bf.Serialize(file, save);
        file.Close();

        Debug.Log("map state saved");
    }
}
