namespace Beer
{
    partial class Visualizer
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
            this.backgroundWorker1 = new System.ComponentModel.BackgroundWorker();
            this.tableLayoutPanelGrid = new System.Windows.Forms.TableLayoutPanel();
            this.numericTimesteps = new System.Windows.Forms.NumericUpDown();
            this.numericInterval = new System.Windows.Forms.NumericUpDown();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.buttonStart = new System.Windows.Forms.Button();
            this.buttonLeft = new System.Windows.Forms.Button();
            this.buttonRight = new System.Windows.Forms.Button();
            this.labelCaught = new System.Windows.Forms.Label();
            this.labelAvoided = new System.Windows.Forms.Label();
            this.labelMissed = new System.Windows.Forms.Label();
            this.labelHit = new System.Windows.Forms.Label();
            this.button1 = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.numericTimesteps)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericInterval)).BeginInit();
            this.SuspendLayout();
            // 
            // backgroundWorker1
            // 
            this.backgroundWorker1.WorkerReportsProgress = true;
            this.backgroundWorker1.WorkerSupportsCancellation = true;
            this.backgroundWorker1.DoWork += new System.ComponentModel.DoWorkEventHandler(this.VisualizerDoWork);
            // 
            // tableLayoutPanelGrid
            // 
            this.tableLayoutPanelGrid.BackColor = System.Drawing.SystemColors.Control;
            this.tableLayoutPanelGrid.ColumnCount = 2;
            this.tableLayoutPanelGrid.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanelGrid.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanelGrid.Location = new System.Drawing.Point(12, 12);
            this.tableLayoutPanelGrid.Margin = new System.Windows.Forms.Padding(1);
            this.tableLayoutPanelGrid.Name = "tableLayoutPanelGrid";
            this.tableLayoutPanelGrid.RowCount = 2;
            this.tableLayoutPanelGrid.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 49F));
            this.tableLayoutPanelGrid.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 51F));
            this.tableLayoutPanelGrid.Size = new System.Drawing.Size(822, 323);
            this.tableLayoutPanelGrid.TabIndex = 0;
            // 
            // numericTimesteps
            // 
            this.numericTimesteps.Location = new System.Drawing.Point(107, 350);
            this.numericTimesteps.Maximum = new decimal(new int[] {
            1000,
            0,
            0,
            0});
            this.numericTimesteps.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.numericTimesteps.Name = "numericTimesteps";
            this.numericTimesteps.Size = new System.Drawing.Size(120, 20);
            this.numericTimesteps.TabIndex = 1;
            this.numericTimesteps.Value = new decimal(new int[] {
            600,
            0,
            0,
            0});
            // 
            // numericInterval
            // 
            this.numericInterval.Location = new System.Drawing.Point(107, 376);
            this.numericInterval.Maximum = new decimal(new int[] {
            1000,
            0,
            0,
            0});
            this.numericInterval.Name = "numericInterval";
            this.numericInterval.Size = new System.Drawing.Size(120, 20);
            this.numericInterval.TabIndex = 2;
            this.numericInterval.Value = new decimal(new int[] {
            500,
            0,
            0,
            0});
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(42, 352);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(55, 13);
            this.label1.TabIndex = 3;
            this.label1.Text = "Timesteps";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(42, 378);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(42, 13);
            this.label2.TabIndex = 4;
            this.label2.Text = "Interval";
            // 
            // buttonStart
            // 
            this.buttonStart.Location = new System.Drawing.Point(421, 350);
            this.buttonStart.Name = "buttonStart";
            this.buttonStart.Size = new System.Drawing.Size(75, 23);
            this.buttonStart.TabIndex = 5;
            this.buttonStart.Text = "Start";
            this.buttonStart.UseVisualStyleBackColor = true;
            this.buttonStart.Click += new System.EventHandler(this.buttonStart_Click);
            // 
            // buttonLeft
            // 
            this.buttonLeft.Location = new System.Drawing.Point(377, 416);
            this.buttonLeft.Name = "buttonLeft";
            this.buttonLeft.Size = new System.Drawing.Size(75, 23);
            this.buttonLeft.TabIndex = 6;
            this.buttonLeft.Text = "Left";
            this.buttonLeft.UseVisualStyleBackColor = true;
            this.buttonLeft.Click += new System.EventHandler(this.buttonLeft_Click);
            // 
            // buttonRight
            // 
            this.buttonRight.Location = new System.Drawing.Point(458, 416);
            this.buttonRight.Name = "buttonRight";
            this.buttonRight.Size = new System.Drawing.Size(75, 23);
            this.buttonRight.TabIndex = 7;
            this.buttonRight.Text = "Right";
            this.buttonRight.UseVisualStyleBackColor = true;
            this.buttonRight.Click += new System.EventHandler(this.buttonRight_Click);
            // 
            // labelCaught
            // 
            this.labelCaught.AutoSize = true;
            this.labelCaught.Location = new System.Drawing.Point(55, 426);
            this.labelCaught.Name = "labelCaught";
            this.labelCaught.Size = new System.Drawing.Size(40, 13);
            this.labelCaught.TabIndex = 8;
            this.labelCaught.Text = "caught";
            // 
            // labelAvoided
            // 
            this.labelAvoided.AutoSize = true;
            this.labelAvoided.Location = new System.Drawing.Point(55, 448);
            this.labelAvoided.Name = "labelAvoided";
            this.labelAvoided.Size = new System.Drawing.Size(45, 13);
            this.labelAvoided.TabIndex = 9;
            this.labelAvoided.Text = "avoided";
            // 
            // labelMissed
            // 
            this.labelMissed.AutoSize = true;
            this.labelMissed.Location = new System.Drawing.Point(131, 426);
            this.labelMissed.Name = "labelMissed";
            this.labelMissed.Size = new System.Drawing.Size(39, 13);
            this.labelMissed.TabIndex = 10;
            this.labelMissed.Text = "missed";
            // 
            // labelHit
            // 
            this.labelHit.AutoSize = true;
            this.labelHit.Location = new System.Drawing.Point(131, 448);
            this.labelHit.Name = "labelHit";
            this.labelHit.Size = new System.Drawing.Size(18, 13);
            this.labelHit.TabIndex = 11;
            this.labelHit.Text = "hit";
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(537, 350);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(110, 23);
            this.button1.TabIndex = 12;
            this.button1.Text = "Show Network";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // Visualizer
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(846, 507);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.labelHit);
            this.Controls.Add(this.labelMissed);
            this.Controls.Add(this.labelAvoided);
            this.Controls.Add(this.labelCaught);
            this.Controls.Add(this.buttonRight);
            this.Controls.Add(this.buttonLeft);
            this.Controls.Add(this.buttonStart);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.numericInterval);
            this.Controls.Add(this.numericTimesteps);
            this.Controls.Add(this.tableLayoutPanelGrid);
            this.Name = "Visualizer";
            this.Text = "Visualizer";
            this.Load += new System.EventHandler(this.Visualizer_Load);
            ((System.ComponentModel.ISupportInitialize)(this.numericTimesteps)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericInterval)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.ComponentModel.BackgroundWorker backgroundWorker1;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanelGrid;
        private System.Windows.Forms.NumericUpDown numericTimesteps;
        private System.Windows.Forms.NumericUpDown numericInterval;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Button buttonStart;
        private System.Windows.Forms.Button buttonLeft;
        private System.Windows.Forms.Button buttonRight;
        private System.Windows.Forms.Label labelCaught;
        private System.Windows.Forms.Label labelAvoided;
        private System.Windows.Forms.Label labelMissed;
        private System.Windows.Forms.Label labelHit;
        private System.Windows.Forms.Button button1;
    }
}