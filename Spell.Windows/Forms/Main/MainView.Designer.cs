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
            StatusLabel = new ToolStripStatusLabel();
            MatchText = new Label();
            statusStrip1.SuspendLayout();
            SuspendLayout();
            // 
            // textQuery
            // 
            textQuery.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            textQuery.Location = new Point(10, 9);
            textQuery.Margin = new Padding(3, 2, 3, 2);
            textQuery.Name = "textQuery";
            textQuery.Size = new Size(832, 23);
            textQuery.TabIndex = 1;
            // 
            // listResult
            // 
            listResult.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            listResult.FullRowSelect = true;
            listResult.Location = new Point(10, 51);
            listResult.Margin = new Padding(3, 2, 3, 2);
            listResult.Name = "listResult";
            listResult.Size = new Size(832, 437);
            listResult.TabIndex = 2;
            listResult.UseCompatibleStateImageBehavior = false;
            listResult.View = View.Details;
            // 
            // statusStrip1
            // 
            statusStrip1.ImageScalingSize = new Size(20, 20);
            statusStrip1.Items.AddRange(new ToolStripItem[] { StatusLabel });
            statusStrip1.Location = new Point(0, 490);
            statusStrip1.Name = "statusStrip1";
            statusStrip1.Padding = new Padding(1, 0, 12, 0);
            statusStrip1.Size = new Size(853, 22);
            statusStrip1.TabIndex = 3;
            statusStrip1.Text = "statusStrip1";
            // 
            // StatusLabel
            // 
            StatusLabel.Name = "StatusLabel";
            StatusLabel.Size = new Size(39, 17);
            StatusLabel.Text = "Status";
            // 
            // MatchText
            // 
            MatchText.AutoSize = true;
            MatchText.Location = new Point(10, 34);
            MatchText.Name = "MatchText";
            MatchText.Size = new Size(58, 15);
            MatchText.TabIndex = 4;
            MatchText.Text = "Unknown";
            // 
            // MainView
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(853, 512);
            Controls.Add(MatchText);
            Controls.Add(statusStrip1);
            Controls.Add(listResult);
            Controls.Add(textQuery);
            Margin = new Padding(3, 2, 3, 2);
            Name = "MainView";
            Text = "Spell";
            statusStrip1.ResumeLayout(false);
            statusStrip1.PerformLayout();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private TextBox textQuery;
        private ListView listResult;
        private StatusStrip statusStrip1;
        private ToolStripStatusLabel StatusLabel;
        private Label MatchText;
    }
}