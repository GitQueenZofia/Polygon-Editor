namespace gk1
{
    partial class Form1
    {
        /// <summary>
        ///  Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        ///  Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        ///  Required method for Designer support - do not modify
        ///  the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            whiteboard = new PictureBox();
            drawing_button = new Button();
            groupBox_buttons = new GroupBox();
            edge_width_slider = new TrackBar();
            textBox2 = new TextBox();
            draw_symmetric_button = new RadioButton();
            draw_bresenham_button = new RadioButton();
            draw_library_button = new RadioButton();
            slider = new TrackBar();
            offset_button = new Button();
            remove_v_button = new Button();
            remove_h_button = new Button();
            vertical_button = new Button();
            horizontal_button = new Button();
            add_vertex_button = new Button();
            remove_vertex_button = new Button();
            select_button = new Button();
            textBox1 = new TextBox();
            ((System.ComponentModel.ISupportInitialize)whiteboard).BeginInit();
            groupBox_buttons.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)edge_width_slider).BeginInit();
            ((System.ComponentModel.ISupportInitialize)slider).BeginInit();
            SuspendLayout();
            // 
            // whiteboard
            // 
            whiteboard.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            whiteboard.BackColor = SystemColors.ControlLightLight;
            whiteboard.Location = new Point(0, 1);
            whiteboard.Name = "whiteboard";
            whiteboard.Size = new Size(652, 662);
            whiteboard.TabIndex = 0;
            whiteboard.TabStop = false;
            whiteboard.Paint += whiteboard_Paint;
            whiteboard.MouseClick += whiteboard_MouseClick;
            whiteboard.MouseMove += whiteboard_MouseMove;
            whiteboard.MouseUp += whiteboard_MouseUp;
            // 
            // drawing_button
            // 
            drawing_button.AutoSizeMode = AutoSizeMode.GrowAndShrink;
            drawing_button.BackColor = Color.HotPink;
            drawing_button.Location = new Point(44, 30);
            drawing_button.Name = "drawing_button";
            drawing_button.Size = new Size(228, 37);
            drawing_button.TabIndex = 1;
            drawing_button.Text = "Draw polygon";
            drawing_button.UseVisualStyleBackColor = false;
            drawing_button.Click += drawing_button_Click;
            // 
            // groupBox_buttons
            // 
            groupBox_buttons.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Right;
            groupBox_buttons.Controls.Add(edge_width_slider);
            groupBox_buttons.Controls.Add(textBox2);
            groupBox_buttons.Controls.Add(draw_symmetric_button);
            groupBox_buttons.Controls.Add(draw_bresenham_button);
            groupBox_buttons.Controls.Add(draw_library_button);
            groupBox_buttons.Controls.Add(slider);
            groupBox_buttons.Controls.Add(offset_button);
            groupBox_buttons.Controls.Add(remove_v_button);
            groupBox_buttons.Controls.Add(remove_h_button);
            groupBox_buttons.Controls.Add(vertical_button);
            groupBox_buttons.Controls.Add(horizontal_button);
            groupBox_buttons.Controls.Add(add_vertex_button);
            groupBox_buttons.Controls.Add(remove_vertex_button);
            groupBox_buttons.Controls.Add(select_button);
            groupBox_buttons.Controls.Add(drawing_button);
            groupBox_buttons.Location = new Point(655, 0);
            groupBox_buttons.Name = "groupBox_buttons";
            groupBox_buttons.Size = new Size(323, 665);
            groupBox_buttons.TabIndex = 4;
            groupBox_buttons.TabStop = false;
            // 
            // edge_width_slider
            // 
            edge_width_slider.AutoSize = false;
            edge_width_slider.BackColor = Color.SandyBrown;
            edge_width_slider.Location = new Point(44, 595);
            edge_width_slider.Maximum = 7;
            edge_width_slider.Minimum = 1;
            edge_width_slider.Name = "edge_width_slider";
            edge_width_slider.Size = new Size(228, 50);
            edge_width_slider.SmallChange = 2;
            edge_width_slider.TabIndex = 17;
            edge_width_slider.TickStyle = TickStyle.Both;
            edge_width_slider.Value = 1;
            edge_width_slider.ValueChanged += edge_width_slider_ValueChanged;
            // 
            // textBox2
            // 
            textBox2.BackColor = Color.Moccasin;
            textBox2.Location = new Point(92, 558);
            textBox2.Name = "textBox2";
            textBox2.Size = new Size(150, 31);
            textBox2.TabIndex = 16;
            textBox2.Text = "Edge width:";
            textBox2.TextAlign = HorizontalAlignment.Center;
            // 
            // draw_symmetric_button
            // 
            draw_symmetric_button.AutoSize = true;
            draw_symmetric_button.Location = new Point(92, 141);
            draw_symmetric_button.Name = "draw_symmetric_button";
            draw_symmetric_button.Size = new Size(120, 29);
            draw_symmetric_button.TabIndex = 15;
            draw_symmetric_button.TabStop = true;
            draw_symmetric_button.Text = "Symmetric";
            draw_symmetric_button.UseVisualStyleBackColor = true;
            // 
            // draw_bresenham_button
            // 
            draw_bresenham_button.AutoSize = true;
            draw_bresenham_button.Location = new Point(92, 108);
            draw_bresenham_button.Name = "draw_bresenham_button";
            draw_bresenham_button.Size = new Size(124, 29);
            draw_bresenham_button.TabIndex = 13;
            draw_bresenham_button.Text = "Bresenham";
            draw_bresenham_button.UseVisualStyleBackColor = true;
            draw_bresenham_button.CheckedChanged += draw_bresenham_button_CheckedChanged;
            // 
            // draw_library_button
            // 
            draw_library_button.AutoSize = true;
            draw_library_button.Checked = true;
            draw_library_button.Location = new Point(92, 73);
            draw_library_button.Name = "draw_library_button";
            draw_library_button.Size = new Size(90, 29);
            draw_library_button.TabIndex = 12;
            draw_library_button.TabStop = true;
            draw_library_button.Text = "Library";
            draw_library_button.UseVisualStyleBackColor = true;
            draw_library_button.CheckedChanged += draw_library_button_CheckedChanged;
            // 
            // slider
            // 
            slider.AutoSize = false;
            slider.BackColor = Color.LightSkyBlue;
            slider.Location = new Point(44, 310);
            slider.Maximum = 100;
            slider.Name = "slider";
            slider.Size = new Size(228, 54);
            slider.TabIndex = 5;
            slider.TickStyle = TickStyle.Both;
            slider.Value = 20;
            slider.ValueChanged += slider_ValueChanged;
            // 
            // offset_button
            // 
            offset_button.BackColor = Color.MediumPurple;
            offset_button.Location = new Point(44, 270);
            offset_button.Name = "offset_button";
            offset_button.Size = new Size(228, 34);
            offset_button.TabIndex = 11;
            offset_button.Text = "Draw offset";
            offset_button.UseVisualStyleBackColor = false;
            offset_button.Click += offset_button_Click;
            // 
            // remove_v_button
            // 
            remove_v_button.BackColor = Color.FromArgb(192, 255, 192);
            remove_v_button.Location = new Point(157, 445);
            remove_v_button.Name = "remove_v_button";
            remove_v_button.Size = new Size(151, 34);
            remove_v_button.TabIndex = 10;
            remove_v_button.Text = "Remove_V";
            remove_v_button.UseVisualStyleBackColor = false;
            remove_v_button.Click += remove_v_button_Click;
            // 
            // remove_h_button
            // 
            remove_h_button.BackColor = Color.FromArgb(192, 255, 255);
            remove_h_button.Location = new Point(157, 394);
            remove_h_button.Name = "remove_h_button";
            remove_h_button.Size = new Size(151, 34);
            remove_h_button.TabIndex = 9;
            remove_h_button.Text = "Remove H";
            remove_h_button.UseVisualStyleBackColor = false;
            remove_h_button.Click += remove_h_button_Click;
            // 
            // vertical_button
            // 
            vertical_button.BackColor = Color.Lime;
            vertical_button.Location = new Point(0, 445);
            vertical_button.Name = "vertical_button";
            vertical_button.Size = new Size(151, 34);
            vertical_button.TabIndex = 8;
            vertical_button.Text = "Vertical edge";
            vertical_button.UseVisualStyleBackColor = false;
            vertical_button.Click += vertical_button_Click;
            // 
            // horizontal_button
            // 
            horizontal_button.BackColor = Color.Cyan;
            horizontal_button.Location = new Point(0, 394);
            horizontal_button.Name = "horizontal_button";
            horizontal_button.Size = new Size(151, 34);
            horizontal_button.TabIndex = 7;
            horizontal_button.Text = "Horizontal_edge";
            horizontal_button.UseVisualStyleBackColor = false;
            horizontal_button.Click += horizontal_button_Click;
            // 
            // add_vertex_button
            // 
            add_vertex_button.BackColor = Color.Yellow;
            add_vertex_button.Location = new Point(0, 497);
            add_vertex_button.Name = "add_vertex_button";
            add_vertex_button.Size = new Size(151, 34);
            add_vertex_button.TabIndex = 6;
            add_vertex_button.Text = "Add vertex";
            add_vertex_button.UseVisualStyleBackColor = false;
            add_vertex_button.Click += add_vertex_button_Click;
            // 
            // remove_vertex_button
            // 
            remove_vertex_button.BackColor = Color.FromArgb(255, 255, 192);
            remove_vertex_button.Location = new Point(157, 497);
            remove_vertex_button.Name = "remove_vertex_button";
            remove_vertex_button.Size = new Size(151, 34);
            remove_vertex_button.TabIndex = 5;
            remove_vertex_button.Text = "Remove vertex";
            remove_vertex_button.UseVisualStyleBackColor = false;
            remove_vertex_button.Click += remove_vertex_button_Click;
            // 
            // select_button
            // 
            select_button.BackColor = Color.Plum;
            select_button.Location = new Point(44, 202);
            select_button.Name = "select_button";
            select_button.Size = new Size(228, 35);
            select_button.TabIndex = 4;
            select_button.Text = "Select";
            select_button.UseVisualStyleBackColor = false;
            select_button.Click += select_button_Click;
            // 
            // textBox1
            // 
            textBox1.Location = new Point(0, 0);
            textBox1.Name = "textBox1";
            textBox1.Size = new Size(150, 31);
            textBox1.TabIndex = 5;
            // 
            // Form1
            // 
            AutoScaleDimensions = new SizeF(10F, 25F);
            AutoScaleMode = AutoScaleMode.Font;
            AutoSize = true;
            ClientSize = new Size(978, 665);
            Controls.Add(textBox1);
            Controls.Add(groupBox_buttons);
            Controls.Add(whiteboard);
            Name = "Form1";
            StartPosition = FormStartPosition.CenterScreen;
            Text = "PolygonEditor";
            WindowState = FormWindowState.Maximized;
            FormClosing += Form1_FormClosing;
            Load += Form1_Load;
            ((System.ComponentModel.ISupportInitialize)whiteboard).EndInit();
            groupBox_buttons.ResumeLayout(false);
            groupBox_buttons.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)edge_width_slider).EndInit();
            ((System.ComponentModel.ISupportInitialize)slider).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private PictureBox whiteboard;
        private Button drawing_button;
        private Button select_edge_button;
        private Button select_vertex_button;
        private GroupBox groupBox_buttons;
        private Button select_button;
        private Button add_vertex_button;
        private Button remove_vertex_button;
        private Button vertical_button;
        private Button horizontal_button;
        private Button remove_v_button;
        private Button remove_h_button;
        private Button offset_button;
        private TrackBar slider;
        private RadioButton draw_bresenham_button;
        private RadioButton draw_library_button;
        private RadioButton draw_symmetric_button;
        private TrackBar edge_width_slider;
        private TextBox textBox2;
        private TextBox textBox1;
    }
}