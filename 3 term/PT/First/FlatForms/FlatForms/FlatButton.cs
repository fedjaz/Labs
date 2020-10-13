using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace GeneticAlgorithms
{
    class FlatButton : Button
    {
        Color curColor;
        Image curImage;
        public override Color BackColor { get => base.BackColor; set => SetBackColor(value); }
        public Color HoverColor { get; set; }
        public Color ClickColor { get; set; }
        public override Image BackgroundImage { get => base.BackgroundImage; set => SetDefaultImage(value); }
        public Image HoverImage { get; set; }
        public Image ClickImage { get; set; }

        public FlatButton()
        {
            curColor = BackColor;
            curImage = Image;
            Invalidate();
        }

        void SetDefaultImage(Image image)
        {
            base.BackgroundImage = image;
            SetImage(image);
        }

        void SetImage(Image image)
        {
            curImage = image;
            Invalidate();
        }

        void SetBackColor(Color color)
        {
            base.BackColor = color;
            SetColor(color);
        }
        void SetColor(Color color)
        {
            curColor = color;
            Invalidate();
        }
        protected override void OnMouseEnter(EventArgs e)
        {
            base.OnMouseEnter(e);
            SetColor(HoverColor);
            if(HoverImage != null)
            {
                SetImage(HoverImage);
            }
        }

        protected override void OnMouseLeave(EventArgs e)
        {
            base.OnMouseLeave(e);
            SetColor(BackColor);
            SetImage(BackgroundImage);
        }

        protected override void OnMouseDown(MouseEventArgs mevent)
        {
            base.OnMouseDown(mevent);
            SetColor(ClickColor);
            if(ClickImage != null)
            {
                SetImage(ClickImage);
            }
        }

        protected override void OnMouseUp(MouseEventArgs mevent)
        {
            base.OnMouseUp(mevent);
            SetColor(HoverColor);
            if(HoverImage != null)
            {
                SetImage(HoverImage);
            }
        }

        protected override void OnPaint(PaintEventArgs pevent)
        {
            base.OnPaint(pevent);
            pevent.Graphics.FillRectangle(new SolidBrush(curColor), new Rectangle(0, 0, Width, Height));
            if(curImage != null)
            {
                pevent.Graphics.DrawImage(curImage, new Rectangle(0, 0, Width, Height));
            }
            if(Text != "")
            {
                SizeF textSize = pevent.Graphics.MeasureString(Text, Font);
                float y = (Height - textSize.Height) / 2, x = (Width - textSize.Width) / 2;
                pevent.Graphics.DrawString(Text, Font, new SolidBrush(ForeColor), x, y);
            }
            if(!Enabled)
            {
                pevent.Graphics.FillRectangle(new SolidBrush(Color.FromArgb(128, 128, 128, 128)), new Rectangle(0, 0, Width, Height));
            }
        }
    }
}
