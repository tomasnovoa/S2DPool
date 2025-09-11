using UnityEngine;

public class InputManager : MonoBehaviour
{
    private GameManager gm;
    private Vector2 dragStartScreen;
    private Vector2 dragEndScreen;
    private bool isDragging = false;

    public void Init(GameManager manager) => gm = manager;

    public void HandleInput()
    {
        if (Input.GetMouseButtonDown(0))
        {
            isDragging = true;
            dragStartScreen = Input.mousePosition;
            gm.uiManager.ShowAimLine(true);
        }
        else if (Input.GetMouseButton(0) && isDragging)
        {
            dragEndScreen = Input.mousePosition;
            gm.uiManager.UpdateAimUI(dragStartScreen, dragEndScreen);
        }
        else if (Input.GetMouseButtonUp(0) && isDragging)
        {
            isDragging = false;
            dragEndScreen = Input.mousePosition;
            LaunchWhiteBall();
            gm.uiManager.ShowAimLine(false);
            gm.uiManager.ClearAimUI();
        }
    }

    void LaunchWhiteBall()
    {
        Vector2 startWorld = Camera.main.ScreenToWorldPoint(dragStartScreen);
        Vector2 endWorld = Camera.main.ScreenToWorldPoint(dragEndScreen);
        Vector2 dir = (Vector2)startWorld - endWorld;
        float speed = dir.magnitude * 3f;
        if (speed < 0.01f) return;
        gm.whiteBall.SetVelocity(dir.normalized * speed);
    }
}
