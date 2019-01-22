//##########################################################################
//¡ï¡ï¡ï¡ï¡ï¡ï¡ï           http://www.cnpopsoft.com           ¡ï¡ï¡ï¡ï¡ï¡ï¡ï
//¡ï¡ï          VB & C# source code and articles for free !!!           ¡ï¡ï
//¡ï¡ï¡ï¡ï¡ï¡ï¡ï                Davidwu                       ¡ï¡ï¡ï¡ï¡ï¡ï¡ï
//##########################################################################

using System;
using System.Collections.Generic;
using System.Text;
using System.Drawing;
using System.Drawing.Drawing2D;

namespace CNPOPSOFT.Controls
{
    [Serializable]
    public class CustomRectangle
    {
        public CustomRectangle()
        {
        }

        public CustomRectangle(float x, float y, float width, float height)
        {
            this.x = x;
            this.y = y;
            this.width = width;
            this.height = height;
        }

        #region Properties

        private float x;
        public float X
        {
            get { return x; }
            set { x = value; }
        }

        private float y;
        public float Y
        {
            get { return y; }
            set { y = value; }
        }

        public float Left
        {
            get { return x; }
            set { x = value; }
        }

        public float Top
        {
            get { return y; }
            set { y = value; }
        }

        private float width;
        public float Width
        {
            get { return width; }
            set { width = value; }
        }

        private float height;
        public float Height
        {
            get { return height; }
            set { height = value; }
        }

        public float Right
        {
            get { return x + width; }
        }

        public float Bottom
        {
            get { return y + height; }
        }

        public Point CenterPoint
        {
            get { return new Point((int)(x + width / 2), (int)(y + height / 2)); }
        }

        #endregion

        #region ·½·¨Çø

        public CustomRectangle Clone()
        {
            return new CustomRectangle(x, y, width, height);
        }

        public Rectangle ToRectangle()
        {
            return new Rectangle((int)x, (int)y, (int)width, (int)height);
        }

        public RectangleF ToRectangleF()
        {
            return new RectangleF(x, y, width, height);
        }

        public Point ToPoint()
        {
            return new Point((int)x, (int)y);
        }

        public PointF ToPointF()
        {
            return new PointF(x, y);
        }

        public bool IsPointInRectangle(int ptX, int ptY)
        {
            return (ptX >= x && ptX <= (x + width) && ptY >= y && ptY <= (y + height));
        }

        public bool IsPointFInRectangle(float ptX, float ptY)
        {
            return (ptX >= x && ptX <= (x + width) && ptY >= y && ptY <= (y + height));
        }

        public static CustomRectangle ToCustomRectangle(RectangleF re)
        {
            CustomRectangle cus = new CustomRectangle();
            cus.X = re.X;
            cus.Y = re.Y;
            cus.Width = re.Width;
            cus.Height = re.Height;
            return cus;
        }

        public static CustomRectangle FromRectangle(Rectangle rect)
        {
            return new CustomRectangle(rect.Left, rect.Top, rect.Width, rect.Height);
        }

        public static CustomRectangle FromRectangleF(RectangleF rect)
        {
            return new CustomRectangle(rect.Left, rect.Top, rect.Width, rect.Height);
        }

        #endregion

    }
}
