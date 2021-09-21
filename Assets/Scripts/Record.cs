using System.Collections.Generic;

[System.Serializable]
public class Record
{
    public string username;
    public int score;

    public Record(string username, int score) {
        this.username = username;
        this.score = score;
    }
}
