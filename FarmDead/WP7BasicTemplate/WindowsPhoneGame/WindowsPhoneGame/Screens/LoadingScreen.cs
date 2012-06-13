#region File Description
//-----------------------------------------------------------------------------
// Javier Cantón Ferrero
// javiercantonferrero@gmail.com
// http://geeks.ms/blogs/jcanton/
// http://xnacommunity.codeplex.com
//
// MVP XNA/DirectX
//-----------------------------------------------------------------------------
#endregion

#region Using Statements
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;
using System.Threading;
using Microsoft.Xna.Framework.Input;
using WindowsPhoneGame.Screens.Transitions;
#endregion

namespace WindowsPhoneGame.Screens
{
    public class LoadingScreen : Screen
    {
        //Fields
        Thread thread;
        TimeSpan time;
        Vector2 position = new Vector2(600, 430);

        #region Properties

        #endregion

        #region Initialize
        public LoadingScreen() : base()
        {
            
        }

        #endregion

        #region Public Methods
        public override void LoadContent()
        {

        }

        public override void Initialize()
        {
            time = TimeSpan.FromSeconds(2);
            thread = new Thread(new ThreadStart(InitializeScreenManager));
            thread.Start();
        }

        public override void Update()
        {
            time -= GameGlobals.gameTime.ElapsedGameTime;

            if (time < TimeSpan.Zero && thread.ThreadState == ThreadState.Stopped)
                ScreenManager.TransitionTo("MainMenu", "BlackFade");
        }

        public override void Draw()
        {
            spriteBatch.Begin();
            spriteBatch.DrawString(GameGlobals.defaultFont, "Loading...", position, Color.Black);
            spriteBatch.End();
        }

        #endregion

        #region Private Methods
        private void InitializeScreenManager()
        {
            //Add all screens here
            ScreenManager.AddScreen("BlackFade", new Fade(Color.Black));
            ScreenManager.AddScreen("MainMenu", new MainMenuScreen());
            ScreenManager.AddScreen("PlayGame", new GameScreen());
            //...
        }

        #endregion
    }
}
