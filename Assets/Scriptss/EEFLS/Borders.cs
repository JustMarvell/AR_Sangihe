using System.IO;
using UnityEngine;
using UnityEngine.TextCore.Text;

public class Borders : MonoBehaviour
{
    public enum BorderPosition {
        xPositive,
        xNegative,
        zPositive,
        zNegative
    }

    public BorderPosition borderPosition;
    public float offset = .4f;
    
    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            Vector3 initialPosition = other.transform.localPosition;
            Vector3 targetPosition = Vector3.zero;

            switch (borderPosition)
            {
                case BorderPosition.xPositive:
                    // example (4, 0, -3) => (-4, 0 ,-3)
                    targetPosition = new Vector3((initialPosition.x - offset) * -1, initialPosition.y, initialPosition.z);
                    break;
                case BorderPosition.xNegative:
                    // example (-4, 0, -3) => (4, 0 ,-3)
                    targetPosition = new Vector3((initialPosition.x + offset) * -1, initialPosition.y, initialPosition.z);
                    break;
                case BorderPosition.zPositive:
                    targetPosition = new Vector3(initialPosition.x, initialPosition.y, (initialPosition.z - offset) * -1);
                    break;
                case BorderPosition.zNegative:
                    targetPosition = new Vector3(initialPosition.x, initialPosition.y, (initialPosition.z + offset) * -1);
                    break;
            }

            other.GetComponent<CharacterController>().enabled = false;
            other.transform.localPosition = targetPosition;
            
            other.GetComponent<CharacterController>().enabled = true;
        }
    }
}
