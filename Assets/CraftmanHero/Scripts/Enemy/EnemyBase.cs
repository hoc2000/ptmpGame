using System.Collections;
using System.Collections.Generic;
using Com.LuisPedroFonseca.ProCamera2D;
using UnityEngine;
using DG.Tweening;
using My.Tool;
using Spine;
using Spine.Unity;
using Spine.Unity.Examples;
using DarkTonic.MasterAudio;

namespace Enemy.Base
{
    public abstract class EnemyBase : MonoBehaviour
    {
        [Header("ENEMYINFORM")]
        [SerializeField] protected EnemyInform enemyInform;
        [Header("SETTUP")]
        [SerializeField] protected bool isBoss;
        [SerializeField] protected Rigidbody2D rig;
        [SerializeField] protected Collider2D coll;
        [SerializeField] protected Animator anim;
        [SerializeField] protected int timeDelayAttack;
        [SerializeField] protected HpBar hpBar;
        [SerializeField] protected Transform fxTranform;
        [SerializeField] protected string nameRagdoll;
        [Header("MOVE")]
        [SerializeField] protected float speedDefault;
        [SerializeField] protected float speedRun;
        [SerializeField] protected Transform pos1;
        [SerializeField] protected Transform pos2;
        protected float speed;

        [Header("ANIMATION")]
        protected SkeletonAnimation skeletonAnimation;
        protected SkeletonMecanim skeletonMecanim;
        //protected SkeletonRagdoll2D ragdoll;
        protected Spine.AnimationState animationState;
        protected Skeleton skeleton;

        [Header("CHECK")]
        [SerializeField] protected float distanceCheckFront;
        [SerializeField] protected float distanceCheckBack;
        [SerializeField] protected LayerMask layerCheck;
        [SerializeField] protected Transform checkPlayerPos;

        [Header("ATTACK")]
        [SerializeField] protected Transform attackPoin;
        [SerializeField] protected Vector2 attackRange;
        [SerializeField] protected float attackDistance;
        protected float xDirection = 1;
        protected bool canExplosion;
        public float timeAtack;
        protected float time;

        protected bool isAttack;
        bool isDeath;
        protected bool isTarget;
        public bool IsDeath
        {
            set { isDeath = value; }
            get { return isDeath; }
        }
        public int CurrentHeath
        {
            set { enemyInform.currentHp = value; }
            get { return enemyInform.currentHp; }
        }
        private void Awake()
        {
            LevelManager.Instance.allEnemyInMap++;
        }
        protected virtual void Start()
        {
            skeletonAnimation = GetComponent<SkeletonAnimation>();
            skeletonMecanim = GetComponent<SkeletonMecanim>();
            //ragdoll = GetComponent<SkeletonRagdoll2D>();
            if (skeletonAnimation != null)
            {
                animationState = skeletonAnimation.AnimationState;
                skeleton = skeletonAnimation.skeleton;
            }
            else if (skeletonMecanim != null)
            {
                skeleton = skeletonMecanim.skeleton;
            }

            if (rig == null)
            {
                rig = gameObject.GetComponent<Rigidbody2D>();
            }

            
            //if (coll == null)
            //{
            //    coll = gameObject.GetComponent<Collider2D>();
            //}
            //if (anim == null)
            //{
            //    anim = gameObject.GetComponent<Animator>();
            //}
            enemyInform.currentHp = enemyInform.maxHp;
            speed = speedDefault;
            SetDataInform();
        }

        void SetDataInform()
        {
            if (isBoss)
            {
                return;
            }else
            {
                
                int atkDame = enemyInform.atkDame;
                int hp = enemyInform.maxHp;
                
                for (int i = 0; i < GameManager.levelSelected; i++)
                {
                    atkDame = atkDame + atkDame * 10 / 100;
                    hp = hp + hp * 10 / 100;
                }
                enemyInform.atkDame = atkDame;// enemyInform.atkDame + (enemyInform.atkDame * 30 / 100) * 30;// (Gamedata.I.CurrentLevel-1);
                enemyInform.maxHp = hp;// enemyInform.maxHp + (enemyInform.maxHp * 30 / 100) * 30;// (Gamedata.I.CurrentLevel - 1);
                enemyInform.currentHp = enemyInform.maxHp;

            }
        }
        public virtual void CheckDirection()
        {
            if (transform.position.x <pos1.position.x && transform.position.x>pos2.position.x)
            {
                CheckDistanceFront();
                //CheckDistanceBack();
            }
            else
            {
                if (transform.position.x >= pos1.position.x)
                {
                    xDirection = -1;
                    transform.localScale = new Vector3(-1f, 1f, 1f);
                }
                if (transform.position.x <= pos2.position.x)
                {
                    xDirection = 1;
                    transform.localScale = new Vector3(1f, 1f, 1f);
                }
            }
        }
        public virtual void Move()
        {
            rig.velocity = new Vector2(xDirection * speed * Time.fixedDeltaTime, rig.velocity.y);
        }
        public virtual void StopMove()
        {
            speed = 0f;
        }
        public virtual void CheckDistanceFront()
        {
            if (Player.Instance.transform.position.x < pos2.position.x || Player.Instance.transform.position.x > pos1.position.x)
            {
                isTarget = false;
                speed = speedDefault;
                return;
            }
            RaycastHit2D hit = Physics2D.Raycast(checkPlayerPos.position, Vector2.right * xDirection, distanceCheckFront, layerCheck, 0f);
            Debug.DrawRay(checkPlayerPos.position, Vector2.right * xDirection* distanceCheckFront, Color.red);

            RaycastHit2D hit1 = Physics2D.Raycast(checkPlayerPos.position, Vector2.left * xDirection, distanceCheckBack, layerCheck, 0f);
            Debug.DrawRay(checkPlayerPos.position, Vector2.left * xDirection * distanceCheckBack, Color.red);

            if (hit || hit1)
            {
                isTarget = true;
                if (!isAttack)
                {
                    speed = speedRun;
                }
                if (transform.position.x > Player.Instance.transform.position.x)
                {
                    xDirection = -1;
                    transform.localScale = new Vector3(-1f, 1f, 1f);
                }
                else
                {
                    if (transform.position.x < Player.Instance.transform.position.x)
                    {
                        xDirection = 1;
                        transform.localScale = new Vector3(1f, 1f, 1f);
                    }
                }
            }
            else
            {
                isTarget = false;
                if (!isAttack)
                {
                    speed = speedDefault;
                }
            }

        }

