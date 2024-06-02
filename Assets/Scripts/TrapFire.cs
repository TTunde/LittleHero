using UnityEngine;
public class TrapFire : TrapController
{
    public bool isWorking;
    Animator myAnimator;
    TrapController trap;

    public float repeatRate;
    private void Start()
    {
        trap = GetComponent<TrapController>();
        myAnimator = GetComponent<Animator>();
        if (transform.parent == null)
        {
            InvokeRepeating("FireSwitch", 0, repeatRate);
        }
    }

    private void Update()
    {
        myAnimator.SetBool("isWorking", isWorking);
        if (isWorking)
        {
            GetComponent<AudioSource>().PlayOneShot(trap.audioSFX);
        }


    }
    public void FireSwitch()
    {
        isWorking = !isWorking;

    }

    public void FireSwitchAfter(float seconds)
    {
        CancelInvoke();
        isWorking = false;
        Invoke("FireSwitch", seconds);
    }
    protected override void OnTriggerEnter2D(Collider2D collision)
    {

        if (isWorking)
        {
            base.OnTriggerEnter2D(collision);
        }
    }


}
