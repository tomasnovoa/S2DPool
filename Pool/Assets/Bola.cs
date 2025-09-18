using UnityEngine;

public class Bola : MonoBehaviour
{
  
    public float radio = 0.25f;   
    [HideInInspector] public Vector2 velocity = Vector2.zero;
    [HideInInspector] public bool esblanca = false; 

    
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

  
    public void SetVelocity(Vector2 v)
    {
        velocity = v;
    }
      public void OnDrawGizmosSelected() 
    {
        Gizmos.color = Color.white;
        Gizmos.DrawWireSphere(transform.position, radio);
    }
}
