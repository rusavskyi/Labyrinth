using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class PlayerBechaviour : MonoBehaviour
{
    public float RunSpeed = 1f;
    public float JumpForce = 1f;
    public float SlideSpeed = 1f;
    public bool IsAlive = true;
    public GameController SceneGameController;
    public Transform SpawnPosition;
    public Transform RightBottomBorder;
    public Transform KunaiSpawnPosition;
    public GameObject KunaiPrefab;
    public Transform[] GroundPoints;
    public Image[] HealthImages;

    private bool IsGrounded = false;
    private bool IsRunning = false;
    private bool IsSliding = false;
    private bool IsJumping = false;
    private bool IsFacingRight = true;
    private bool CanAttack = true;
    private float HorizontalAxis = 0f;
    private float CheckRadius = 0.1f;
    private Transform StandingGround = null;
    private Animator PlayerAnimator;
    private Rigidbody2D PlayerRigidbody;
    private float JumpTime = 0f;
    private float JumpTimeout = 0.3f;
    private int Health = 3;
    private float _kunaiSpeed = -50;

    // Use this for initialization
    void Start()
    {
        PlayerAnimator = GetComponent<Animator>();
        PlayerRigidbody = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        HorizontalAxis = Input.GetAxis("Horizontal");
        if (JumpTime <= 0)
        {
            IsJumping = false;
            PlayerAnimator.SetBool("IsJumping", IsJumping);
        }
        else
        {
            JumpTime -= Time.deltaTime;
        }
        if (IsAlive)
        {
            if (IsGrounded)
            {
                FacingHandler();
                Run();
                if (Input.GetAxisRaw("Jump") != 0f)
                {
                    Jump();
                }
                if (Input.GetAxisRaw("Fire1") != 0f)
                {
                    ThrowKunai();
                }
            }
            else
            {
                transform.position = new Vector3(transform.position.x + RunSpeed * Time.deltaTime * HorizontalAxis,
                    transform.position.y,
                    transform.position.z);
            }
        }

        if (transform.position.y < RightBottomBorder.position.y)
        {
            transform.position = SpawnPosition.position;
            PlayerRigidbody.velocity = Vector2.zero;
        }

        if (!IsAlive)
        {
            StartCoroutine(DeathCoroutine());
        }
    }

    private void ThrowKunai()
    {
        if (CanAttack)
        {
            PlayerAnimator.SetBool("IsAttacking", true);
            CanAttack = false;
            StartCoroutine(ThrowCoroutine());
        }
    }

    private IEnumerator ThrowCoroutine()
    {
        yield return new WaitForSeconds(0.2f);
        PlayerAnimator.SetBool("IsAttacking", false);
        Vector2 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        float AngleRad = Mathf.Atan2(mousePosition.x - transform.position.x, -(mousePosition.y - transform.position.y));
        float AngleDeg = (180 / Mathf.PI) * AngleRad;
        KunaiSpawnPosition.transform.rotation = Quaternion.Euler(0, 0, AngleDeg);
        GameObject tmpKunai = Instantiate(KunaiPrefab, KunaiSpawnPosition.position, KunaiSpawnPosition.rotation);
        tmpKunai.GetComponent<Rigidbody2D>().AddRelativeForce(_kunaiSpeed * Vector2.up, ForceMode2D.Impulse);
        yield return new WaitForEndOfFrame();
        yield return new WaitForSeconds(2f);
        CanAttack = true;
    }

    private void FacingHandler()
    {
        if (HorizontalAxis > 0 && transform.localScale.x < 0 ||
            HorizontalAxis < 0 && transform.localScale.x > 0)
        {
            transform.localScale = new Vector3(transform.localScale.x * -1, transform.localScale.y);
        }
    }

    private void Run()
    {
        if (HorizontalAxis > 0 || HorizontalAxis < 0)
        {
            IsRunning = true;
        }
        else
        {
            IsRunning = false;
        }
        PlayerAnimator.SetBool("IsRunning", IsRunning);
        transform.position = new Vector3(transform.position.x + RunSpeed * Time.deltaTime * HorizontalAxis,
            transform.position.y,
            transform.position.z);
    }

    private void Jump()
    {
        JumpTime = JumpTimeout;
        IsJumping = true;
        IsGrounded = false;
        PlayerAnimator.SetBool("IsJumping", IsJumping);
        PlayerRigidbody.AddForce(new Vector2(HorizontalAxis * JumpForce / 2, JumpForce), ForceMode2D.Impulse);
        PlayerRigidbody.velocity.Normalize();
        StandingGround = null;
        transform.SetParent(StandingGround);
    }

    private void Slide()
    {

    }

    private bool GroundedCheck()
    {
        foreach (Transform groundPoint in GroundPoints)
        {
            Collider2D[] colliders = Physics2D.OverlapCircleAll(groundPoint.position, CheckRadius);
            foreach (Collider2D collider2D in colliders)
            {
                if (collider2D.tag == "Ground" || collider2D.tag == "Enemy")
                {
                    IsGrounded = true;
                    PlayerAnimator.SetBool("IsGrounded", IsGrounded);
                    PlayerAnimator.SetBool("IsJumping", false);
                    return true;
                }
            }
        }
        IsGrounded = false;
        PlayerAnimator.SetBool("IsGrounded", IsGrounded);
        return false;
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        StartCoroutine(CheckCoroutine());
        if (collision.gameObject.tag == "Ground")
        {
            StandingGround = collision.transform;
            transform.SetParent(StandingGround);
        }
        else if (collision.gameObject.tag == "Enemy")
        {
            if (IsAlive)
            {
                Health--;
                HealthImages[Health].enabled = false;
                if (Health <= 0)
                {
                    IsAlive = false;
                    PlayerAnimator.SetBool("IsDead", true);

                }
            }
        }
    }

    IEnumerator CheckCoroutine()
    {
        while (!IsGrounded)
        {
            IsGrounded = GroundedCheck();
            yield return new WaitForSeconds(0.1f);
        }
    }

    IEnumerator DeathCoroutine()
    {
        yield return new WaitForSeconds(2f);
        Time.timeScale = 0;
        SceneGameController.DeathPanel.SetActive(true);

    }
}
