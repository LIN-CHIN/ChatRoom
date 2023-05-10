namespace ChatRoom
{
	partial class FrmChatRoom
	{
		/// <summary>
		/// Required designer variable.
		/// </summary>
		private System.ComponentModel.IContainer components = null;

		/// <summary>
		/// Clean up any resources being used.
		/// </summary>
		/// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
		protected override void Dispose( bool disposing )
		{
			if( disposing && ( components != null ) ) {
				components.Dispose();
			}
			base.Dispose( disposing );
		}

		#region Windows Form Designer generated code

		/// <summary>
		/// Required method for Designer support - do not modify
		/// the contents of this method with the code editor.
		/// </summary>
		private void InitializeComponent()
		{
			tbInput =  new TextBox() ;
			rtbMessage =  new RichTextBox() ;
			tableLayoutPanel1 =  new TableLayoutPanel() ;
			panel1 =  new Panel() ;
			button1 =  new Button() ;
			tableLayoutPanel1.SuspendLayout();
			panel1.SuspendLayout();
			SuspendLayout();
			// 
			// tbInput
			// 
			tbInput.Location =  new Point( 3, 3 ) ;
			tbInput.Multiline =  true ;
			tbInput.Name =  "tbInput" ;
			tbInput.Size =  new Size( 796, 48 ) ;
			tbInput.TabIndex =  7 ;
			// 
			// rtbMessage
			// 
			rtbMessage.Dock =  DockStyle.Fill ;
			rtbMessage.Enabled =  false ;
			rtbMessage.Location =  new Point( 3, 3 ) ;
			rtbMessage.Name =  "rtbMessage" ;
			rtbMessage.ReadOnly =  true ;
			rtbMessage.Size =  new Size( 945, 557 ) ;
			rtbMessage.TabIndex =  6 ;
			rtbMessage.Text =  "" ;
			// 
			// tableLayoutPanel1
			// 
			tableLayoutPanel1.ColumnCount =  1 ;
			tableLayoutPanel1.ColumnStyles.Add( new ColumnStyle( SizeType.Percent, 100F ) );
			tableLayoutPanel1.Controls.Add( rtbMessage, 0, 0 );
			tableLayoutPanel1.Controls.Add( panel1, 0, 1 );
			tableLayoutPanel1.Dock =  DockStyle.Fill ;
			tableLayoutPanel1.Location =  new Point( 0, 0 ) ;
			tableLayoutPanel1.Name =  "tableLayoutPanel1" ;
			tableLayoutPanel1.RowCount =  2 ;
			tableLayoutPanel1.RowStyles.Add( new RowStyle( SizeType.Percent, 90F ) );
			tableLayoutPanel1.RowStyles.Add( new RowStyle( SizeType.Percent, 10F ) );
			tableLayoutPanel1.RowStyles.Add( new RowStyle( SizeType.Absolute, 20F ) );
			tableLayoutPanel1.Size =  new Size( 951, 626 ) ;
			tableLayoutPanel1.TabIndex =  9 ;
			// 
			// panel1
			// 
			panel1.Controls.Add( button1 );
			panel1.Controls.Add( tbInput );
			panel1.Dock =  DockStyle.Fill ;
			panel1.Location =  new Point( 3, 566 ) ;
			panel1.Name =  "panel1" ;
			panel1.Size =  new Size( 945, 57 ) ;
			panel1.TabIndex =  8 ;
			// 
			// button1
			// 
			button1.Location =  new Point( 811, 3 ) ;
			button1.Name =  "button1" ;
			button1.Size =  new Size( 125, 48 ) ;
			button1.TabIndex =  8 ;
			button1.Text =  "發送" ;
			button1.UseVisualStyleBackColor =  true ;
			button1.Click +=  btnEnter_Click ;
			// 
			// FrmChatRoom
			// 
			AutoScaleDimensions =  new SizeF( 9F, 19F ) ;
			AutoScaleMode =  AutoScaleMode.Font ;
			ClientSize =  new Size( 951, 626 ) ;
			Controls.Add( tableLayoutPanel1 );
			Name =  "FrmChatRoom" ;
			Text =  "FrmChatRoom" ;
			tableLayoutPanel1.ResumeLayout( false );
			panel1.ResumeLayout( false );
			panel1.PerformLayout();
			ResumeLayout( false );
		}

		#endregion
		private TextBox tbInput;
		private RichTextBox rtbMessage;
		private TableLayoutPanel tableLayoutPanel1;
		private Panel panel1;
		private Button button1;
	}
}