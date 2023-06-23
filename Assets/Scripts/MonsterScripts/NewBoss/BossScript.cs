using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// public abstract class BossPatternState
// {
//     protected BossSc boss;

//     public BossPatternState(BossSc boss)
//     {
//         this.boss = boss;
//     }

//     public abstract void EnterState();
//     public abstract void UpdateState();
//     public abstract void ExitState();
// }

// public class Pattern1State : BossPatternState
// {
//     private float speed = 5f;
//     private Transform player;
//     private bool isChasing = false;
//     private bool isMovingRight = true;

//     public Pattern1State(BossSc boss) : base(boss)
//     {
//     }

//     public override void EnterState()
//     {
//         boss.pattern1 = true;
//         boss.animator.SetTrigger("pattern2");
//         player = GameObject.FindGameObjectWithTag("Player").transform;
//     }

//     public override void UpdateState()
//     {
//         if (player != null && !isChasing)
//         {
//             if (Vector2.Distance(boss.transform.position, player.position) < 20f)
//             {
//                 isChasing = true;
//                 isMovingRight = boss.transform.position.x < player.position.x;
//             }
//         }

//         if (isChasing)
//         {
//             if (isMovingRight)
//             {
//                 boss.transform.Translate(Vector2.right * speed * Time.deltaTime);
//             }
//             else
//             {
//                 boss.transform.Translate(Vector2.left * speed * Time.deltaTime);
//             }
//         }
//     }

//     public override void ExitState()
//     {
//         boss.pattern1 = false;
//     }
// }

// public class Pattern2State : BossPatternState
// {
//     private float speed = 5f;
//     private Transform player;
//     private bool isChasing = false;
//     private bool isMovingRight = true;

//     public Pattern2State(BossSc boss) : base(boss)
//     {
//     }

//     public override void EnterState()
//     {
//         player = GameObject.FindGameObjectWithTag("Player").transform;
//     }

//     public override void UpdateState()
//     {
//         if (player != null)
//         {
//             float distance = Vector2.Distance(boss.transform.position, player.position);

//             if (!isChasing && 10 < distance && distance < 20)
//             {
//                 isChasing = true;
//                 isMovingRight = boss.transform.position.x < player.position.x;
//             }

//             if (isChasing)
//             {
//                 if (isMovingRight)
//                 {
//                     boss.transform.Translate(Vector2.right * speed * Time.deltaTime);
//                 }
//                 else
//                 {
//                     boss.transform.Translate(Vector2.left * speed * Time.deltaTime);
//                 }
//             }

//             if (distance <= 10)
//             {
//                 isChasing = false;

//                 Vector2 playerDirection = player.position - boss.transform.position;

//                 if (playerDirection.x > 0)
//                 {
//                     boss.rightObject2.SetActive(true);
//                     boss.leftObject2.SetActive(false);
//                 }
//                 else
//                 {
//                     boss.leftObject2.SetActive(true);
//                     boss.rightObject2.SetActive(false);
//                 }
//             }
//         }
//     }

//     public override void ExitState()
//     {
//         boss.rightObject2.SetActive(false);
//         boss.leftObject2.SetActive(false);
//     }
// }

// public class BossScript : MonoBehaviour
// {
//     private PlayerController playerController;
//     public int randomIndex;

//     private float speed = 5f;
//     private Transform player;
//     private bool isChasing = false;
//     private bool isMovingRight = true;
//     public bool pattern1 = false;
//     public GameObject rightObject2;
//     public GameObject leftObject2;

//     public GameObject rightObject3;
//     public GameObject leftObject3;
//     public GameObject rightObject4;
//     public GameObject leftObject4;

//     private Animator animator;
//     private BossPatternState currentState; // 현재 패턴 상태

//     void Start()
//     {
//         randomIndex = Random.Range(0, 4);
//         player = GameObject.FindGameObjectWithTag("Player").transform;

//         animator = GetComponent<Animator>();

//         // 초기 상태를 랜덤하게 설정
//         currentState = GetRandomPatternState();
//         currentState.EnterState();
//     }

//     void Update()
//     {
//         currentState.UpdateState();
//     }

//     void OnCollisionEnter2D(Collision2D collision)
//     {
//         if (pattern1)
//         {
//             if (collision.collider.CompareTag("Wall"))
//             {
//                 isChasing = false;
//                 speed = 0f;
//                 StartCoroutine(ResumeChasingAfterDelay(3f));
//                 Debug.Log("wall");
//             }

//             if (collision.gameObject.layer == LayerMask.NameToLayer("Player"))
//             {
//                 Debug.Log("player");
//                 playerController.Hit(DamageType.HP, 1);
//             }
//         }
//     }

//     IEnumerator ResumeChasingAfterDelay(float delay)
//     {
//         yield return new WaitForSeconds(delay);
//         speed = 5f;
//         pattern1 = false;

//         // 상태 변경
//         currentState.ExitState();
//         currentState = GetRandomPatternState();
//         currentState.EnterState();
//     }

//     BossPatternState GetRandomPatternState()
//     {
//         int patternIndex = Random.Range(0, 4);
//         switch (patternIndex)
//         {
//             case 0:
//                 return new Pattern1State(this);
//             case 1:
//                 return new Pattern2State(this);
//             case 2:
//                 return new Pattern3State(this);
//             case 3:
//                 return new Pattern4State(this);
//             default:
//                 return null;
//         }
//     }
// }

// public class BossPattern3State : BossPatternState
// {
//     private Transform player;
//     public GameObject leftObj;
//     public GameObject rightObj;

//     private float moveSpeed = 5f;
//     private float moveDistance = 15f;
//     private bool isMovingLeft = true;
//     private Vector3 initialPosition;

//     public BossPattern3State(BossSc boss) : base(boss)
//     {
//     }

