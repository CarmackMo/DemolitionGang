using UnityEngine;

public class EscapeAreaTrigger : MonoBehaviour
{
    public bool escaped
    {
        get { return escaped; }
    }

    public CountdownTrigger triggerCountdownScript;

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Excavator") && triggerCountdownScript.IsCountdownStarted())
        {
            triggerCountdownScript.SetEscaped(true);
        }
    }
}