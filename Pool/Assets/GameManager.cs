using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SocialPlatforms;

public class GameManager : MonoBehaviour
{
    [Header("Prefabs y Conteo")]
    public GameObject whiteBallPrefab;
    public GameObject ballPrefab;
    public int extraBalls = 15;

    [Header("Mesa (límites)")]
    public float minX = -8f, maxX = 8f, minY = -4.5f, maxY = 4.5f;

    [Header("Referencias a Managers")]
    public InputManager inputManager;
    public PhysicsManager physicsManager;
    public UIManager uiManager;

    [HideInInspector] public Bola whiteBall;
    [HideInInspector] public List<Bola> bolas = new List<Bola>();

    void Start()
    {
        SetupScene();
        inputManager.Init(this);
        physicsManager.Init(this);
        uiManager.Init(this);
    }

    void Update()
    {
        inputManager.HandleInput();
        physicsManager.StepPhysics(Time.deltaTime);
        uiManager.SyncUI();
    }

    void SetupScene()
    {
        bolas.Clear();

        // Bola blanca
        GameObject wgo = Instantiate(whiteBallPrefab, Vector3.zero, Quaternion.identity);
        whiteBall = wgo.GetComponent<Bola>();
        whiteBall.isWhite = true;
        bolas.Add(whiteBall);

        // Bolas extra
        for (int i = 0; i < extraBalls; i++)
        {
            Vector2 pos = new Vector2(Random.Range(minX, maxX), Random.Range(minY, maxY));
            GameObject go = Instantiate(ballPrefab, pos, Quaternion.identity);
            Bola ball = go.GetComponent<Bola>();
            bolas.Add(ball);

            // velocidad inicial aleatoria
            float sp = Random.Range(0.5f, 2.0f);
            float ang = Random.Range(0f, Mathf.PI * 2f);
            ball.velocity = new Vector2(Mathf.Cos(ang), Mathf.Sin(ang)) * sp;
        }
    }
}

