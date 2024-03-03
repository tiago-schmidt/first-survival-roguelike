[System.Serializable]
public class GameData
{
    public float gold;

    // this constructor is used as default values for a new game
    // when there is no previous data saved
    public GameData()
    {
        gold = 0f;
    }
}
