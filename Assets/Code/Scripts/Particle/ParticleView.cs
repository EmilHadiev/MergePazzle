using UnityEngine;

public class ParticleView : MonoBehaviour
{
    public void Show()
    {
        Hide();
        gameObject.SetActive(true);
    }

    private void Hide()
    {
        gameObject.SetActive(false);
    }
}