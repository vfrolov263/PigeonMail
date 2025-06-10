using UnityEngine;

public class MoveCloud : MonoBehaviour
{
    private void Update()
    {
        transform.Translate(new Vector3(-0.1f, 0, 0));
    }
}
