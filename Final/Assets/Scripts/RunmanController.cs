using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class RunmanController : MonoBehaviour
{
    public Text readyText;
    public Text leftTimeText;
    public GameObject gameOverContent;
    public Text gameResult;
    public Button retryBtn;
    public Button nextBtn;

    public State currentState;
    private Animator _animator;
    private static readonly int StartBool = Animator.StringToHash("Run");
    private static readonly int JumpTrigger = Animator.StringToHash("Jump");

    private static readonly int SlideTrigger = Animator.StringToHash("Slide");

    private float speed;

    // Start is called before the first frame update
    IEnumerator Start()
    {
        _animator = GetComponent<Animator>();
        currentState = State.Idle;
        speed = GameManager.Instance.Setting.RunSpeed;
        int leftTime = 4;
        while (leftTime > 0)
        {
            yield return new WaitForSeconds(1);
            leftTime--;
            readyText.text = leftTime + "";
        }

        readyText.gameObject.SetActive(false);
        leftTimeText.gameObject.SetActive(true);
        StartRun();
        leftTime = GameManager.Instance.Setting.RunTime;
        leftTimeText.text = leftTime + "";
        while (leftTime > 0)
        {
            yield return new WaitForSeconds(1);
            leftTimeText.text = leftTime + "";
            leftTime--;
        }

        FindObjectOfType<CameraFollow>().enabled = false;
        leftTimeText.gameObject.SetActive(false);
        GameOver(false);
    }

    // Update is called once per frame
    void Update()
    {
        // if (currentState == State.Idle && Input.GetKeyDown(KeyCode.S))
        // {
        //     currentState = State.Running;
        //     _animator.SetBool(StartBool, true);
        // }
        // else
        if (currentState == State.Running &&
            (Input.GetKeyDown(KeyCode.UpArrow) || Input.GetKeyDown(KeyCode.Space) || Input.GetKeyDown(KeyCode.W)))
        {
            _animator.SetTrigger(JumpTrigger);
        }
        else if (currentState == State.Running && (Input.GetKeyDown(KeyCode.S) || Input.GetKeyDown(KeyCode.DownArrow)))
        {
            _animator.SetTrigger(SlideTrigger);
        }
    }


    private void StartRun()
    {
        currentState = State.Running;
        _animator.SetBool(StartBool, true);
        StartCoroutine(Moving());
    }

    private IEnumerator Moving()
    {
        Transform transform = this.transform;
        while (currentState != State.Collision)
        {
            yield return null;
            var position = transform.position;
            position.z += speed * Time.deltaTime;
            transform.position = position;
        }
    }

    public void GameOver(bool isFailed)
    {
        gameOverContent.SetActive(true);
        bool showNext = !isFailed && GameManager.Instance.HasNextLevel();
        nextBtn.gameObject.SetActive(showNext);
        retryBtn.gameObject.SetActive(true);
        if (isFailed)
        {
            StopAllCoroutines();
            var component = gameOverContent.GetComponent<Image>();
            var color = component.color;
            color.a = 1;
            component.color = color;
        }
        else
        {
            GameManager.Instance.IncreaseWinTimes();
        }

        gameResult.text = isFailed ? "Game Lose!" : "Game Win!";
    }

    public void Retry()
    {
        GameManager.Instance.Retry();
    }

    public void NextLevel()
    {
        GameManager.Instance.StartNextLevel();
    }
}

public enum State
{
    Idle,
    Running,
    Slide,
    Jump,
    Collision
}