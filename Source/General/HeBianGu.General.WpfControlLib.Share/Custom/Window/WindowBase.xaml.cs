﻿// Copyright © 2022 By HeBianGu(QQ:908293466) https://github.com/HeBianGu/WPF-ControlBase

using HeBianGu.Base.WpfBase;
using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Effects;

namespace HeBianGu.General.WpfControlLib
{
    public interface IWindowBase
    {
        Action<WindowBase> CloseAnimation { get; set; }
        Action<WindowBase> ShowAnimation { get; set; }

        void BeginClose();
        void RefreshHide();
        void Show();
        void Show(bool value);
        bool? ShowDialog();
    }

    /// <summary>
    /// WindowBase.xaml 的交互逻辑
    /// </summary>
    public abstract partial class WindowBase : Window, IWindowBase
    {
        public static ComponentResourceKey DynamicKey => new ComponentResourceKey(typeof(WindowBase), "S.WindowBase.Dynamic");

        public static ComponentResourceKey DefaultKey => new ComponentResourceKey(typeof(WindowBase), "S.WindowBase.Default");
        public static ComponentResourceKey SingleKey => new ComponentResourceKey(typeof(WindowBase), "S.WindowBase.Single");
        public static ComponentResourceKey AccentKey => new ComponentResourceKey(typeof(WindowBase), "S.WindowBase.Accent");
        public static ComponentResourceKey ClearKey => new ComponentResourceKey(typeof(WindowBase), "S.WindowBase.Clear");


        static WindowBase()
        {
            DefaultStyleKeyProperty.OverrideMetadata(typeof(WindowBase), new FrameworkPropertyMetadata(typeof(WindowBase)));
        }

        #region - 依赖属性 -

        #region 默认Header：窗体字体图标Icon

        public static new readonly DependencyProperty IconProperty =
            DependencyProperty.Register("Icon", typeof(string), typeof(WindowBase), new PropertyMetadata("\ue62e"));

        /// <summary>
        /// 按钮字体图标编码
        /// </summary>
        public new string Icon
        {
            get { return (string)GetValue(IconProperty); }
            set { SetValue(IconProperty, value); }
        }

        #endregion

        #region  默认Header：窗体字体图标大小

        public static readonly DependencyProperty IconSizeProperty =
            DependencyProperty.Register("IconSize", typeof(double), typeof(WindowBase), new PropertyMetadata(20D));

        /// <summary>
        /// 按钮字体图标大小
        /// </summary>
        public double IconSize
        {
            get { return (double)GetValue(IconSizeProperty); }
            set { SetValue(IconSizeProperty, value); }
        }

        #endregion

        #region CaptionHeight 标题栏高度

        public static readonly DependencyProperty CaptionHeightProperty =
            DependencyProperty.Register("CaptionHeight", typeof(double), typeof(WindowBase), new PropertyMetadata(26D));

        /// <summary>
        /// 标题高度
        /// </summary>
        public double CaptionHeight
        {
            get { return (double)GetValue(CaptionHeightProperty); }
            set
            {
                SetValue(CaptionHeightProperty, value);
                //this._WC.CaptionHeight = value;
            }
        }

        #endregion

        #region CaptionBackground 标题栏背景色

        public static readonly DependencyProperty CaptionBackgroundProperty = DependencyProperty.Register(
            "CaptionBackground", typeof(Brush), typeof(WindowBase), new PropertyMetadata(null));

        public Brush CaptionBackground
        {
            get { return (Brush)GetValue(CaptionBackgroundProperty); }
            set { SetValue(CaptionBackgroundProperty, value); }
        }


        public CornerRadius CaptionCornerRadius
        {
            get { return (CornerRadius)GetValue(CaptionCornerRadiusProperty); }
            set { SetValue(CaptionCornerRadiusProperty, value); }
        }

        // Using a DependencyProperty as the backing store for MyProperty.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty CaptionCornerRadiusProperty =
            DependencyProperty.Register("CaptionCornerRadius", typeof(CornerRadius), typeof(WindowBase), new FrameworkPropertyMetadata(default(CornerRadius), (d, e) =>
             {
                 WindowBase control = d as WindowBase;

                 if (control == null) return;

                 if (e.OldValue is CornerRadius o)
                 {

                 }

                 if (e.NewValue is CornerRadius n)
                 {

                 }

             }));


        #endregion

        #region CaptionForeground 标题栏前景景色

        public static readonly DependencyProperty CaptionForegroundProperty = DependencyProperty.Register(
            "CaptionForeground", typeof(Brush), typeof(WindowBase), new PropertyMetadata(null));

        public Brush CaptionForeground
        {
            get { return (Brush)GetValue(CaptionForegroundProperty); }
            set { SetValue(CaptionForegroundProperty, value); }
        }

        #endregion 

        #region Header 标题栏内容模板，以提高默认模板，可自定义

