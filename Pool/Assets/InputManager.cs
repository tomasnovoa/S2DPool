using UnityEngine;

public class InputManager : MonoBehaviour
{
    private GameManager gm;
    private Vector2 arrastreP;
    private Vector2 arrastreF;
    private bool enArrastre = false;

    public void Init(GameManager manager) => gm = manager;

    public void Inputs()
    {
        if (Input.GetMouseButtonDown(0))
        {
            enArrastre = true;
            arrastreP = Input.mousePosition;
            gm.uiManager.MostrarAim(true);
        }
        else if (Input.GetMouseButton(0) && enArrastre)
        {
            arrastreF = Input.mousePosition;
            gm.uiManager.Aim(arrastreP, arrastreF);
        }
        else if (Input.GetMouseButtonUp(0) && enArrastre)
        {
            enArrastre = false;
            arrastreF = Input.mousePosition;
            Tiro();
            gm.uiManager.MostrarAim(false);
            gm.uiManager.ClearAim();
        }
    }

    void Tiro()
    {
        Vector2 inicial = Camera.main.ScreenToWorldPoint(arrastreP);
        Vector2 final = Camera.main.ScreenToWorldPoint(arrastreF);
        Vector2 direccion = (Vector2)inicial - final;
        float speed = direccion.magnitude * 3f;
        if (speed < 0.01f) return;
        gm.blanca.SetVelocity(direccion.normalized * speed);
    }
}
