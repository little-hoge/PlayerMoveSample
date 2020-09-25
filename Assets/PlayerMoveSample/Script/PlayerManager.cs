using UnityEngine;
using UnityEngine.UI;

public class PlayerManager : MonoBehaviour {

    /// <summary> 移動速度 </summary>
    [SerializeField] private float speed = 0.05f;

    /// <summary> ジャンプ力 </summary>
    [SerializeField] private float jumpPower = 3f;

    /// <summary> 体力 </summary>
    [SerializeField] private Slider lifeSlider;

    /// <summary> 連続ジャンプ可能回数 </summary>
    private const short JUMPCOUNT = 2;
    
    /// <summary> ジャンプ回数 </summary>
    [SerializeField] private short jumpCount;

    /// <summary> 重力 </summary>
    [SerializeField] private float GRAVITY = 1;
    
    /// <summary> 重力コンポーネント </summary>
    Rigidbody2D rigidbody2d;

    // Start is called before the first frame update
    void Start() {
        rigidbody2d = GetComponent<Rigidbody2D>();
        lifeSlider = transform.Find("Slider").GetComponent<Slider>();

        // 回転無効
        rigidbody2d.constraints = RigidbodyConstraints2D.FreezeRotation;

    }

    // Update is called once per frame
    void Update() {
        Move();
        Jump();
    }

    // 移動
    void Move() {
        var xSpeed = Input.GetAxis("Horizontal") * speed;

        transform.Translate(xSpeed, 0, 0);
    }

    // ジャンプ
    void Jump() {
        if (Input.GetKeyDown("space") && jumpCount < JUMPCOUNT) {
            rigidbody2d.velocity = new Vector2(0, 0);
            rigidbody2d.AddForce(new Vector3(0, jumpPower * 100.0f, 0));
            jumpCount++;
        }
    }

    // 接触中
    void OnTriggerStay2D(Collider2D obj) {

        if (obj.tag == "damagefloor") lifeSlider.value -= 0.001f;

        jumpCount = 0;
    }


}
