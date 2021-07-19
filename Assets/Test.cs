using UnityEngine;

class Test : MonoBehaviour
{
    [SerializeField] Texture2D _gradient = null;
    [SerializeField] ComputeShader _filter = null;
    [SerializeField] Material _visualizer = null;
    [SerializeField] Mesh _quadMesh = null;

    ComputeBuffer _buffer;

    bool IsLinear
      => QualitySettings.activeColorSpace == ColorSpace.Linear;

    void Start()
    {
        _buffer = new ComputeBuffer(256 * 2, sizeof(float));

        _filter.SetBool("InputIsLinear", IsLinear);
        _filter.SetTexture(0, "Input", _gradient);
        _filter.SetBuffer(0, "Output", _buffer);
        _filter.Dispatch(0, 256 / 32, 1, 1);

        _visualizer.SetBuffer("_Buffer", _buffer);
    }

    void OnDestroy()
      => _buffer.Dispose();

    void OnRenderObject()
    {
        _visualizer.SetPass(0);
        Graphics.DrawMeshNow(_quadMesh, Matrix4x4.identity);
    }
}
