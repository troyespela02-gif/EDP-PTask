namespace EspelaAdrianTroy
{
    partial class Products
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
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
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            listView1 = new ListView();
            listView2 = new ListView();
            label1 = new Label();
            label2 = new Label();
            label3 = new Label();
            button3 = new Button();
            button4 = new Button();
            button2 = new Button();
            button1 = new Button();
            SuspendLayout();
            // 
            // listView1
            // 
            listView1.Location = new Point(35, 90);
            listView1.Name = "listView1";
            listView1.Size = new Size(199, 202);
            listView1.TabIndex = 0;
            listView1.UseCompatibleStateImageBehavior = false;
            listView1.SelectedIndexChanged += listView1_SelectedIndexChanged;
            // 
            // listView2
            // 
            listView2.Location = new Point(313, 90);
            listView2.Name = "listView2";
            listView2.Size = new Size(199, 202);
            listView2.TabIndex = 1;
            listView2.UseCompatibleStateImageBehavior = false;
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Font = new Font("Segoe UI", 27.75F, FontStyle.Bold, GraphicsUnit.Point, 0);
            label1.Location = new Point(168, 9);
            label1.Name = "label1";
            label1.Size = new Size(217, 50);
            label1.TabIndex = 2;
            label1.Text = "PRODUCTS";
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Location = new Point(395, 72);
            label2.Name = "label2";
            label2.Size = new Size(47, 15);
            label2.TabIndex = 3;
            label2.Text = "DRINKS";
            label2.Click += label2_Click;
            // 
            // label3
            // 
            label3.AutoSize = true;
            label3.Location = new Point(97, 72);
            label3.Name = "label3";
            label3.Size = new Size(45, 15);
            label3.TabIndex = 4;
            label3.Text = "FOODS";
            // 
            // button3
            // 
            button3.Location = new Point(313, 298);
            button3.Name = "button3";
            button3.Size = new Size(92, 41);
            button3.TabIndex = 7;
            button3.Text = "ADD";
            button3.UseVisualStyleBackColor = true;
            // 
            // button4
            // 
            button4.Location = new Point(422, 298);
            button4.Name = "button4";
            button4.Size = new Size(90, 41);
            button4.TabIndex = 8;
            button4.Text = "REMOVE";
            button4.UseVisualStyleBackColor = true;
            // 
            // button2
            // 
            button2.Location = new Point(142, 298);
            button2.Name = "button2";
            button2.Size = new Size(92, 41);
            button2.TabIndex = 9;
            button2.Text = "REMOVE";
            button2.UseVisualStyleBackColor = true;
            // 
            // button1
            // 
            button1.Location = new Point(35, 298);
            button1.Name = "button1";
            button1.Size = new Size(92, 41);
            button1.TabIndex = 10;
            button1.Text = "ADD";
            button1.UseVisualStyleBackColor = true;
            // 
            // Products
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(556, 383);
            Controls.Add(button1);
            Controls.Add(button2);
            Controls.Add(button4);
            Controls.Add(button3);
            Controls.Add(label3);
            Controls.Add(label2);
            Controls.Add(label1);
            Controls.Add(listView2);
            Controls.Add(listView1);
            Name = "Products";
            Text = "Products";
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private ListView listView1;
        private ListView listView2;
        private Label label1;
        private Label label2;
        private Label label3;
        private Button button3;
        private Button button4;
        private Button button2;
        private Button button1;
    }
}