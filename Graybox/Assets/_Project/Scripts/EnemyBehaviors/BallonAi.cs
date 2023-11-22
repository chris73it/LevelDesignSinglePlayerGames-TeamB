using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BallonAi : MonoBehaviour {
    [SerializeField] Button startButton;
    [SerializeField] Button pauseButton;

    public float speed;
    Vector3 targetPos;
    public GameObject ways;
    public Transform[] wayPoints;
    private bool isActive = false;
    int pointIndex;
    int pointCount;
    int direction = 1;

    private void Play() {
        isActive = true;
    }

    private void Awake() {
        wayPoints = new Transform[ways.transform.childCount];
        for(int i = 0; i < ways.gameObject.transform.childCount; i++) {
            wayPoints[i] = ways.transform.GetChild(i).gameObject.transform;
        }
    }

    private void Reset() {
        isActive = false;
    }

    private void Start() {
        PlaybackControl.play += Play;
        PlayerController.Respawn += Reset;
        pointCount = wayPoints.Length;
        pointIndex = 1;
        targetPos = wayPoints[pointIndex].transform.position;
    }

    private void Update() {
        if(isActive) {
            var step = speed * Time.deltaTime;
            transform.position = Vector3.MoveTowards(transform.position, targetPos, step);

            if(transform.position == targetPos) {
                NextPoint();
            }
        }
    }

    void NextPoint() {
        if(pointIndex == pointCount - 1) {
            direction = -1;
        }

        if(pointIndex == 0) {
            direction = 1;
        }

        pointIndex += direction;
        targetPos = wayPoints[pointIndex].transform.position;
    }
}
