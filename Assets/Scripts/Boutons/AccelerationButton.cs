using UnityEngine;
using UnityEngine.UIElements;

public class AccelerationButton : MonoBehaviour
{
    public int accelerationRatio;   // Le ratio d'accélération du temps lorsqu'on apuie sur le bouton

    public void Accelerer()
    {
        Time.timeScale = accelerationRatio;
    }
}