//     public override void EnterState()
//     {
//         player = GameObject.FindGameObjectWithTag("Player").transform;
//         initialPosition = boss.transform.position;
//     }

//     public override void UpdateState()
//     {
//         if (boss.gameObject == leftObj)
//         {
//             LeftObj();
//         }
//         if (boss.gameObject == rightObj)
//         {
//             RightObj();
//         }
//     }

//     public override void ExitState()
//     {
//         boss.rightObject3.SetActive(false);
//         boss.leftObject3.SetActive(false);
//     }

//     void LeftObj()
//     {
//         if (boss.gameObject.activeSelf)
//         {
//             float distance = Vector3.Distance(boss.transform.position, player.position);

//             if (distance <= 22f)
//             {
//                 if (isMovingLeft)
//                 {
//                     boss.transform.Translate(Vector3.left * moveSpeed * Time.deltaTime);

//                     if (boss.transform.position.x - initialPosition.x <= -moveDistance)
//                     {
//                         isMovingLeft = false;
//                     }
//                 }
//                 else
//                 {
//                     boss.transform.Translate(Vector3.right * moveSpeed * Time.deltaTime);

//                     if (boss.transform.position.x - initialPosition.x >= moveDistance / 3)
//                     {
//                         boss.transform.position = player.position;
//                         moveSpeed = 0;
//                         StartCoroutine(MoveAndRestL());
//                     }
//                 }
//             }
//         }
//     }

//     private IEnumerator MoveAndRestL()
//     {
//         yield return new WaitForSeconds(2f);
//         moveSpeed = 5f;
//         isMovingLeft = true;
//     }

//     void RightObj()
//     {
//         if (boss.gameObject.activeSelf)
//         {
//             float distance = Vector3.Distance(boss.transform.position, player.position);

//             if (distance <= 22f)
//             {
//                 if (isMovingLeft)
//                 {
//                     boss.transform.Translate(Vector3.left * moveSpeed * Time.deltaTime);

//                     if (boss.transform.position.x - initialPosition.x <= -moveDistance / 3)
//                     {
//                         boss.transform.position = player.position;
//                         moveSpeed = 0;
//                         StartCoroutine(MoveAndRestR());
//                     }
//                 }
//                 else
//                 {
//                     boss.transform.Translate(Vector3.right * moveSpeed * Time.deltaTime);

//                     if (boss.transform.position.x - initialPosition.x >= moveDistance)
//                     {
//                         isMovingLeft = true;
//                     }
//                 }
//             }
//         }
//     }

//     private IEnumerator MoveAndRestR()
//     {
//         yield return new WaitForSeconds(2f);
//         moveSpeed = 5f;
//         isMovingLeft = false;
//     }
// }

// public class BossPattern4State : BossPatternState
// {
//     private Transform player;
//     public GameObject leftObj;
//     public GameObject rightObj;

//     private float moveSpeed = 5f;
//     private float moveDistance = 20f;
//     private bool isMovingLeft = true;
//     private Vector3 initialPosition;

//     public BossPattern4State(BossSc boss) : base(boss)
//     {
//     }

//     public override void EnterState()
//     {
//         player = GameObject.FindGameObjectWithTag("Player").transform;
//         initialPosition = boss.transform.position;
//     }

//     public override void UpdateState()
//     {
//         if (boss.gameObject == leftObj)
//         {
//             LeftObj();
//         }
//         if (boss.gameObject == rightObj)
//         {
//             RightObj();
//         }
//     }

//     public override void ExitState()
//     {
//         boss.rightObject4.SetActive(false);
//         boss.leftObject4.SetActive(false);
//     }

//     void LeftObj()
//     {
//         if (boss.gameObject.activeSelf)
//         {
//             float distance = Vector3.Distance(boss.transform.position, player.position);

//             if (distance <= 22f)
//             {
//                 if (isMovingLeft)
//                 {
//                     boss.transform.Translate(Vector3.left * moveSpeed * Time.deltaTime);

//                     if (boss.transform.position.x - initialPosition.x <= -moveDistance)
//                     {
//                         isMovingLeft = false;
//                     }
//                 }
//                 else
//                 {
//                     boss.transform.Translate(Vector3.right * moveSpeed * Time.deltaTime);

//                     if (boss.transform.position.x - initialPosition.x >= moveDistance / 3)
//                     {
//                         boss.transform.position = player.position;
//                         moveSpeed = 0;
//                         StartCoroutine(MoveAndRestL());
//                     }
//                 }
//             }
//         }
//     }

//     private IEnumerator MoveAndRestL()
//     {
//         yield return new WaitForSeconds(2f);
//         moveSpeed = 5f;
//         isMovingLeft = true;
//     }

//     void RightObj()
//     {
//         if (boss.gameObject.activeSelf)
//         {
//             float distance = Vector3.Distance(boss.transform.position, player.position);

//             if (distance <= 22f)
//             {
//                 if (isMovingLeft)
//                 {
//                     boss.transform.Translate(Vector3.left * moveSpeed * Time.deltaTime);

//                     if (boss.transform.position.x - initialPosition.x <= -moveDistance / 3)
//                     {
//                         boss.transform.position = player.position;
//                         moveSpeed = 0;
//                         StartCoroutine(MoveAndRestR());
//                     }
//                 }
//                 else
//                 {
//                     boss.transform.Translate(Vector3.right * moveSpeed * Time.deltaTime);

//                     if (boss.transform.position.x - initialPosition.x >= moveDistance)
//                     {
//                         isMovingLeft = true;
//                     }
//                 }
//             }
//         }
//     }

//     private IEnumerator MoveAndRestR()
//     {
//         yield return new WaitForSeconds(2f);
//         moveSpeed = 5f;
//         isMovingLeft = false;
//     }
// }