#region Using Statements
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Graphics;
using WindowsPhoneGame.Thumbsticks;
using WindowsPhoneGame.Agents;
using Microsoft.Xna.Framework.Input.Touch;
#endregion

namespace WindowsPhoneGame.Screens
{
    public class GameScreen : Screen
    {
        //Fields


        #region Properties
        #endregion

        Texture2D world;
        Texture2D textureHero;
        Texture2D textureThumbsticks;

        TouchCollection toques;

        Hero hero;

        //VirtualThumbsticks virtualThumbsticks;

        Rectangle mCameraPosition;
        Matrix mCameraMatrix = Matrix.Identity;

        #region Initialize
        public GameScreen()
            : base()
        {
            TouchPanel.EnabledGestures = GestureType.FreeDrag | GestureType.Tap;
        }

        public override void LoadContent()
        {
            world = GameGlobals.content.Load<Texture2D>("Textures/World/world1");
            textureHero=GameGlobals.content.Load<Texture2D>("Textures/Sprites/granger");
            hero = new Hero(textureHero, new Vector2(200, 250), new Vector2(1, 1), new Point(128, 128), new Point(0, 0), new Point(8, 4), 100);
            textureThumbsticks = GameGlobals.content.Load<Texture2D>("Textures/GUI/thumbstick");
        }

        #endregion

        #region Public Methods
        public override void Initialize()
        {

        }

        public override void Update()
        {
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed)
                ScreenManager.TransitionTo("MainMenu", "BlackFade");


        Vector2 camera = new Vector2(hero.position.X - (GameGlobals.device.Viewport.Width * 0.5f),
        hero.position.Y - (GameGlobals.device.Viewport.Height * 0.5f));
        Vector2 cameraMax = new Vector2(
                                        world.Width * world.Width - GameGlobals.device.Viewport.Width,
                                        world.Height * world.Height - GameGlobals.device.Viewport.Height);
        camera = Vector2.Clamp(camera, Vector2.Zero, cameraMax);

        mCameraPosition = new Rectangle((int)camera.X, (int)camera.Y, GameGlobals.device.Viewport.Width, GameGlobals.device.Viewport.Height);
        mCameraMatrix = Matrix.CreateTranslation(-mCameraPosition.X, -mCameraPosition.Y, 0);

        WindowsPhoneGame.Thumbsticks.VirtualThumbsticks.Update();

        if (WindowsPhoneGame.Thumbsticks.VirtualThumbsticks.LeftThumbstick.LengthSquared() > 0)
        {
            Vector2 pos = new Vector2(hero.position.X, hero.position.Y);
            pos += WindowsPhoneGame.Thumbsticks.VirtualThumbsticks.LeftThumbstick * 5.0f;
            hero.CaminarA(pos);
        }



            hero.update(GameGlobals.gameTime);
        }

        public override void Draw()
        {
            spriteBatch.Begin();

            spriteBatch.Draw(world, new Rectangle(0, 0, world.Width, world.Height), Color.White);

            spriteBatch.End();

            hero.Draw(GameGlobals.gameTime, spriteBatch);
        }
        #endregion

        #region Private Methods
        #endregion
    }
}
