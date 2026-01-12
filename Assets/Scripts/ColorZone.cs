using UnityEngine;

public class ColorZone : MonoBehaviour
{
    public ColorType zoneColor;

    private void OnTriggerEnter(Collider other)
    {
        PickableObject obj = other.GetComponent<PickableObject>();
        if (obj == null) return;

        if (obj.colorType == zoneColor)
        {
            ScoreManager.Instance.AddScore(10);
            Destroy(obj.gameObject);
        }
        else
        {
            ScoreManager.Instance.AddScore(-5);
        }
    }
}
