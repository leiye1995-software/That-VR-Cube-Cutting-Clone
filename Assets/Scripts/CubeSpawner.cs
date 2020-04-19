using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CubeSpawner : MonoBehaviour
{

    //beatmap file in JSON format
    public TextAsset beatmap;
    //track info in JSON format
    public TextAsset info;
    //the music
    public AudioSource audioSource;
    public AudioClip audioClip;

    //With CubeBehaviour script
    public GameObject cubeBehaviour;
    public GameObject wallBehaviour;

    public Transform cameraPosition;

    /*
    Cube Spawner:
        2
        1
        0
          0 1 2 3
    */

    public Transform cubeSpawner00;
    public Transform cubeSpawner01;
    public Transform cubeSpawner02;
    public Transform cubeSpawner03;

    public Transform cubeSpawner10;
    public Transform cubeSpawner11;
    public Transform cubeSpawner12;
    public Transform cubeSpawner13;

    public Transform cubeSpawner20;
    public Transform cubeSpawner21;
    public Transform cubeSpawner22;
    public Transform cubeSpawner23;

    public float timeForNoteToReachPlayerSeconds = 1.75f;

    private List<List<Transform>> spawners;
    private int noteCounter;
    private int obstacleCounter;
    private TrackMap trackMap;
    private TrackInfo trackInfo;
    private float noteTimeMultiplier;

    void Start()
    {
        noteCounter = 0;
        obstacleCounter = 0;
        trackMap = JsonUtility.FromJson<TrackMap>(beatmap.text);
        trackInfo = JsonUtility.FromJson<TrackInfo>(info.text);

        noteTimeMultiplier = trackInfo._beatsPerMinute / 60f;

        spawners = new List<List<Transform>>();
        List<Transform> cubeSpawnersRow = new List<Transform>
        {
            cubeSpawner00,
            cubeSpawner01,
            cubeSpawner02,
            cubeSpawner03
        };

        spawners.Add(cubeSpawnersRow);

        cubeSpawnersRow = new List<Transform>
        {
            cubeSpawner10,
            cubeSpawner11,
            cubeSpawner12,
            cubeSpawner13
        };

        spawners.Add(cubeSpawnersRow);

        cubeSpawnersRow = new List<Transform>
        {
            cubeSpawner20,
            cubeSpawner21,
            cubeSpawner22,
            cubeSpawner23
        };

        spawners.Add(cubeSpawnersRow);


    }

    public void StartTrack()
    {
        //set spawner cluster at the hight of the camera
        transform.position = new Vector3(transform.position.x,
            cameraPosition.position.y,
            transform.position.z);

        foreach (TrackMap.Note note in trackMap._notes)
        {
            //1.75f is the amount of time that a note reaches the player
            //this spawns note in a certain amount of time
            //while right now two notes with the same _time
            //spawns in the correct place
            //racing with counter global variable
            //is weird and could lead to problems
            //TODO: change this
            Invoke("SpawnNote", (note._time / noteTimeMultiplier) - timeForNoteToReachPlayerSeconds);
        }

        foreach (TrackMap.Obstacle obstacle in trackMap._obstacles)
        {
            //TODO: same as notes with the counter
            Invoke("SpawnWall", (obstacle._time / noteTimeMultiplier) - timeForNoteToReachPlayerSeconds);
        }

        audioSource.PlayOneShot(audioClip);
    }

    void SpawnNote()
    {
        TrackMap.Note note = trackMap._notes[noteCounter];
        ++noteCounter;
        Transform spawnPoint = spawners[note._lineLayer][note._lineIndex];
        GameObject newCube = GameObject.Instantiate(cubeBehaviour, spawnPoint.position, cubeBehaviour.transform.rotation);
        CubeBehaviour newCubeBehaviour = newCube.GetComponent<CubeBehaviour>();
        newCubeBehaviour.SetCubeProperties(note._type, note._cutDirection);
    }

    void SpawnWall()
    {
        TrackMap.Obstacle obstacle = trackMap._obstacles[obstacleCounter];
        ++obstacleCounter;
        int lineLayer;
        float height;
        if (obstacle._type == 0)
        {
            //vertical wall
            lineLayer = 1;
            height = 3f;
        }
        else
        {
            //horizontal wall (ceiling)
            lineLayer = 2;
            height = 1f;
        }
        Transform spawnPoint = spawners[lineLayer][obstacle._lineIndex];
        GameObject newWall = GameObject.Instantiate(wallBehaviour, spawnPoint.position, cubeBehaviour.transform.rotation);
        WallBehaviour newWallBehaviour = newWall.GetComponent<WallBehaviour>();
        //only one "type" of wall, lets just set things here for now
        //set scales: width (x), height (y) and depth (z)
        //the 4 for depth is found out just by guessing how to space for a duration of one beat
        Vector3 newScale = new Vector3(
            newWall.transform.localScale.x * (float)obstacle._width,
            newWall.transform.localScale.y * height,
            newWall.transform.localScale.z * obstacle._duration * 4f);
        newWall.transform.localScale = newScale;
        //reposition the wall because of scaling
        //can't explain how it works, it just works, for now...
        newWall.transform.position = new Vector3(
            spawnPoint.transform.position.x + ((float)(obstacle._width - 1) * (spawnPoint.localScale.x / 2f)),
            spawnPoint.transform.position.y,
            spawnPoint.transform.position.z + (newWall.transform.localScale.z / 2f)/* + (obstacle._duration / 2)*/);
        //Debug.Log(newWall.transform.position.x + ":" + newWall.transform.position.y + ":" + newWall.transform.position.z);
    }
}
