using UnityEngine;

public class Bola : MonoBehaviour
{
    [Header("Propiedades")]
    public float radius = 0.25f;   // radio en unidades
    [HideInInspector] public Vector2 velocity = Vector2.zero;
    [HideInInspector] public bool isWhite = false; // si es la bola blanca

    // Para lectura rápida del transform
    private Transform _t;

    void Awake()
    {
        _t = transform;
    }

    public Vector2 Position
    {
        get => _t.position;
        set => _t.position = value;
    }

    // Método de utilidad para asignar velocidad y opcionalmente ajustar visual
    public void SetVelocity(Vector2 v)
    {
        velocity = v;
    }
}
