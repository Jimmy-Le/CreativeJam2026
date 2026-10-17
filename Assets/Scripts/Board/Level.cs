using System;
using System.Collections.Generic;
using UnityEngine;
using static MusicManager;

[CreateAssetMenu(fileName = "Level", menuName = "Scriptable Objects/Level")]
public class Level : ScriptableObject
{
    [SerializeField] public string levelName;
    [SerializeField] public Sprite levelSelectBackgroundPicture;
    [SerializeField] public Sprite levelSelectForegroundPicture;
    [SerializeField] public MusicType levelMusic;
    [SerializeField] public int stepsAllowed = 3;
    [SerializeField] public int BoardWidth;
    [SerializeField] public List<TileAndChild> levelTilesToGenerate = new();

    #region JSON Methods
    /// <summary>
    /// Loads the level data from a JSON string representation.
    /// </summary>
    /// <param name="jsonString">The JSON string representing the level data.</param>
    public void LoadFromJson(string jsonString)
    {
        LevelData data = JsonUtility.FromJson<LevelData>(jsonString);

        levelName = data.levelName;
        levelMusic = data.levelMusic;
        stepsAllowed = data.stepsAllowed;
        BoardWidth = data.BoardWidth;

        if (!string.IsNullOrEmpty(data.levelSelectBackgroundPicturePath))
        {
            levelSelectBackgroundPicture = LoadSprite("Sprites", data.levelSelectBackgroundPicturePath);
            if (levelSelectBackgroundPicture == null)
                Debug.LogError($"[Level Import] Could not find Sprite: '{data.levelSelectBackgroundPicturePath}'");
        }

        if (!string.IsNullOrEmpty(data.levelSelectForegroundPicturePath))
        {
            levelSelectForegroundPicture = LoadSprite("Sprites", data.levelSelectForegroundPicturePath);
            if (levelSelectForegroundPicture == null)
                Debug.LogError($"[Level Import] Could not find Sprite: '{data.levelSelectForegroundPicturePath}'");
        }

        levelTilesToGenerate.Clear();
        foreach (var tileData in data.levelTilesToGenerate)
        {
            GameObject loadedTile = null;
            if (!string.IsNullOrEmpty(tileData.tile))
            {
                string cleanTileName = System.IO.Path.GetFileNameWithoutExtension(tileData.tile);
                loadedTile = Resources.Load<GameObject>("Prefabs/Tiles/" + cleanTileName);
                if (loadedTile == null)
                    Debug.LogError($"[Level Import] Could not find Tile GameObject: '{tileData.tile}'");
            }

            GameObject loadedComponent = null;
            if (!string.IsNullOrEmpty(tileData.tileComponent))
            {
                string cleanCompName = System.IO.Path.GetFileNameWithoutExtension(tileData.tileComponent);
                loadedComponent = Resources.Load<GameObject>("Prefabs/Components/" + cleanCompName);
                if (loadedComponent == null)
                    Debug.LogError($"[Level Import] Could not find Component GameObject: '{tileData.tileComponent}'");
            }

            levelTilesToGenerate.Add(new TileAndChild
            {
                tile = loadedTile,
                tileComponent = loadedComponent
            });
        }
    }

    /// <summary>
    /// Helper method to load a sprite, supporting both standalone files and sliced sprite sheets.
    /// </summary>
    /// <param name="folderPath">The path to the folder containing the sprite.</param>
    /// <param name="spriteName">The name of the sprite to load.</param>
    /// <returns>The loaded sprite, or null if not found.</returns>
    private Sprite LoadSprite(string folderPath, string spriteName)
    {
        spriteName = System.IO.Path.GetFileNameWithoutExtension(spriteName);

        Sprite s = Resources.Load<Sprite>(folderPath + "/" + spriteName);
        if (s != null) return s;

        Sprite[] allSprites = Resources.LoadAll<Sprite>(folderPath);
        foreach (var sprite in allSprites)
        {
            if (sprite.name == spriteName)
            {
                return sprite;
            }
        }

        return null;
    }

    /// <summary>
    /// Saves the level data to a JSON string representation. 
    /// </summary>
    /// <returns>The JSON string representing the level data.</returns>
    public string SaveToJson()
    {
        LevelData data = new LevelData
        {
            levelName = levelName,
            levelMusic = levelMusic,
            stepsAllowed = stepsAllowed,
            BoardWidth = BoardWidth
        };

        data.levelSelectBackgroundPicturePath = levelSelectBackgroundPicture != null ? levelSelectBackgroundPicture.name : string.Empty;
        data.levelSelectForegroundPicturePath = levelSelectForegroundPicture != null ? levelSelectForegroundPicture.name : string.Empty;

        data.levelTilesToGenerate.Clear();
        foreach (var item in levelTilesToGenerate)
        {
            data.levelTilesToGenerate.Add(new TileDataPath
            {
                tile = item.tile != null ? item.tile.name : string.Empty,
                tileComponent = item.tileComponent != null ? item.tileComponent.name : string.Empty
            });
        }

        return JsonUtility.ToJson(data, true);
    }

    /// <summary>
    /// Exports the level data to a JSON file with the specified file name in the persistent data path.
    /// </summary>
    /// <param name="fileName">The name of the file to export the level data to.</param>
    public void ExportLevelToFile(string fileName)
    {
        string json = SaveToJson();
        string path = System.IO.Path.Combine(Application.persistentDataPath, fileName + ".json");
        
        System.IO.File.WriteAllText(path, json);
        Debug.Log($"Level exported to: {path}");
    }
    #endregion JSON Methods
}

[Serializable]
public class LevelData
{
    public string levelName;
    public string levelSelectBackgroundPicturePath;
    public string levelSelectForegroundPicturePath;
    public MusicType levelMusic;
    public int stepsAllowed = 3;
    public int BoardWidth;
    public List<TileDataPath> levelTilesToGenerate = new();
}

[Serializable]
public struct TileDataPath
{
    public string tile;
    public string tileComponent;
}

[Serializable]
public struct TileAndChild
{
    public GameObject tile;
    public GameObject tileComponent;
}
