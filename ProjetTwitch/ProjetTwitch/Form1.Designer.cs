﻿namespace ProjetTwitch
{
    partial class Form1
    {
        /// <summary>
        /// Variable nécessaire au concepteur.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Nettoyage des ressources utilisées.
        /// </summary>
        /// <param name="disposing">true si les ressources managées doivent être supprimées ; sinon, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Code généré par le Concepteur Windows Form

        /// <summary>
        /// Méthode requise pour la prise en charge du concepteur - ne modifiez pas
        /// le contenu de cette méthode avec l'éditeur de code.
        /// </summary>
        private void InitializeComponent()
        {
            this.streamsFolowed = new System.Windows.Forms.Label();
            this.validateButton = new System.Windows.Forms.Button();
            this.nameBox = new System.Windows.Forms.TextBox();
            this.delayBox = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // streamsFolowed
            // 
            this.streamsFolowed.AutoSize = true;
            this.streamsFolowed.Location = new System.Drawing.Point(12, 72);
            this.streamsFolowed.Name = "streamsFolowed";
            this.streamsFolowed.Size = new System.Drawing.Size(87, 13);
            this.streamsFolowed.TabIndex = 0;
            this.streamsFolowed.Text = "No user selected";
            // 
            // validateButton
            // 
            this.validateButton.Location = new System.Drawing.Point(197, 10);
            this.validateButton.Name = "validateButton";
            this.validateButton.Size = new System.Drawing.Size(75, 49);
            this.validateButton.TabIndex = 1;
            this.validateButton.Text = "Start";
            this.validateButton.UseVisualStyleBackColor = true;
            this.validateButton.Click += new System.EventHandler(this.validateButton_Click);
            // 
            // nameBox
            // 
            this.nameBox.Location = new System.Drawing.Point(77, 12);
            this.nameBox.Name = "nameBox";
            this.nameBox.Size = new System.Drawing.Size(114, 20);
            this.nameBox.TabIndex = 2;
            // 
            // delayBox
            // 
            this.delayBox.Location = new System.Drawing.Point(77, 39);
            this.delayBox.Name = "delayBox";
            this.delayBox.Size = new System.Drawing.Size(114, 20);
            this.delayBox.TabIndex = 3;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(12, 15);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(64, 13);
            this.label1.TabIndex = 4;
            this.label1.Text = "User name :";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(12, 42);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(54, 13);
            this.label2.TabIndex = 5;
            this.label2.Text = "Delay (s) :";
            // 
            // Form1
            // 
            this.AcceptButton = this.validateButton;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(284, 295);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.delayBox);
            this.Controls.Add(this.nameBox);
            this.Controls.Add(this.validateButton);
            this.Controls.Add(this.streamsFolowed);
            this.Name = "Form1";
            this.Text = "Twitch Online";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label streamsFolowed;
        private System.Windows.Forms.Button validateButton;
        private System.Windows.Forms.TextBox nameBox;
        private System.Windows.Forms.TextBox delayBox;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
    }
}

