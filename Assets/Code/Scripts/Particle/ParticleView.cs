using UnityEngine;

public class ParticleView : MonoBehaviour
{
    public void Show()
    {
        Hide();
        gameObject.SetActive(true);
    }

    public void Hide()
    {
        gameObject.SetActive(false);
    }
}