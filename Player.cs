using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace Asteroids {
    public class Player : GameEntity {

        public Animation playerAnimation;

        public Vector2 Position;

        public bool Active;

        public int Health;

        public const float PlayerSpeed = 0.01f;

        public const double FireSpeed = .25f;

        public double fireCoolDown;

        public const float SIZE = .05f;

        #region Geometry

        public int Width() => playerAnimation.frameWidth;

        public int Height() => playerAnimation.frameHeight;

        public CollisionRect collisionRect;

        public override ICollisionShape CollisionShape {
            get => collisionRect;
        }

        #endregion

        public void Initialize(Animation anim, Vector2 position) {
            playerAnimation = anim;
            Position = position;
            Active = true;
            Health = 100;
            var rect = new RectangleF() {
                X = position.X - (SIZE / 2),
                Y = position.Y - (SIZE / 4),
                Width = SIZE,
                Height = SIZE / 2
            };
            collisionRect = new CollisionRect(rect);
        }


        public override void Update(Game1 game, GameTime ellapsedTime) {
            Position.X += game.controlManager.ScalarPressed(PlayerCommand.MoveLeftRight) * PlayerSpeed;
            Position.Y += game.controlManager.ScalarPressed(PlayerCommand.MoveUpDown) * PlayerSpeed;
            Position.X = MathHelper.Clamp(Position.X, 0, 1);
            Position.Y = MathHelper.Clamp(Position.Y, 0, 1);
            collisionRect.rectangle.X = Position.X - (SIZE / 2);
            collisionRect.rectangle.Y = Position.Y - (SIZE / 4);
            playerAnimation.position = Position;
            playerAnimation.Update(game, ellapsedTime);
            if (fireCoolDown > 0)
                fireCoolDown -= ellapsedTime.ElapsedGameTime.TotalSeconds;
            if (game.controlManager.Pressed(PlayerCommand.Fire) && fireCoolDown <= 0) {
                var bulletPos = new Vector2(Position.X + SIZE/2f, Position.Y-Bullet.SIZE/2);
                var bullet = new Bullet();
                bullet.Initialize(game, bulletPos);
                game.AddEntity(bullet);
                fireCoolDown = FireSpeed;
            }
        }


        public override void Draw(Game1 game, SpriteBatch spriteBatch) {
            playerAnimation.Draw(game, spriteBatch);
        }


        public override void Collision(GameEntity other, Game1 game) {
            //System.Console.WriteLine("Hit: " + other.GetType().Name);
        }
    }
}