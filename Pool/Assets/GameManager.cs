using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SocialPlatforms;

public class GameManager : MonoBehaviour
{
    
    public GameObject bola_blanca;
    public GameObject bola_Normal;
    public int numeroBolas = 15;

    
    public float minX = -8f, maxX = 8f, minY = -4.5f, maxY = 4.5f;

    
    public InputManager inputManager;
    public PhysicsManager physicsManager;
    public UIManager uiManager;

    [HideInInspector] public Bola blanca;
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
        inputManager.Inputs();
        physicsManager.SeteoPhysics(Time.deltaTime);
        uiManager.SyncUI();
    }

    void SetupScene()
    {
        bolas.Clear();

        
        GameObject wgo = Instantiate(bola_blanca, Vector3.zero, Quaternion.identity);
        blanca = wgo.GetComponent<Bola>();
        blanca.esblanca = true;
        bolas.Add(blanca);

        
        for (int i = 0; i < numeroBolas; i++)
        {
            Vector2 pos = new Vector2(Random.Range(minX, maxX), Random.Range(minY, maxY));
            GameObject go = Instantiate(bola_Normal, pos, Quaternion.identity);
            Bola ball = go.GetComponent<Bola>();
            bolas.Add(ball);

            
            float velocidadInicial = Random.Range(0.5f, 2.0f);
            float angulo = Random.Range(0f, Mathf.PI * 2f);
            ball.velocity = new Vector2(Mathf.Cos(angulo), Mathf.Sin(angulo)) * velocidadInicial;
        }
    }

   
}