        public static readonly DependencyProperty HeaderProperty = DependencyProperty.Register(
            "Header", typeof(ControlTemplate), typeof(WindowBase), new PropertyMetadata(null));

        public ControlTemplate Header
        {
            get { return (ControlTemplate)GetValue(HeaderProperty); }
            set { SetValue(HeaderProperty, value); }
        }

        #endregion

        #region MaxboxEnable 是否显示最大化按钮

        public static readonly DependencyProperty MaxboxEnableProperty = DependencyProperty.Register(
            "MaxboxEnable", typeof(bool), typeof(WindowBase), new PropertyMetadata(true));

        public bool MaxboxEnable
        {
            get { return (bool)GetValue(MaxboxEnableProperty); }
            set { SetValue(MaxboxEnableProperty, value); }
        }

        #endregion

        #region MinboxEnable 是否显示最小化按钮

        public static readonly DependencyProperty MinboxEnableProperty = DependencyProperty.Register(
            "MinboxEnable", typeof(bool), typeof(WindowBase), new PropertyMetadata(true));

        public bool MinboxEnable
        {
            get { return (bool)GetValue(MinboxEnableProperty); }
            set { SetValue(MinboxEnableProperty, value); }
        }

        #endregion

        #region MinboxEnable 是否显示设置按钮

        //public static readonly DependencyProperty SetboxEnableProperty = DependencyProperty.Register(
        //    " SetboxEnable", typeof(bool), typeof(WindowBase), new PropertyMetadata(true));

        //public bool SetboxEnable
        //{
        //    get { return (bool)GetValue(SetboxEnableProperty); }
        //    set { SetValue(SetboxEnableProperty, value); }
        //}


        public bool IsClose
        {
            get { return (bool)GetValue(IsCloseProperty); }
            set { SetValue(IsCloseProperty, value); }
        }

        // Using a DependencyProperty as the backing store for MyProperty.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty IsCloseProperty =
            DependencyProperty.Register("IsClose", typeof(bool), typeof(WindowBase), new PropertyMetadata(default(bool), (d, e) =>
            {
                WindowBase control = d as WindowBase;

                if (control == null) return;

                bool config = (bool)e.NewValue;

                if (config)
                {
                    control.Close();
                }

            }));


        public bool IsUseDrag
        {
            get { return (bool)GetValue(IsUseDragProperty); }
            set { SetValue(IsUseDragProperty, value); }
        }

        // Using a DependencyProperty as the backing store for MyProperty.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty IsUseDragProperty =
            DependencyProperty.Register("IsUseDrag", typeof(bool), typeof(WindowBase), new PropertyMetadata(default(bool), (d, e) =>
             {
                 WindowBase control = d as WindowBase;

                 if (control == null) return;

                 //bool config = e.NewValue as bool;

             }));


        #endregion

        #region - 窗体内容区域Effect效果 -

        public Effect AdornerDecoratorEffect
        {
            get { return (Effect)GetValue(AdornerDecoratorEffectProperty); }
            set { SetValue(AdornerDecoratorEffectProperty, value); }
        }

        // Using a DependencyProperty as the backing store for MyProperty.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty AdornerDecoratorEffectProperty =
            DependencyProperty.Register("AdornerDecoratorEffect", typeof(Effect), typeof(WindowBase), new PropertyMetadata(default(Effect), (d, e) =>
            {
                WindowBase control = d as WindowBase;

                if (control == null) return;

                Effect config = e.NewValue as Effect;

            }));

        /// <summary> 默认磨砂效果 </summary>
        public BlurEffect DefaultBlurEffect
        {
            get { return (BlurEffect)GetValue(DefaultBlurEffectProperty); }
            set { SetValue(DefaultBlurEffectProperty, value); }
        }

        // Using a DependencyProperty as the backing store for MyProperty.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty DefaultBlurEffectProperty =
            DependencyProperty.Register("DefaultBlurEffect", typeof(BlurEffect), typeof(WindowBase), new PropertyMetadata(new BlurEffect(), (d, e) =>
            {
                MainWindowBase control = d as MainWindowBase;

                if (control == null) return;

                BlurEffect config = e.NewValue as BlurEffect;

            }));
        #endregion

        ///// <summary> 是否启用磨砂效果 </summary>
        //public bool IsUseBlur
        //{
        //    get { return (bool)GetValue(IsUseBlurProperty); }
        //    set { SetValue(IsUseBlurProperty, value); }
        //}

        //// Using a DependencyProperty as the backing store for MyProperty.  This enables animation, styling, binding, etc...
        //public static readonly DependencyProperty IsUseBlurProperty =
        //    DependencyProperty.Register("IsUseBlur", typeof(bool), typeof(WindowBase), new PropertyMetadata(default(bool), (d, e) =>
        //     {
        //         WindowBase control = d as WindowBase;

        //         if (control == null) return;

