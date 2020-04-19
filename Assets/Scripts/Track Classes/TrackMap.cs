[System.Serializable]
public class TrackMap
{
    public Note[] _notes;
    public Obstacle[] _obstacles;

    [System.Serializable]
    public class Note
    {
        /*
        _time= the beat which this note appears at
        _lineIndex= horizontal left to right [0-3]
        _lineLayer= vertical bottom to top [0-2]
        _type:
            0 = red
            1 = blue
            3 = mine
        _cutDirection:
            0 = bottom to top
            1 = top to bottom
            2 = right to left
            3 = left to right
            4 = bottom right to top left
            5 = bottom left to top right
            6 = top right to bottom left
            7 = top left to bottom right
            8 = dot
        */
        public float _time;
        public int _lineIndex;
        public int _lineLayer;
        public int _type;
        public int _cutDirection;
    }

    [System.Serializable]
    public class Obstacle
    {
        /*
        _time= the beat which this wall appears at
        _duration= duration in beats of this wall (depth)
        _type:
            0 = vertical wall
            1 = horizontal wall (ceiling, top row)
        _lineIndex= horizontal left to right [0-3]
        _width= horizontal width
        */
        public float _time;
        public float _duration;
        public int _type;
        public int _lineIndex;
        public int _width;
    }
}
