using System;
using System.Drawing;
using System.Windows.Forms;

namespace SequencerDemo {
    partial class SkinForm : Form {
        private readonly PlayerForm Main;
        public SkinForm(PlayerForm main) {
            InitializeComponent();
            DoubleBuffered = true;
            SetStyles();//减少闪烁
            Main = main;
            ShowInTaskbar = false;
            BackColor = Main.BackColor;
            //统一大小
            Size = Main.Size;
            Main.Resize += Main_Resize;
            Main.Owner = this;//设置控件层的拥有皮肤层
            FormMovableEvent();
            Location = Main.Location;//统一控件层和皮肤层的位置
        }
        
        protected void Main_Resize(object sender, EventArgs e) {
            Size = Main.Size;
        }

        #region 减少闪烁
        private void SetStyles() {
            SetStyle(
                ControlStyles.UserPaint |
                ControlStyles.AllPaintingInWmPaint |
                ControlStyles.OptimizedDoubleBuffer |
                ControlStyles.ResizeRedraw |
                ControlStyles.DoubleBuffer, true);
            //强制分配样式重新应用到控件上
            UpdateStyles();
            base.AutoScaleMode = AutoScaleMode.None;
        }
        #endregion
        #region 窗口移动

        /// <summary>
        /// 窗体移动监听绑定
        /// </summary>
        private void FormMovableEvent() {
            //绘制层窗体移动
            this.LocationChanged += new EventHandler(Frm_LocationChanged);
            //控制层层窗体移动
            Main.LocationChanged += new EventHandler(Frm_LocationChanged);
        }

        /// <summary>
        /// 窗口移动时
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        void Frm_LocationChanged(object sender, EventArgs e) {
            //将调用此事件的窗口保存下
            var frm = sender as Form;
            if (frm == this) {
                Main.Location = new Point(this.Left, this.Top);
            } else {
                Location = new Point(Main.Left, Main.Top);
            }
        }
        #endregion
    }
}
