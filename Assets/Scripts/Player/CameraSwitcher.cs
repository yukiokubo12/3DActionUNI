using UnityEngine;
using Cinemachine;

public class CameraSwitcher : MonoBehaviour
{
    /// <summary>カメラ切り替えキー</summary>
    [SerializeField] string m_switchButton = "Cancel";
    /// <summary>切り替えたい複数の Virtual Camera</summary>
    [SerializeField] Cinemachine​Virtual​Camera​Base[] m_virtualCameras = default;
    /// <summary>現在アクティブなカメラの Index</summary>
    int m_cameraIndex = 0;

    void Update()
    {
        // 設定したキーを押されたら、次のカメラに切り替える
        // if (Input.GetButtonDown(m_switchButton))
        // {
        //     m_cameraIndex = (m_cameraIndex + 1) % m_virtualCameras.Length;
            // m_virtualCameras[m_cameraIndex].MoveToTopOfPrioritySubqueue();
        // }
    }
}
