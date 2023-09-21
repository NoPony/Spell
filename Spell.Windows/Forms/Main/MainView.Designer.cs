namespace Spell.Forms.Main
{
    partial class MainView : Form
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
            textQuery = new TextBox();
            listResult = new ListView();
            statusStrip1 = new StatusStrip();
            SuspendLayout();
            // 
            // textQuery
            // 
            textQuery.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            textQuery.Location = new Point(12, 12);
            textQuery.Name = "textQuery";
            textQuery.Size = new Size(705, 27);
            textQuery.TabIndex = 1;
            // 
            // listResult
            // 
            listResult.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            listResult.FullRowSelect = true;
            listResult.Location = new Point(12, 45);
            listResult.Name = "listResult";
            listResult.Size = new Size(705, 331);
            listResult.TabIndex = 2;
            listResult.UseCompatibleStateImageBehavior = false;
            listResult.View = View.Details;
            // 
            // statusStrip1
            // 
            statusStrip1.ImageScalingSize = new Size(20, 20);
            statusStrip1.Location = new Point(0, 377);
            statusStrip1.Name = "statusStrip1";
            statusStrip1.Size = new Size(729, 24);
            statusStrip1.TabIndex = 3;
            statusStrip1.Text = "statusStrip1";
            // 
            // MainView
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(729, 401);
            Controls.Add(statusStrip1);
            Controls.Add(listResult);
            Controls.Add(textQuery);
            Name = "MainView";
            Text = "Spell";
            FormClosed += Exit;
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private TextBox textQuery;
        private ListView listResult;
        private StatusStrip statusStrip1;
    }
}