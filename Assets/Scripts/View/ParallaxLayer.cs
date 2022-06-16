using UnityEngine;

namespace View
{
    /// <summary>
    /// Used to move a transform relative to be main camera position with a scale factor applied.
    /// This used to implement parallax scrolling effects on different branches of gameobjects.
    /// </summary>
    public class ParallaxLayer : MonoBehaviour
    {
        /// <summary>
        /// Movement of the layer is scaled by this value.
        /// </summary>
        public Vector3 movementScale = Vector3.one;

        private Transform _camera;

        private void Awake()
        {
            _camera = Camera.main!.transform;
        }

        private void LateUpdate()
        {
            transform.position = Vector3.Scale(_camera.position, movementScale);
        }
    }
}