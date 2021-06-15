using UnityEngine;
using TMPro;


[RequireComponent(typeof(TextMeshProUGUI))]
public class QT_UIText : MonoBehaviour
{
    TextMeshProUGUI display;
    // Start is called before the first frame update
    void Start()
    {
        display = GetComponent<TextMeshProUGUI>();
        display.SetText("");
        if (QuickTimeMaster.current != null)
        {
            QuickTimeMaster.current.onQuickTimeStart += StartDisplay;
            QuickTimeMaster.current.onQuickTimeEnd += EndDisplay;
        }else
        {
            Debug.Log($"Quick time master was null, deleating this", this);
            Destroy(this);
        }
        //display.enabled = false;
        
    }


    private void EndDisplay(QuickTimeComponent arg1, bool arg2)
    {
        display.SetText("");
    }

    private void StartDisplay(QuickTimeComponent obj)
    {
        //Debug.Log("Updatting Text");
        //display.enabled = true;
        display.SetText(obj._event.UItext);


    }
    private void OnDestroy()
    {
        if (QuickTimeMaster.current != null)
        {
            QuickTimeMaster.current.onQuickTimeStart -= StartDisplay;
            QuickTimeMaster.current.onQuickTimeEnd -= EndDisplay;
        }
    }
}
