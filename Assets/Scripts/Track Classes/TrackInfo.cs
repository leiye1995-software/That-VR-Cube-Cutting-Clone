[System.Serializable]
public class TrackInfo
{
    public string _songName;
    public int _beatsPerMinute;
    public float _songTimeOffset;
    public string _songFilename;
    public string _coverImageFilename;
    public DifficultyBeatmapSet[] _difficultyBeatmapSets;

    [System.Serializable]
    public class DifficultyBeatmapSet
    {
        public DifficultyBeatmap[] _difficultyBeatmaps;
    }

    [System.Serializable]
    public class DifficultyBeatmap
    {
        public string _difficulty;
        public string _beatmapFilename;
    }

}
