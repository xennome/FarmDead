using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;

namespace WindowsPhoneGame.Agents
{
    class Hero
    {
        public enum DireccionDisparo
        {
            ARRIBA, ABAJO, IZQUIERDA, DERECHA
        }

        public enum Direcciones
        {
            PARADO = 0,
            ARRIBA = 1,
            IZQUIERDA = 2,
            ABAJO = 3,
            DERECHA = 4,
            MUERTO = 5,
            DISPAROARRIBA = 6,
            DISPAROABAJO = 7,
            DISPARODERECHA = 8,
            DISPAROIZQUIERDA = 9,
            PARADOARRIBA = 10,
            PARADOABAJO = 11,
            PARADODERECHA = 12,
            PARADOIZQUIERDA = 13
        };



            
        protected Texture2D textureImage;
        protected Point frameSize;
        Point currentFrame;
        Point sheetSize;
        int timeSinceLastFrame = 0;
        int millisecondsPerFrame;
        public Vector2 speed;
        public Vector2 position;
        public Vector2 positionFinal;
        public bool caminar = false;

        //Para gestionar animaciones
        Dictionary<String, Animation> mAnimaciones = new Dictionary<string, Animation>();
        public Animation mAnimacion;
        protected bool mAnimFinished;
        protected bool mSpriteCaminando = false;
        protected bool mSpriteParado = false;

        protected DireccionDisparo dDisparo;

        int dirAnterior;



        public Hero(Texture2D textureImage, Vector2 position,Vector2 speed, Point frameSize, Point currentFrame, Point sheetSize, int millisecondsPerFrame) {
            this.textureImage = textureImage;
            this.position = position;
            this.speed = speed;
            this.frameSize = frameSize;
            this.currentFrame = currentFrame;
            this.sheetSize = sheetSize;
            this.millisecondsPerFrame = millisecondsPerFrame;


            AddAnimation("DERECHA", 0, 0, 5, true);
            AddAnimation("IZQUIERDA", 1, 0, 5, true);
            AddAnimation("ABAJO", 2, 0, 5, true);
            AddAnimation("ARRIBA", 3, 0, 5, true);

            AddAnimation("DISPARODERECHA", 4, 0, 3, false);
            AddAnimation("DISPAROIZQUIERDA", 5, 0, 3, false);
            AddAnimation("DISPAROABAJO", 6, 0, 3, false);
            AddAnimation("DISPAROARRIBA", 7, 0, 3, false);

            AddAnimation("PARADODERECHA", 4, 0, 1, true);
            AddAnimation("PARADOIZQUIERDA", 5, 0, 1, true);
            AddAnimation("PARADOABAJO", 6, 0, 1, true);
            AddAnimation("PARADOARRIBA", 7, 0, 1, true);

            AddAnimation("RECARGAR", 8, 0, 7, false);
            AddAnimation("PARADOANIMADO", 9, 0, 5, false);





        }


        public void AddAnimation(string name, int fila, int primerFrame, int numeroFrames, bool loop)
        {
            Animation anim = new Animation();
            anim.fila = fila;
            anim.frameInicial = primerFrame;
            anim.numeroFrames = numeroFrames;
            anim.Loop = loop;
            mAnimaciones[name] = anim;
        }

        public void SetActualAnimation(string name)
        {
            if (name == null)
            {
                mAnimacion = null;
                return;
            }

            mAnimFinished = false;

            Animation anim = mAnimaciones[name];
            if (anim != null && anim != mAnimacion)
            {
                currentFrame.X = anim.frameInicial;
                currentFrame.Y = anim.fila;
                mAnimacion = anim;
                //Calculo esto por si tengo distintos tiempos de animaciones
                millisecondsPerFrame = (int) (1000.0f / mAnimacion.velocidad);
            }
        }


        public void updateAnim(GameTime gameTime) {

            if (mAnimacion != null)
            {
                timeSinceLastFrame += gameTime.ElapsedGameTime.Milliseconds;
                if (timeSinceLastFrame > millisecondsPerFrame)
                {
                    timeSinceLastFrame = 0;
                    ++currentFrame.X;
                    if (currentFrame.X >= mAnimacion.numeroFrames)
                    {
                        if (mAnimacion.Loop)
                        {
                            currentFrame.X = 0;
                        }
                        else
                        {
                            mAnimFinished = true;
                        }
                    }
                }
            }
        }

        public void CaminarA(Vector2 posicion)
        {
            positionFinal = posicion;
            mSpriteCaminando = true;
        }


        public void Parar()
        {
            positionFinal = position;
            mSpriteCaminando = false;
            mSpriteParado = true;
            position = new Vector2();
            mAnimacion = null;
        }




        public virtual void update(GameTime gameTime) {

            if (mSpriteCaminando)
            {
                Vector2 destino = new Vector2((int)positionFinal.X - this.position.X, (int)positionFinal.Y - this.position.Y);
                if (destino.Length() < 4)
                {
                    mSpriteCaminando = false;

                }
                else
                {
                    destino.Normalize();
                    position += destino * 3.0f;
                }
            }



            if (mSpriteCaminando)
            {
                Vector2 direccion = positionFinal - position;
                if (Math.Abs(direccion.X) > Math.Abs(direccion.Y))
                {
                    if (direccion.X > 0)
                    {
                        SetActualAnimation("DERECHA");
                        dDisparo = DireccionDisparo.DERECHA;
                        dirAnterior = 3;
                    }
                    else
                    {
                        SetActualAnimation("IZQUIERDA");
                        dDisparo = DireccionDisparo.IZQUIERDA;
                        dirAnterior = 4;
                    }
                }
                else
                {
                    if (direccion.Y > 0)
                    {
                        SetActualAnimation("ABAJO");
                        dDisparo = DireccionDisparo.ABAJO;
                        dirAnterior = 1;
                    }
                    else
                    {
                        SetActualAnimation("ARRIBA");
                        dDisparo = DireccionDisparo.ARRIBA;
                        dirAnterior = 2;
                    }
                }
            }
            else
            {
                switch (dirAnterior)
                {
                    case 1: SetActualAnimation("PARADOABAJO");
                        break;
                    case 2: SetActualAnimation("PARADOARRIBA");
                        break;
                    case 3: SetActualAnimation("PARADODERECHA");
                        break;
                    case 4: SetActualAnimation("PARADOIZQUIERDA");
                        break;
                }
            }

            updateAnim(gameTime);

        }



        public virtual void Draw(GameTime gameTime, SpriteBatch spriteBatch) {
            spriteBatch.Begin();
            spriteBatch.Draw(textureImage, position, new Rectangle(currentFrame.X * frameSize.X, currentFrame.Y * frameSize.Y, frameSize.X, frameSize.Y), Color.White, 0, Vector2.Zero, 1f, SpriteEffects.None, 0);
            spriteBatch.End();
        }

        public Rectangle collisionRect
        {

            get
            {
                return new Rectangle((int)position.X, (int)position.Y, frameSize.X, frameSize.Y);
            }
        }



    

    }
}