        //         //bool config = e.NewValue as bool;

        //     }));


        /// <summary> 显示时的动画效果 </summary>
        public Action<WindowBase> ShowAnimation
        {
            get { return (Action<WindowBase>)GetValue(ShowAnimationProperty); }
            set { SetValue(ShowAnimationProperty, value); }
        }

        // Using a DependencyProperty as the backing store for MyProperty.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty ShowAnimationProperty =
            DependencyProperty.Register("ShowAnimation", typeof(Action<WindowBase>), typeof(WindowBase), new PropertyMetadata(default(Action<WindowBase>), (d, e) =>
             {
                 WindowBase control = d as WindowBase;

                 if (control == null) return;

                 Action<WindowBase> config = e.NewValue as Action<WindowBase>;

             }));

        /// <summary> 关闭时的动画效果 </summary>

        public Action<WindowBase> CloseAnimation
        {
            get { return (Action<WindowBase>)GetValue(CloseAnimationProperty); }
            set { SetValue(CloseAnimationProperty, value); }
        }

        // Using a DependencyProperty as the backing store for MyProperty.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty CloseAnimationProperty =
            DependencyProperty.Register("CloseAnimation", typeof(Action<WindowBase>), typeof(WindowBase), new PropertyMetadata(default(Action<WindowBase>), (d, e) =>
             {
                 WindowBase control = d as WindowBase;

                 if (control == null) return;

                 Action<WindowBase> config = e.NewValue as Action<WindowBase>;

             }));

        #endregion

        #region - 绑定命令 -
        public ICommand CloseWindowCommand { get; protected set; }
        public ICommand MaximizeWindowCommand { get; protected set; }
        public ICommand MinimizeWindowCommand { get; protected set; }

        private void CloseCommand_Execute(object sender, ExecutedRoutedEventArgs e)
        {
            this.OnCloseAnimation();
        }

        protected virtual void OnCloseAnimation()
        {
            this.CloseAnimation?.Invoke(this);
        }

        private void MaxCommand_Execute(object sender, ExecutedRoutedEventArgs e)
        {
            this.WindowState = this.WindowState == WindowState.Maximized ? WindowState.Normal : WindowState.Maximized;
            e.Handled = true;
        }

        private void MinCommand_Execute(object sender, ExecutedRoutedEventArgs e)
        {
            this.WindowState = WindowState.Minimized;
            e.Handled = true;

            ////  Do ：当点击任务栏，最小化时发生
            //var engine = DoubleStoryboardEngine.Create(this.Top, this.Top + 200, 0.2, Window.TopProperty.Name);
            //engine.CompletedEvent += (l, k) => this.WindowState = WindowState.Minimized;
            //DoubleStoryboardEngine.Create(1, 0, 0.3, UIElement.OpacityProperty.Name).Start(this);
            //engine.Start(this);
            //e.Handled = true;
        }

        protected override void OnMouseLeftButtonDown(MouseButtonEventArgs e)
        {

            if (e.LeftButton == MouseButtonState.Pressed && this.IsUseDrag)
                this.DragMove();

            base.OnMouseLeftButtonDown(e);
        }

        #endregion

        public double TopTemp { get; set; }

        public double LeftTemp { get; set; }

        public WindowBase()
        {
            this.WindowStartupLocation = WindowStartupLocation.CenterScreen;
            this.MaxHeight = SystemParameters.WorkArea.Height;
            this.CloseWindowCommand = new RoutedUICommand();
            this.MaximizeWindowCommand = new RoutedUICommand();
            this.MinimizeWindowCommand = new RoutedUICommand();

            this.BindCommand(CloseWindowCommand, this.CloseCommand_Execute);
            this.BindCommand(MaximizeWindowCommand, this.MaxCommand_Execute);
            this.BindCommand(MinimizeWindowCommand, this.MinCommand_Execute);

            this.ShowAnimation = l =>
              {
                  this.Show(true);
              };

            this.CloseAnimation = l =>
             {
                 this.Show(false);
             };


            this.Loaded += (l, k) =>
            {
                this.ShowAnimation?.Invoke(this);
            };
        }

        public new bool? ShowDialog()
        {
            return base.ShowDialog();
        }

        public new void Show()
        {
            base.Show();
        }

        public void BeginClose()
        {
            this.CloseAnimation?.Invoke(this);
        }

        public virtual void RefreshHide()
        {

        }

        public virtual void Show(bool value)
        {
            IWindowAnimationService animation = ServiceRegistry.Instance.GetInstance<IWindowAnimationService>();

            if (animation == null)
            {
                if (value)
                {
                    this.Show();
                }
                else
                {
                    this.Close();
                }
            }
            else
            {
                if (value)
                {
                    animation?.ShowAnimation(this);
                }
                else
                {
                    animation?.CloseAnimation(this);
                }
            }

        }
    }
}