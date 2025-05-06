using UnityEngine;

public class Advancer : MonoBehaviour
{
    [SerializeField] private Vector3 speed;

    //毎フレーム呼ばれる
    void Update()
    {
        //進む
        transform.position += speed * Time.deltaTime;
    }
}
