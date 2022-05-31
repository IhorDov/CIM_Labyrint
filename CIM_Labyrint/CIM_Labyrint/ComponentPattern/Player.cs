using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Diagnostics.Tracing;
using System.Text;
using System.Threading;

namespace CIM_Labyrint
{
    class Player : Component, IGameListner
    {
        private Animator animator;

        private Dictionary<Keys, BUTTONSTATE> movementKeys = new Dictionary<Keys, BUTTONSTATE>();

        private Thread internalThread;

        private Collider playerCollider;

        private SoundEffect walkSound;
        private SoundEffectInstance soundEffectInstance;


        //public Player()
        //{
        //    internalThread = new Thread(InputHandlerThread);
        //    internalThread.IsBackground = true;
        //    internalThread.Start();
        //}

        //void InputHandlerThread()
        //{
        //    while (true)
        //    {
        //        //time =+ 0.001f;

        //        //if (time >= GameWorld.DeltaTime)
        //        //{
        //        //    time = 0;
        //        //}
        //        InputHandler.Instance.Execute(this);
        //    }
        //}

        public void Move(Vector2 velocity)
        {

            if (velocity != Vector2.Zero)
            {
                velocity.Normalize();
            }

            velocity *= Speed;

            GameObject.Transform.Position += velocity * GameWorld.DeltaTime;

            if (velocity.X > 0)
            {
                animator.PlayAnimation("Right");
                soundEffectInstance.Stop();
                soundEffectInstance.Play();
            }
            else if (velocity.X < 0)
            {
                animator.PlayAnimation("Left");
                soundEffectInstance.Stop();
                soundEffectInstance.Play();
            }
            else if (velocity.Y < 0)
            {
                animator.PlayAnimation("Back");
                soundEffectInstance.Stop();
                soundEffectInstance.Play();
            }
            else if (velocity.Y > 0)
            {
                animator.PlayAnimation("Forward");
                soundEffectInstance.Stop();
                soundEffectInstance.Play();
            }
            else if (velocity.X == 0 && velocity.Y == 0)
            {
                animator.PlayAnimation("Stay");
            }
        }

        public override void Awake()
        {
            this.Speed = 150;

            GameObject.Tag = "Player";


            walkSound = GameWorld.Instance.Content.Load<SoundEffect>("326543__sqeeeek__wetfootsteps");
            soundEffectInstance = walkSound.CreateInstance();
            soundEffectInstance.IsLooped = false;

            soundEffectInstance.Pitch = 0.1f;
            soundEffectInstance.Pan = 0.1f;
            SoundEffect.MasterVolume = 0.3f;


        }

        public override void Start()
        {
            SpriteRenderer sr = GameObject.GetComponent<SpriteRenderer>() as SpriteRenderer;

            sr.SetSprite("Player/PlayerF_2");
            sr.LayerDepth = 0;
            sr.Rotation = 0;

            animator = (Animator)GameObject.GetComponent<Animator>();

            playerCollider = GameObject.GetComponent<Collider>() as Collider;
        }

        public override void Update()
        {
              InputHandler.Instance.Execute(this);
        }

        public void Notify(GameEvent gameEvent)
        {
            ButtonEvent be = (gameEvent as ButtonEvent);

            if (gameEvent is CollisionEvent ce)
            {
                Collider otherCollider = ce.Other.GetComponent<Collider>();

                if (ce.Other.Tag == "Block")
                {
                    if (playerCollider.CollisionBox.Right >= otherCollider.CollisionBox.Right)
                    {
                        GameObject.Transform.Translate(new Vector2(1, 0));
                    }

                    if (playerCollider.CollisionBox.Left <= otherCollider.CollisionBox.Left)
                    {
                        GameObject.Transform.Translate(new Vector2(-1, 0));
                    }

                    if (playerCollider.CollisionBox.Top >= otherCollider.CollisionBox.Top)
                    {
                        GameObject.Transform.Translate(new Vector2(0, 1));
                    }

                    if (playerCollider.CollisionBox.Bottom <= otherCollider.CollisionBox.Bottom)
                    {
                        GameObject.Transform.Translate(new Vector2(0, -1));
                    }
                }                
            }

            if (gameEvent is ButtonEvent)
            {
                movementKeys[be.Key] = be.State;                
            }            
        }
    }
}