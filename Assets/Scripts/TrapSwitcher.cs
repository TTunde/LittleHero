using UnityEngine;

public class TrapSwitcher : MonoBehaviour
{
    public TrapFire myTrap;
    private Animator myAnimator;
    [Header("Audio info")]
    [SerializeField] AudioClip audioSFX;



    private void Start()
    {
        myAnimator = GetComponent<Animator>();
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.GetComponent<PlayerMovement>() != null)
        {
            // t�zcsapda kapcsol� ami elind�tja a lenyom�s anim�ci�t �s kikapcsolja 3 mp-re a t�zet
            myAnimator.SetTrigger("pressed");
            GetComponent<AudioSource>().PlayOneShot(audioSFX);
            myTrap.FireSwitchAfter(3);
        }
    }
}
