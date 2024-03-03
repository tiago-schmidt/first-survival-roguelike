using System;
using System.IO;
using UnityEngine;
using Debug = UnityEngine.Debug;

public class FileDataHandler
{
    private readonly string _dataDirPath = "";
    private readonly string _dataFileName = "";
    private bool _useEncryption = false;
    private readonly string _encryptionCodeWord = System.Guid.NewGuid().ToString();

    public FileDataHandler(string dataDirPath, string dataFileName, bool useEncryption)
    {
        _dataDirPath = dataDirPath;
        _dataFileName = dataFileName;
        _useEncryption = useEncryption;
    }

    public GameData Load()
    {
        // using Path.Combine due to different OS's having different path separators
        string fullPath = Path.Combine(_dataDirPath, _dataFileName);

        GameData loadedData = null;

        if (File.Exists(fullPath))
        {
            try
            {
                // load serialized data from the file
                string dataToLoad = "";

                // write the serialized data to file
                using FileStream stream = new(fullPath, FileMode.Open);
                using StreamReader reader = new(stream);

                dataToLoad = reader.ReadToEnd();

                // optionally decrypt
                if(_useEncryption)
                {
                    dataToLoad = EncryptDecrypt(dataToLoad);
                }

                // deserialize the JSON into the C# game data object
                loadedData = JsonUtility.FromJson<GameData>(dataToLoad);
            }
            catch(Exception e)
            {
                Debug.LogError("Error occured when trying to load data to file: " + fullPath + "\n" + e);
            }
        }        

        return loadedData;
    }

    public void Save(GameData data)
    {
        // using Path.Combine due to different OS's having different path separators
        string fullPath = Path.Combine(_dataDirPath, _dataFileName);
        try
        {
            // create the directory the file will be written to if it doesn't already exist
            Directory.CreateDirectory(Path.GetDirectoryName(fullPath));

            // serialize the C# game data object to JSON
            string dataToStore = JsonUtility.ToJson(data, true);

            // optionally encrypt
            if(_useEncryption)
            {
                dataToStore = EncryptDecrypt(dataToStore);
            }

            // write the serialized data to file
            using FileStream stream = new(fullPath, FileMode.Create);
            using StreamWriter writer = new(stream);

            writer.Write(dataToStore);
        }
        catch(Exception e)
        {
            Debug.LogError("Error occured when trying to save data to file: " + fullPath + "\n" + e);
        }
    }

    // simple implementation of XOR Encryption
    private string EncryptDecrypt(string data)
    {
        string modifiedData = "";
        for (int i = 0; i < data.Length; i++)
        {
            modifiedData += (char) (data[i] ^ _encryptionCodeWord[i % _encryptionCodeWord.Length]);
        }
        return modifiedData;
    }
}
