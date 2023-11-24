using UnityEngine;

namespace Managers
{
    public class AnimationManager : MonoBehaviour
    {
        [SerializeField] private Animator startScreenAnimator;
        [SerializeField] private Animator gameScreenAnimator;
        [SerializeField] private Animator endScreenAnimator;

        public void PlayEnterStartScreenAnimation()
        {
            startScreenAnimator.SetTrigger("Enter");
        }

        public void PlayExitStartScreenAnimation()
        {
            startScreenAnimator.SetTrigger("Exit");
        }

        public void PlayEnterGameAnimation()
        {
            gameScreenAnimator.SetTrigger("Enter");
        }

        public void PlayEnterGameOverAnimation()
        {
            endScreenAnimator.SetTrigger("Enter");
        }

        public void PlayExitGameOverAnimation()
        {
            endScreenAnimator.SetTrigger("Exit");
        }
    }
}