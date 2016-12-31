namespace ProjetTwitch
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
            this.streamerState = new System.Windows.Forms.Label();
            this.validateButton = new System.Windows.Forms.Button();
            this.streamerBox = new System.Windows.Forms.TextBox();
            this.SuspendLayout();
            // 
            // streamerState
            // 
            this.streamerState.AutoSize = true;
            this.streamerState.Location = new System.Drawing.Point(12, 35);
            this.streamerState.Name = "streamerState";
            this.streamerState.Size = new System.Drawing.Size(101, 13);
            this.streamerState.TabIndex = 0;
            this.streamerState.Text = "No stramer selected";
            // 
            // validateButton
            // 
            this.validateButton.Location = new System.Drawing.Point(197, 10);
            this.validateButton.Name = "validateButton";
            this.validateButton.Size = new System.Drawing.Size(75, 23);
            this.validateButton.TabIndex = 1;
            this.validateButton.Text = "GO";
            this.validateButton.UseVisualStyleBackColor = true;
            this.validateButton.Click += new System.EventHandler(this.validateButton_Click);
            // 
            // streamerBox
            // 
            this.streamerBox.Location = new System.Drawing.Point(12, 12);
            this.streamerBox.Name = "streamerBox";
            this.streamerBox.Size = new System.Drawing.Size(179, 20);
            this.streamerBox.TabIndex = 2;
            // 
            // Form1
            // 
            this.AcceptButton = this.validateButton;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(284, 261);
            this.Controls.Add(this.streamerBox);
            this.Controls.Add(this.validateButton);
            this.Controls.Add(this.streamerState);
            this.Name = "Form1";
            this.Text = "Form1";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label streamerState;
        private System.Windows.Forms.Button validateButton;
        private System.Windows.Forms.TextBox streamerBox;
    }
}

