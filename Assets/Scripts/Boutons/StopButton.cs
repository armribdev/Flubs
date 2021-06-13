using UnityEngine;

public class StopButton : MonoBehaviour
{
    public void Stop()      // Bouton Stop
    {
        Time.timeScale = 0;
    }
}
