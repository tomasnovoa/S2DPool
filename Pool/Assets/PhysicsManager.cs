using UnityEngine;

public class PhysicsManager : MonoBehaviour
{
    private GameManager gm;

    public float friccionPorSegundo = 0.6f;
    [Range(0f, 1f)] public float restitutionBall = 0.92f;
    [Range(0f, 1f)] public float restitutionWall = 0.88f;

    private float margen = 0.001f;

    public void Init(GameManager manager) => gm = manager;

    public void SeteoPhysics(float dt)
    {
        foreach (var b in gm.bolas) Friccion(b, dt);

        foreach (var b in gm.bolas) b.Position += b.velocity * dt;

        foreach (var b in gm.bolas) ColisionPared(b);

        for (int i = 0; i < gm.bolas.Count; i++)
        {
            for (int j = i + 1; j < gm.bolas.Count; j++)
                ColisionBolas(gm.bolas[i], gm.bolas[j]);
        }
    }

    void Friccion(Bola b, float dt)
    {
        if (b.velocity.sqrMagnitude <= 0f) return;
        float factor = 1f - friccionPorSegundo * dt;
        if (factor < 0f) factor = 0f;
        b.velocity *= factor;
        if (b.velocity.magnitude < 0.01f) b.velocity = Vector2.zero;
    }

    void ColisionPared(Bola b)
    {
        Vector2 p = b.Position, v = b.velocity;
        bool collided = false; Vector2 n = Vector2.zero;

        if (p.x - b.radio < gm.minX) { p.x = gm.minX + b.radio + margen; n = Vector2.right; collided = true; }
        else if (p.x + b.radio > gm.maxX) { p.x = gm.maxX - b.radio - margen; n = Vector2.left; collided = true; }
        if (p.y - b.radio < gm.minY) { p.y = gm.minY + b.radio + margen; n = Vector2.up; collided = true; }
        else if (p.y + b.radio > gm.maxY) { p.y = gm.maxY - b.radio - margen; n = Vector2.down; collided = true; }

        if (collided)
        {
            Vector2 reflected = v - 2f * Vector2.Dot(v, n) * n;
            b.velocity = reflected * restitutionWall;
        }

        b.Position = p;
    }

    void ColisionBolas(Bola a, Bola b)
    {
        Vector2 delta = b.Position - a.Position;
        float dist = delta.magnitude, rsum = a.radio + b.radio;

        if (dist < rsum && dist > 0f)
        {
            Vector2 n = delta / dist;
            float overlap = rsum - dist;
            a.Position -= n * overlap / 2f;
            b.Position += n * overlap / 2f;

            Vector2 velocidadR = a.velocity - b.velocity;
            float velocidadN = Vector2.Dot(velocidadR, n);

            if (velocidadN < 0)
            {
                float j = -(1f + restitutionBall) * velocidadN / 2f;
                Vector2 impulso = j * n;
                a.velocity += impulso;
                b.velocity -= impulso;
            }
        }
    }
}
