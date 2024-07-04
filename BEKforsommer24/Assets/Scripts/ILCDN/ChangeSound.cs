namespace IL3DN
{
    using UnityEngine;

    /// <summary>
    /// Override player sound when walking in different environments
    /// Attach this to a trigger
    /// </summary>
    public class ChangeSound : MonoBehaviour
    {
        public AudioClip[] footStepsOverride;
        public AudioClip jumpSound;
        public AudioClip landSound;
    }
}