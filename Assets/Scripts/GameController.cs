using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class GameController : MonoBehaviour
{
    private CubePosition nowCube = new(0, 1, 0);
    public float cubeChangePositionSpeed = 0.5f;
    public Transform CubeToPlace;
    public GameObject cubeToCreate, allcubes, vfx;
    private bool isLose, fisrtCube;
    private Coroutine showcubeplace;
    public GameObject[] canvasStartGame;
    public Text scoreTxt;
    public GameObject restartButton;
    private Transform lastPlacedCube;

    private readonly List<Vector3> allCubesPositions = new()
    {
        new(0, 0, 0),
        new(0, 1, 0),
        new(1, 0, 1),
        new(1, 0, -1),
        new(-1, 0, 1),
        new(-1, 0, -1),
        new(1, 0, 0),
        new(-1, 0, 0),
        new(0, 0, 1),
        new(0, 0, -1),
    };

    private Rigidbody allcubesrb;
    private Transform mainCam;

    private void Start()
    {
        scoreTxt.text = PlayerPrefs.GetInt("score") + "\n0";
        mainCam = Camera.main.transform;
        allcubesrb = allcubes.GetComponent<Rigidbody>();
        showcubeplace = StartCoroutine(ShowCubePlace());
    }

    private void Update()
    {
        if (CubeToPlace != null && allcubes != null && !EventSystem.current.IsPointerOverGameObject())
        {
            if (Input.GetMouseButtonDown(0) && !isLose)
            {
                if (!fisrtCube)
                {
                    fisrtCube = true;
                    foreach (GameObject obj in canvasStartGame)
                    {
                        Destroy(obj);
                    }
                }

                GameObject newCube = Instantiate(cubeToCreate, CubeToPlace.position, Quaternion.identity);
                if (newCube.TryGetComponent<Renderer>(out var renderer))
                {
                    renderer.material.color = new Color(Random.value, Random.value, Random.value);
                }

                newCube.transform.SetParent(allcubes.transform);
                lastPlacedCube = newCube.transform;
                nowCube.SetVector(CubeToPlace.position);
                allCubesPositions.Add(nowCube.GetVector());
                GameObject VFX = Instantiate(vfx, newCube.transform.position, Quaternion.identity);
                Destroy(VFX, 1.5f);

                if (PlayerPrefs.GetString("music") != "No")
                {
                    GetComponent<AudioSource>().Play();
                }

                SpawnPositions();
                allcubesrb.isKinematic = true;
                allcubesrb.isKinematic = false;
                MoveCameraChangeBg();
                mainCam.GetComponentInParent<RotateCamera>().target = lastPlacedCube;
            }
        }
        if (IsFallen() && !isLose)
        {
            Destroy(CubeToPlace.gameObject);
            StopCoroutine(showcubeplace);
            restartButton.SetActive(true);
            isLose = true;
        }
    }

    private bool IsFallen()
    {
        if (allcubes == null)
        {
            return true; // Объект не существует
        }

        Vector3 eulerAngles = allcubes.transform.localEulerAngles;

        // Приведение углов к диапазону -180 до 180
        float xAngle = eulerAngles.x > 180 ? eulerAngles.x - 360 : eulerAngles.x;
        float zAngle = eulerAngles.z > 180 ? eulerAngles.z - 360 : eulerAngles.z;

        // Проверка на падение
        return Mathf.Abs(xAngle) > 1f || Mathf.Abs(zAngle) > 1f;
    }

    private IEnumerator ShowCubePlace()
    {
        while (true)
        {
            SpawnPositions();
            yield return new WaitForSeconds(cubeChangePositionSpeed);
        }
    }

    private void SpawnPositions()
    {
        List<Vector3> positions = new();
        CheckAndAddPosition(positions, new Vector3(nowCube.x + 1, nowCube.y, nowCube.z));
        CheckAndAddPosition(positions, new Vector3(nowCube.x - 1, nowCube.y, nowCube.z));
        CheckAndAddPosition(positions, new Vector3(nowCube.x, nowCube.y + 1, nowCube.z));
        CheckAndAddPosition(positions, new Vector3(nowCube.x, nowCube.y - 1, nowCube.z));
        CheckAndAddPosition(positions, new Vector3(nowCube.x, nowCube.y, nowCube.z + 1));
        CheckAndAddPosition(positions, new Vector3(nowCube.x, nowCube.y, nowCube.z - 1));

        if (positions.Count > 0)
        {
            CubeToPlace.position = positions[Random.Range(0, positions.Count)];
        }
        else
        {
            isLose = true;
        }
    }

    private void CheckAndAddPosition(List<Vector3> positions, Vector3 newPosition)
    {
        if (IsPositionEmpty(newPosition))
        {
            positions.Add(newPosition);
        }
    }

    private bool IsPositionEmpty(Vector3 pos)
    {
        if (pos.y == 0)
        {
            return false;
        }
        foreach (Vector3 item in allCubesPositions)
        {
            if (item == pos)
            {
                return false;
            }
        }
        return true;
    }

    private void MoveCameraChangeBg()
    {
        if (PlayerPrefs.GetInt("score") < allcubes.transform.childCount)
        {
            PlayerPrefs.SetInt("score", allcubes.transform.childCount);
        }
        scoreTxt.text = PlayerPrefs.GetInt("score") + "\n" + allcubes.transform.childCount;
    }
}

internal struct CubePosition
{
    public int x, y, z;

    public CubePosition(int x, int y, int z)
    {
        this.x = x;
        this.y = y;
        this.z = z;
    }

    public readonly Vector3 GetVector()
    {
        return new Vector3(x, y, z);
    }

    public void SetVector(Vector3 pos)
    {
        x = Mathf.RoundToInt(pos.x);
        y = Mathf.RoundToInt(pos.y);
        z = Mathf.RoundToInt(pos.z);
    }
}