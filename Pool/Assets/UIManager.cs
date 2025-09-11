using UnityEngine;
using UnityEngine.UI;
using TMPro;
public class UIManager : MonoBehaviour
{
    private GameManager gm;

    [Header("UI")]
    public TextMeshProUGUI uiSpeedText;
    public TextMeshProUGUI  uiAngleText;
    public LineRenderer aimLine;

    public void Init(GameManager manager) => gm = manager;

    public void UpdateAimUI(Vector2 dragStart, Vector2 dragEnd)
    {
        Vector2 startWorld = Camera.main.ScreenToWorldPoint(dragStart);
        Vector2 endWorld = Camera.main.ScreenToWorldPoint(dragEnd);
        Vector2 dir = (Vector2)startWorld - endWorld;

        float speed = dir.magnitude * 3f;
        float angle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;

        if (uiSpeedText) uiSpeedText.text = $"Vel: {speed:F2}";
        if (uiAngleText) uiAngleText.text = $"Ang: {angle:F1}�";

        if (aimLine)
        {
            aimLine.positionCount = 2;
            aimLine.SetPosition(0, startWorld);
            aimLine.SetPosition(1, startWorld - dir.normalized * Mathf.Min(dir.magnitude, 3f));
        }
    }

    public void ClearAimUI()
    {
        if (uiSpeedText) uiSpeedText.text = "";
        if (uiAngleText) uiAngleText.text = "";
    }

    public void ShowAimLine(bool state)
    {
        if (aimLine) aimLine.enabled = state;
    }

    public void SyncUI()
    {
        if (gm.whiteBall != null)
        {
            float sp = gm.whiteBall.velocity.magnitude;
            float ang = Mathf.Atan2(gm.whiteBall.velocity.y, gm.whiteBall.velocity.x) * Mathf.Rad2Deg;
            if (uiSpeedText) uiSpeedText.text = $"Speed: {sp:F2}";
            if (uiAngleText) uiAngleText.text = $"Angle: {ang:F1}�";
        }
    }

  

}
