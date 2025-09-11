using UnityEngine;

public class PhysicsManager : MonoBehaviour
{
    private GameManager gm;

    [Header("Física")]
    public float frictionPerSecond = 0.6f;
    [Range(0f, 1f)] public float restitutionBall = 0.92f;
    [Range(0f, 1f)] public float restitutionWall = 0.88f;

    private float smallGap = 0.001f;

    public void Init(GameManager manager) => gm = manager;

    public void StepPhysics(float dt)
    {
        foreach (var b in gm.bolas) DragBall(b, dt);

        foreach (var b in gm.bolas) b.Position += b.velocity * dt;

        foreach (var b in gm.bolas) ResolveWallCollision(b);

        for (int i = 0; i < gm.bolas.Count; i++)
        {
            for (int j = i + 1; j < gm.bolas.Count; j++)
                ResolveBallCollision(gm.bolas[i], gm.bolas[j]);
        }
    }

    void DragBall(Bola b, float dt)
    {
        if (b.velocity.sqrMagnitude <= 0f) return;
        float factor = 1f - frictionPerSecond * dt;
        if (factor < 0f) factor = 0f;
        b.velocity *= factor;
        if (b.velocity.magnitude < 0.01f) b.velocity = Vector2.zero;
    }

    void ResolveWallCollision(Bola b)
    {
        Vector2 p = b.Position, v = b.velocity;
        bool collided = false; Vector2 n = Vector2.zero;

        if (p.x - b.radius < gm.minX) { p.x = gm.minX + b.radius + smallGap; n = Vector2.right; collided = true; }
        else if (p.x + b.radius > gm.maxX) { p.x = gm.maxX - b.radius - smallGap; n = Vector2.left; collided = true; }
        if (p.y - b.radius < gm.minY) { p.y = gm.minY + b.radius + smallGap; n = Vector2.up; collided = true; }
        else if (p.y + b.radius > gm.maxY) { p.y = gm.maxY - b.radius - smallGap; n = Vector2.down; collided = true; }

        if (collided)
        {
            Vector2 reflected = v - 2f * Vector2.Dot(v, n) * n;
            b.velocity = reflected * restitutionWall;
        }

        b.Position = p;
    }

    void ResolveBallCollision(Bola a, Bola b)
    {
        Vector2 delta = b.Position - a.Position;
        float dist = delta.magnitude, rsum = a.radius + b.radius;

        if (dist < rsum && dist > 0f)
        {
            Vector2 n = delta / dist;
            float overlap = rsum - dist;
            a.Position -= n * overlap / 2f;
            b.Position += n * overlap / 2f;

            Vector2 relVel = a.velocity - b.velocity;
            float relNormal = Vector2.Dot(relVel, n);

            if (relNormal < 0)
            {
                float j = -(1f + restitutionBall) * relNormal / 2f;
                Vector2 impulse = j * n;
                a.velocity += impulse;
                b.velocity -= impulse;
            }
        }
    }
}
