
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Text;

namespace CIM_Labyrint
{
    class PlayerBuilder : IBuilder
    {
        private GameObject gameObject;

        private Vector2 gridPosition;

        public Vector2 position;

        public PlayerBuilder(float x, float y)
        {
            this.gridPosition.X = x;
            this.gridPosition.Y = y;

            this.position = GridPlacement.Placement(gridPosition);
        }

        public void BuildGameObject()
        {
            gameObject = new GameObject();

            BuildComponents();

            Animator animator = (Animator)gameObject.GetComponent<Animator>();

            animator.AddAnimation(BuildAnimation("Forward", new string[]
            { "Player/PlayerF_1", "Player/PlayerF_2", "Player/PlayerF_3", "Player/PlayerF_4" }));
            animator.AddAnimation(BuildAnimation("Back", new string[]
            { "Player/PlayerB_1", "Player/PlayerB_2", "Player/PlayerB_3", "Player/PlayerB_4" }));
            animator.AddAnimation(BuildAnimation("Right", new string[]
            { "Player/PlayerR_1", "Player/PlayerR_2", "Player/PlayerR_3", "Player/PlayerR_4" }));
            animator.AddAnimation(BuildAnimation("Left", new string[]
            { "Player/PlayerL_1", "Player/PlayerL_2", "Player/PlayerL_3", "Player/PlayerL_4" }));
            animator.AddAnimation(BuildAnimation("Stay", new string[]
            { "Player/PlayerF_2", "Player/PlayerF_2", "Player/PlayerF_2", "Player/PlayerF_2" }));

            gameObject.Transform.Position = new Vector2(position.X, position.Y);
        }

        private void BuildComponents()
        {
            Player p = (Player)gameObject.AddComponent(new Player());

            gameObject.AddComponent(new Player());
            gameObject.AddComponent(new SpriteRenderer());
            gameObject.AddComponent(new Animator());

            Collider c = (Collider)gameObject.AddComponent(new Collider());
            c.CollisionEvent.Attach(p);
        }

        private Animation BuildAnimation(string animationName, string[] spriteNames)
        {
            Texture2D[] sprites = new Texture2D[spriteNames.Length];

            for (int i = 0; i < sprites.Length; i++)
            {
                sprites[i] = GameWorld.Instance.Content.Load<Texture2D>(spriteNames[i]);
            }

            Animation animation = new Animation(animationName, sprites, 5);

            return animation;
        }

        public GameObject GetResult()
        {
            return gameObject;
        }
    }
}