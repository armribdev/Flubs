using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PowerUp : MonoBehaviour
{
    public FlubSelector flubSelector;
    public int count;
    public int number;

    Text text;
    Button button;
    
    void Start() {
        button = GetComponent<Button>();
        text = GetComponentInChildren<Text>();
        text.text = count.ToString();
    }

    public void usePowerUp() {
        if (count > 0) {
            count = flubSelector.GivePowerUp(number, count);
            text.text = count.ToString();

            if (count == 0) {
                button.interactable = false;
            }
        }

    }
}
