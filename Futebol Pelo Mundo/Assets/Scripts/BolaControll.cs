using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BolaControll : MonoBehaviour
{
    [SerializeField] private GameObject setaGO;

    //Ang
    public float zRotate = 0.0f;
    public bool liberaRot = false;
    public bool liberaTiro = false;

    //Força
    private Rigidbody2D bola = null;
    private float force = 0;
    [SerializeField] private Image seta2Img;
    private bool jaChutada = false;

    //Paredes
    private Transform paredeLD, paredeLE;

    //Morte bola anim
    [SerializeField] private GameObject morteBolaAnim = null;

    //Toque
    private Collider2D toqueCol;

    public void Awake()
    {
        setaGO = GameObject.Find("SetaVazia");
        seta2Img = GameObject.Find("SetaCheia").GetComponent<Image>();
        setaGO.GetComponent<Image>().enabled = false;
        seta2Img.GetComponent<Image>().enabled = false;
        paredeLD = GameObject.Find("ParedeLD").GetComponent<Transform>();
        paredeLE = GameObject.Find("ParedeLE").GetComponent<Transform>();
    }

    void Start()
    {
        bola = GetComponent<Rigidbody2D>();  
    }

    // Update is called once per frame
    void Update()
    {
        RotacaoSeta();
        InputRotacao();
        LimitaRotacao();
        PosicionaSeta();

        //Força
        ControlaForca();
        AplicaForca();

        //Parede
        VerificaParede();

        VerificaMovimento();
    }

    void PosicionaSeta()
    {
        setaGO.GetComponent<Image>().rectTransform.position = transform.position;
    }

    void RotacaoSeta()
    {
        setaGO.GetComponent<Image>().rectTransform.eulerAngles = new Vector3(0, 0, zRotate);
    }

    void InputRotacao()
    {
        if (liberaRot)
        {
            float moveY = Input.GetAxis("Mouse Y");

            if (zRotate > 0)
            {
                if (moveY > 0)
                {
                    zRotate -= 2.5f;
                }
            }
            if (zRotate < 90)
            {
                //print("Menor que 0");
                if (moveY < 0)
                {
                    zRotate += 2.5f;
                }
            }
        }
    }

    void LimitaRotacao()
    {
        if (zRotate >= 90)
        {
            zRotate = 90;
        }
        if (zRotate <= 0)
        {
            zRotate = 0;
        }
    }

    void VerificaMovimento()
    {
        if(jaChutada)
        {
            StartCoroutine(Movimentacao());
        }
    }

    IEnumerator Movimentacao()
    {
        yield return new WaitForSeconds(4);
        if(bola.velocity.magnitude < 0.7f)
        {
            Instantiate(morteBolaAnim, transform.position, Quaternion.identity);
            Destroy(this.gameObject);
            GameManager.instance.bolasEmCena -= 1;
            GameManager.instance.chutesBola -= 1;
        }  
    }

    void OnMouseDown()
    {
        if(GameManager.instance.tiro == 0)
        {
            liberaRot = true;
            setaGO.GetComponent<Image>().enabled = true;
            seta2Img.GetComponent<Image>().enabled = true;

            toqueCol = GameObject.Find("toque").GetComponent<Collider2D>();
        }
    }

    void OnMouseUp()
    {
        liberaRot = false;
        setaGO.GetComponent<Image>().enabled = false;
        seta2Img.GetComponent<Image>().enabled = false;
        seta2Img.fillAmount = 0;

        if (GameManager.instance.tiro == 0 && force > 0)
        {
            liberaTiro = true;
            AudioManager.instance.SonsFXToca(1);
            GameManager.instance.tiro = 1;
            jaChutada = true;
            toqueCol.enabled = false;
        }
    }

    //Força

    void AplicaForca()
    {
        float x = force * Mathf.Cos(zRotate * Mathf.Deg2Rad);
        float y = force * Mathf.Sin(zRotate * Mathf.Deg2Rad);

        if (liberaTiro)
        {
            bola.AddForce(new Vector2(x, y));
            liberaTiro = false;
            seta2Img.fillAmount = 0;
        }
    }

    void ControlaForca()
    {
        if (liberaRot)
        {
            float moveX = Input.GetAxis("Mouse X");

            if (moveX < 0)
            {
                seta2Img.fillAmount += 0.8f * Time.deltaTime;
                force = seta2Img.fillAmount * 1200;
            }
            if (moveX > 0)
            {
                seta2Img.fillAmount -= 0.8f * Time.deltaTime;
                force = seta2Img.fillAmount * 1200;
            }
        }
    }

    void BolaDinamica()
    {
        bola.isKinematic = false;
    }

    void VerificaParede()
    {
        if (this.gameObject.transform.position.x > paredeLD.position.x)
        {
            Destroy(this.gameObject);
            GameManager.instance.bolasEmCena -= 1;
            GameManager.instance.chutesBola -= 1;
        }
        if (this.gameObject.transform.position.x < paredeLE.position.x)
        {
            Destroy(this.gameObject);
            GameManager.instance.bolasEmCena -= 1;
            GameManager.instance.chutesBola -= 1;
        }
    }

    public void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Morte"))
        {
            Instantiate(morteBolaAnim, transform.position, Quaternion.identity);
            Destroy(this.gameObject);
            GameManager.instance.bolasEmCena -= 1;
            GameManager.instance.chutesBola -= 1;
        }

        if(collision.gameObject.CompareTag("Win"))
        {
            GameManager.instance.win = true;
            int temp = OndeEstou.instance.fase - 2;
            temp++;
            PlayerPrefs.SetInt("Level" + temp, 1);
        }

    }
}
