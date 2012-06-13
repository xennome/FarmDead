using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.GamerServices;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;


namespace WindowsPhoneGame.Screens.Transitions
{
    /// <summary>
    /// Componente de juego que implementa IUpdateable.
    /// </summary>
    public class LogoScreen : Microsoft.Xna.Framework.GameComponent
    {
        public LogoScreen(Game game)
            : base(game)
        {
            // TODO: genere aquí los componentes secundarios
        }

        /// <summary>
        /// Permite al componente de juego realizar la inicialización necesaria antes de empezar a
        /// ejecutarse. En este punto puede consultar los servicios necesarios y cargar contenido.
        /// </summary>
        public override void Initialize()
        {
            // TODO: agregue aquí su código de inicialización

            base.Initialize();
        }

        /// <summary>
        /// Permite al componente de juego actualizarse.
        /// </summary>
        /// <param name="gameTime">Proporciona una instantánea de los valores de tiempo.</param>
        public override void Update(GameTime gameTime)
        {
            // TODO: agregue aquí su código de actualización

            base.Update(gameTime);
        }
    }
}
