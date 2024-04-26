using UnityEngine;

public class TapParticle : MonoBehaviour
{
    public GameObject prefab;
    public float deleteTime = 0.5f;

    // �n�[�g�̃p�[�e�B�N����0.5�b�o��
    public void TapHartParticle()
    {
        if (Input.GetMouseButtonDown(0))
        {
            var mousePosition = Input.mousePosition;
            mousePosition.z = 3f;
            GameObject clone = Instantiate(prefab, Camera.main.ScreenToWorldPoint(mousePosition), Quaternion.identity);
            Destroy(clone, deleteTime);
        }
    }
}
