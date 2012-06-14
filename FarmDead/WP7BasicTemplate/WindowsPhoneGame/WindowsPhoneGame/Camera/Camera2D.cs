using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace WindowsPhoneGame.Camera
{
    class Camera2D
    {
        Vector2 cameraPosition;
        Matrix transformation;
        


        public Vector2 CameraPosition {
            get { return cameraPosition; }
            set { cameraPosition = value; }
        }




        public Camera2D(Vector2 CameraPosition) {
            this.cameraPosition = CameraPosition;
        }


        public void Update() { 
        
        
        }

        public void Draw() { }

    }
}
