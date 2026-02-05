using UnityEngine;
using TMPro;
using System.Collections;

public class WaveUI : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI waveText;
    [SerializeField] private float displayTime = 2f;
    
    private Coroutine display;

    public void showWave(int waveIndex)
    {
        if(display != null)
            StopCoroutine(display);
        
        display = StartCoroutine(showRoutine(waveIndex));
    }

    private IEnumerator showRoutine(int waveIndex)
    {
        waveText.gameObject.SetActive(true);
        waveText.text = "Wave " + waveIndex;
        yield return new WaitForSeconds(displayTime);
        waveText.gameObject.SetActive(false);
    }
}
