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
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
#endregion

namespace WindowsPhoneGame.Screens
{
    public class GameScreen : Screen
    {
        //Fields


        #region Properties
        #endregion

        #region Initialize
        public GameScreen()
            : base()
        {

        }

        public override void LoadContent()
        {
            
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
        }

        public override void Draw()
        {
            spriteBatch.Begin();

            spriteBatch.DrawString(GameGlobals.defaultFont, "Insert your game here!!!", new Vector2(180, 200), Color.Yellow);
            
            spriteBatch.End();
        }
        #endregion

        #region Private Methods
        #endregion
    }
}
