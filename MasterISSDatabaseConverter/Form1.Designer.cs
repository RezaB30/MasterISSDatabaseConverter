
namespace MasterISSDatabaseConverter
{
    partial class Form1
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
            this.TableDataFiles = new System.Windows.Forms.OpenFileDialog();
            this.converter_btn = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.process_listbox = new System.Windows.Forms.ListBox();
            this.label3 = new System.Windows.Forms.Label();
            this.completedTables_listbox = new System.Windows.Forms.ListBox();
            this.button1 = new System.Windows.Forms.Button();
            this.backgroundWorker1 = new System.ComponentModel.BackgroundWorker();
            this.backgroundWorker2 = new System.ComponentModel.BackgroundWorker();
            this.subGroup_btn = new System.Windows.Forms.Button();
            this.subGroupBgWorker = new System.ComponentModel.BackgroundWorker();
            this.copyAddress_btn = new System.Windows.Forms.Button();
            this.backgroundWorker3 = new System.ComponentModel.BackgroundWorker();
            this.radiusAuth_btn = new System.Windows.Forms.Button();
            this.backgroundWorker4 = new System.ComponentModel.BackgroundWorker();
            this.progressBar1 = new System.Windows.Forms.ProgressBar();
            this.SuspendLayout();
            // 
            // TableDataFiles
            // 
            this.TableDataFiles.FileName = "TableDataFiles";
            // 
            // converter_btn
            // 
            this.converter_btn.Location = new System.Drawing.Point(251, 8);
            this.converter_btn.Name = "converter_btn";
            this.converter_btn.Size = new System.Drawing.Size(159, 27);
            this.converter_btn.TabIndex = 2;
            this.converter_btn.Text = "Convert";
            this.converter_btn.UseVisualStyleBackColor = true;
            this.converter_btn.Click += new System.EventHandler(this.converter_btn_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(162)));
            this.label1.Location = new System.Drawing.Point(12, 232);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(66, 20);
            this.label1.TabIndex = 4;
            this.label1.Text = "Process";
            // 
            // process_listbox
            // 
            this.process_listbox.BackColor = System.Drawing.Color.Black;
            this.process_listbox.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.process_listbox.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(162)));
            this.process_listbox.ForeColor = System.Drawing.SystemColors.Window;
            this.process_listbox.FormattingEnabled = true;
            this.process_listbox.ItemHeight = 16;
            this.process_listbox.Location = new System.Drawing.Point(0, 255);
            this.process_listbox.Name = "process_listbox";
            this.process_listbox.Size = new System.Drawing.Size(748, 164);
            this.process_listbox.TabIndex = 7;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(162)));
            this.label3.Location = new System.Drawing.Point(589, 10);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(137, 20);
            this.label3.TabIndex = 8;
            this.label3.Text = "Completed Tables";
            // 
            // completedTables_listbox
            // 
            this.completedTables_listbox.BackColor = System.Drawing.Color.Black;
            this.completedTables_listbox.ForeColor = System.Drawing.Color.White;
            this.completedTables_listbox.FormattingEnabled = true;
            this.completedTables_listbox.Location = new System.Drawing.Point(593, 33);
            this.completedTables_listbox.Name = "completedTables_listbox";
            this.completedTables_listbox.Size = new System.Drawing.Size(133, 212);
            this.completedTables_listbox.TabIndex = 9;
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(251, 52);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(159, 27);
            this.button1.TabIndex = 10;
            this.button1.Text = "Load To Database";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // backgroundWorker1
            // 
            this.backgroundWorker1.DoWork += new System.ComponentModel.DoWorkEventHandler(this.backgroundWorker1_DoWork);
            // 
            // backgroundWorker2
            // 
            this.backgroundWorker2.DoWork += new System.ComponentModel.DoWorkEventHandler(this.backgroundWorker2_DoWork);
            // 
            // subGroup_btn
            // 
            this.subGroup_btn.Location = new System.Drawing.Point(12, 8);
            this.subGroup_btn.Name = "subGroup_btn";
            this.subGroup_btn.Size = new System.Drawing.Size(159, 27);
            this.subGroup_btn.TabIndex = 11;
            this.subGroup_btn.Text = "Load Subscription Group";
            this.subGroup_btn.UseVisualStyleBackColor = true;
            this.subGroup_btn.Click += new System.EventHandler(this.subGroup_btn_Click);
            // 
            // subGroupBgWorker
            // 
            this.subGroupBgWorker.DoWork += new System.ComponentModel.DoWorkEventHandler(this.subGroupBgWorker_DoWork);
            // 
            // copyAddress_btn
            // 
            this.copyAddress_btn.Location = new System.Drawing.Point(251, 96);
            this.copyAddress_btn.Name = "copyAddress_btn";
            this.copyAddress_btn.Size = new System.Drawing.Size(159, 27);
            this.copyAddress_btn.TabIndex = 13;
            this.copyAddress_btn.Text = "Copy Addresses";
            this.copyAddress_btn.UseVisualStyleBackColor = true;
            this.copyAddress_btn.Click += new System.EventHandler(this.copyAddress_btn_Click);
            // 
            // backgroundWorker3
            // 
            this.backgroundWorker3.DoWork += new System.ComponentModel.DoWorkEventHandler(this.backgroundWorker3_DoWork);
            // 
            // radiusAuth_btn
            // 
            this.radiusAuth_btn.Location = new System.Drawing.Point(12, 52);
            this.radiusAuth_btn.Name = "radiusAuth_btn";
            this.radiusAuth_btn.Size = new System.Drawing.Size(159, 27);
            this.radiusAuth_btn.TabIndex = 14;
            this.radiusAuth_btn.Text = "Load Radius Authorization";
            this.radiusAuth_btn.UseVisualStyleBackColor = true;
            this.radiusAuth_btn.Click += new System.EventHandler(this.radiusAuth_btn_Click);
            // 
            // backgroundWorker4
            // 
            this.backgroundWorker4.DoWork += new System.ComponentModel.DoWorkEventHandler(this.backgroundWorker4_DoWork);
            // 
            // progressBar1
            // 
            this.progressBar1.Location = new System.Drawing.Point(12, 183);
            this.progressBar1.Name = "progressBar1";
            this.progressBar1.Size = new System.Drawing.Size(575, 32);
            this.progressBar1.TabIndex = 15;
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(748, 419);
            this.Controls.Add(this.progressBar1);
            this.Controls.Add(this.radiusAuth_btn);
            this.Controls.Add(this.copyAddress_btn);
            this.Controls.Add(this.subGroup_btn);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.completedTables_listbox);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.process_listbox);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.converter_btn);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.Name = "Form1";
            this.ShowIcon = false;
            this.Text = "Database Converter";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.OpenFileDialog TableDataFiles;
        private System.Windows.Forms.Button converter_btn;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.ListBox process_listbox;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.ListBox completedTables_listbox;
        private System.Windows.Forms.Button button1;
        private System.ComponentModel.BackgroundWorker backgroundWorker1;
        private System.ComponentModel.BackgroundWorker backgroundWorker2;
        private System.Windows.Forms.Button subGroup_btn;
        private System.ComponentModel.BackgroundWorker subGroupBgWorker;
        private System.Windows.Forms.Button copyAddress_btn;
        private System.ComponentModel.BackgroundWorker backgroundWorker3;
        private System.Windows.Forms.Button radiusAuth_btn;
        private System.ComponentModel.BackgroundWorker backgroundWorker4;
        private System.Windows.Forms.ProgressBar progressBar1;
    }
}