        public virtual void CheckAttack()
        {
            if (isTarget)
            {
                if (Vector2.Distance(transform.position, Player.Instance.transform.position) <= attackDistance)
                {
                    //time += Time.deltaTime;
                    isAttack = true;
                    speed = 0f;
                    if (time>=timeAtack)
                    {
                        Attack();
                        time = 0;
                    }

                }
                else
                {
                    isAttack = false;
                    
                }
            }
        }

        public virtual void Attack()
        {
            MasterAudio.PlaySound(Constants.AUDIO.ZOMBIEATTACK);
            speed = 0f;

            Collider2D[] hitPlayer = Physics2D.OverlapBoxAll(attackPoin.position, attackRange,0f, layerCheck);
            foreach (Collider2D player in hitPlayer)
            {
                Debug.Log("Player hit + " + player.name + " " + enemyInform.atkDame);
                player.GetComponent<Player>().TakeDamage(enemyInform.atkDame);
                ProCamera2DShake.Instance.Shake(0);
            }
        }
        public virtual void TakeDamage(int dame)
        {
            enemyInform.currentHp -= dame;
            if (hpBar!=null)
            {
                hpBar.gameObject.SetActive(true);
                hpBar.UpdateHpBar(enemyInform.currentHp, enemyInform.maxHp);
            }
            MasterAudio.PlaySound(Constants.AUDIO.HIT);
            var textDame = ContentMgr.Instance.GetItem("TextDame", transform.position, Quaternion.identity);
            textDame.GetComponent<TextDame>().Init(dame);
            ContentMgr.Instance.GetItem(Constants.POOLING.BLOODENEMY, new Vector3(fxTranform.position.x, fxTranform.position.y, fxTranform.position.z - 1), Quaternion.identity);
            ContentMgr.Instance.GetItem(Constants.POOLING.HITFX, fxTranform.position, Quaternion.identity);
            VibrationManager.Instance.VibrateMedium();
            if (enemyInform.currentHp <= 0)
            {
                Death();
            }
        }
        public virtual void Death()
        {
            if (hpBar != null)
            {
                hpBar.gameObject.SetActive(false);
            }
            IsDeath = true;
            LevelManager.Instance.enemiesDie++;
            Invoke("ShowCoinFx", 0.1f);           
            this.PostEvent(EventID.EnemyDeath);
            DeathEffect();
        }
        public virtual void DeathEffect()
        {
            if (nameRagdoll != "")
            {
                Debug.Log(nameRagdoll);
                rig.constraints = RigidbodyConstraints2D.None;
                coll.enabled = false;
                enabled = false;
                float dir = Mathf.Sign(transform.position.x - Player.Instance.transform.position.x);
                var raggdoll = ContentMgr.Instance.GetItem(nameRagdoll,new Vector3(transform.position.x,transform.position.y+0.2f, transform.position.z), Quaternion.identity);
                raggdoll.GetComponent<SkeletonAnimation>().initialFlipX = transform.localScale.x == -1;
                raggdoll.SetActive(true);

            }


            gameObject.SetActive(false);
        }

        string currentAnim;
        float currentAnimSpeed;
        public void SetAnim(string anim, bool loop, float animSpeed = 1)
        {
            if (currentAnim == anim && currentAnimSpeed == animSpeed)
            {
                return;
            }
            currentAnim = anim;
            currentAnimSpeed = animSpeed; 
            animationState.SetAnimation(0, anim, loop).TimeScale = animSpeed;
        }

        void ShowCoinFx()
        {
            int countCoin = 5;
            if (isBoss)
            {
                countCoin = 30;
            }
            for (int i = 0; i < countCoin; i++)
            {
                var coin = ContentMgr.Instance.GetItem("Coin", fxTranform.position, Quaternion.identity);
    
                coin.GetComponent<Rigidbody2D>().velocity += (new Vector2(Random.Range(-0.3f, 0.3f), Random.Range(0.95f, 1.05f)) * 10);
                //yield return new WaitForSeconds(0.02f);
            }
        }

        private void OnDrawGizmos()
        {

            Gizmos.color = Color.red;
            Gizmos.DrawWireCube(attackPoin.position, attackRange);

        }
    }

    [System.Serializable]
    public class EnemyInform
    {
        public int maxHp;
        public int atkDame;
        public float atkSpeed;
        public int currentHp;
    }
}
