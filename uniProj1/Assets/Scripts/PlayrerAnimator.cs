using UnityEngine;

public class PlayrerAnimator : MonoBehaviour
{
    [SerializeField] private Player player;
    private Animator animator;
    private void Awake()
    {
        animator = GetComponent<Animator>();
    }
    private void Update()
    {
        animator.SetBool("Walk", player.IsWalking());
    }
}
