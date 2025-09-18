using UnityEngine;
using UnityEngine.UI;
using TMPro;
public class UIManager : MonoBehaviour
{
    private GameManager gm;

    
    public TextMeshProUGUI uiSpeedText;
    public TextMeshProUGUI  uiAngleText;
    public LineRenderer aimLine;

    public void Init(GameManager manager) => gm = manager;

    public void Aim(Vector2 dragStart, Vector2 dragEnd)
    {
        Vector2 inicio = Camera.main.ScreenToWorldPoint(dragStart);
        Vector2 final = Camera.main.ScreenToWorldPoint(dragEnd);
        Vector2 dir = (Vector2)inicio - final;

        float speed = dir.magnitude * 3f;
        float angle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;

        if (uiSpeedText) uiSpeedText.text = $"Vel: {speed:F2}";
        if (uiAngleText) uiAngleText.text = $"Ang: {angle:F1}�";

        if (aimLine)
        {
            aimLine.positionCount = 2;
            aimLine.SetPosition(0, inicio);
            aimLine.SetPosition(1, inicio - dir.normalized * Mathf.Min(dir.magnitude, 3f));
        }
    }

    public void ClearAim()
    {
        if (uiSpeedText) uiSpeedText.text = "";
        if (uiAngleText) uiAngleText.text = "";
    }

    public void MostrarAim(bool state)
    {
        if (aimLine) aimLine.enabled = state;
    }

    public void SyncUI()
    {
        if (gm.blanca != null)
        {
            float sp = gm.blanca.velocity.magnitude;
            float ang = Mathf.Atan2(gm.blanca.velocity.y, gm.blanca.velocity.x) * Mathf.Rad2Deg;
            if (uiSpeedText) uiSpeedText.text = $"Speed: {sp:F2}";
            if (uiAngleText) uiAngleText.text = $"Angle: {ang:F1}�";
        }
    }

  

}
