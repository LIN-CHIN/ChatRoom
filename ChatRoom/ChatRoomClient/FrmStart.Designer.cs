namespace ChatRoom
{
	partial class FrmStart
	{
		/// <summary>
		///  Required designer variable.
		/// </summary>
		private System.ComponentModel.IContainer components = null;

		/// <summary>
		///  Clean up any resources being used.
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
		///  Required method for Designer support - do not modify
		///  the contents of this method with the code editor.
		/// </summary>
		private void InitializeComponent()
		{
			btnConnection =  new Button() ;
			unConnection =  new Button() ;
			btnTopic1 =  new Button() ;
			tbUserName =  new TextBox() ;
			tbPwd =  new TextBox() ;
			label1 =  new Label() ;
			label2 =  new Label() ;
			tableLayoutPanel1 =  new TableLayoutPanel() ;
			panel4 =  new Panel() ;
			lbConnectionType =  new Label() ;
			panel3 =  new Panel() ;
			rdoWS =  new RadioButton() ;
			rdoTCP =  new RadioButton() ;
			panel1 =  new Panel() ;
			panel2 =  new Panel() ;
			panel6 =  new Panel() ;
			panel7 =  new Panel() ;
			rtbMessage =  new RichTextBox() ;
			panel8 =  new Panel() ;
			label3 =  new Label() ;
			panel9 =  new Panel() ;
			label4 =  new Label() ;
			tableLayoutPanel1.SuspendLayout();
			panel4.SuspendLayout();
			panel3.SuspendLayout();
			panel1.SuspendLayout();
			panel2.SuspendLayout();
			panel7.SuspendLayout();
			panel8.SuspendLayout();
			panel9.SuspendLayout();
			SuspendLayout();
			// 
			// btnConnection
			// 
			btnConnection.Location =  new Point( 360, 24 ) ;
			btnConnection.Name =  "btnConnection" ;
			btnConnection.Size =  new Size( 102, 54 ) ;
			btnConnection.TabIndex =  4 ;
			btnConnection.Text =  "連線" ;
			btnConnection.UseVisualStyleBackColor =  true ;
			btnConnection.Click +=  btnConnection_Click ;
			// 
			// unConnection
			// 
			unConnection.Location =  new Point( 468, 24 ) ;
			unConnection.Name =  "unConnection" ;
			unConnection.Size =  new Size( 118, 54 ) ;
			unConnection.TabIndex =  5 ;
			unConnection.Text =  "關閉連線" ;
			unConnection.UseVisualStyleBackColor =  true ;
			unConnection.Click +=  btnDisconnection_Click ;
			// 
			// btnTopic1
			// 
			btnTopic1.Location =  new Point( 20, 16 ) ;
			btnTopic1.Name =  "btnTopic1" ;
			btnTopic1.Size =  new Size( 118, 42 ) ;
			btnTopic1.TabIndex =  6 ;
			btnTopic1.Text =  "主題一" ;
			btnTopic1.UseVisualStyleBackColor =  true ;
			btnTopic1.Click +=  btnTopic1_Click ;
			// 
			// tbUserName
			// 
			tbUserName.Location =  new Point( 119, 17 ) ;
			tbUserName.Name =  "tbUserName" ;
			tbUserName.Size =  new Size( 218, 27 ) ;
			tbUserName.TabIndex =  2 ;
			// 
			// tbPwd
			// 
			tbPwd.Location =  new Point( 119, 56 ) ;
			tbPwd.Name =  "tbPwd" ;
			tbPwd.Size =  new Size( 218, 27 ) ;
			tbPwd.TabIndex =  3 ;
			// 
			// label1
			// 
			label1.AutoSize =  true ;
			label1.Location =  new Point( 26, 25 ) ;
			label1.Name =  "label1" ;
			label1.Size =  new Size( 87, 19 ) ;
			label1.TabIndex =  3 ;
			label1.Text =  "使用者帳號:" ;
			// 
			// label2
			// 
			label2.AutoSize =  true ;
			label2.Location =  new Point( 71, 59 ) ;
			label2.Name =  "label2" ;
			label2.Size =  new Size( 42, 19 ) ;
			label2.TabIndex =  4 ;
			label2.Text =  "密碼:" ;
			// 
			// tableLayoutPanel1
			// 
			tableLayoutPanel1.CellBorderStyle =  TableLayoutPanelCellBorderStyle.Single ;
			tableLayoutPanel1.ColumnCount =  3 ;
			tableLayoutPanel1.ColumnStyles.Add( new ColumnStyle( SizeType.Percent, 10F ) );
			tableLayoutPanel1.ColumnStyles.Add( new ColumnStyle( SizeType.Percent, 40F ) );
			tableLayoutPanel1.ColumnStyles.Add( new ColumnStyle( SizeType.Percent, 50F ) );
			tableLayoutPanel1.Controls.Add( panel4, 0, 0 );
			tableLayoutPanel1.Controls.Add( panel3, 1, 0 );
			tableLayoutPanel1.Controls.Add( panel1, 1, 1 );
			tableLayoutPanel1.Controls.Add( panel2, 1, 2 );
			tableLayoutPanel1.Controls.Add( panel6, 0, 3 );
			tableLayoutPanel1.Controls.Add( panel7, 2, 3 );
			tableLayoutPanel1.Controls.Add( panel8, 0, 1 );
			tableLayoutPanel1.Controls.Add( panel9, 0, 2 );
			tableLayoutPanel1.Dock =  DockStyle.Fill ;
			tableLayoutPanel1.Location =  new Point( 0, 0 ) ;
			tableLayoutPanel1.Name =  "tableLayoutPanel1" ;
			tableLayoutPanel1.RowCount =  4 ;
			tableLayoutPanel1.RowStyles.Add( new RowStyle( SizeType.Percent, 13.636364F ) );
			tableLayoutPanel1.RowStyles.Add( new RowStyle( SizeType.Percent, 18.181818F ) );
			tableLayoutPanel1.RowStyles.Add( new RowStyle( SizeType.Percent, 13.636364F ) );
			tableLayoutPanel1.RowStyles.Add( new RowStyle( SizeType.Percent, 54.5454559F ) );
			tableLayoutPanel1.Size =  new Size( 1088, 634 ) ;
			tableLayoutPanel1.TabIndex =  5 ;
			// 
			// panel4
			// 
			panel4.Controls.Add( lbConnectionType );
			panel4.Location =  new Point( 4, 4 ) ;
			panel4.Name =  "panel4" ;
			panel4.Size =  new Size( 102, 79 ) ;
			panel4.TabIndex =  6 ;
			// 
			// lbConnectionType
			// 
			lbConnectionType.AutoSize =  true ;
			lbConnectionType.Location =  new Point( 9, 26 ) ;
			lbConnectionType.Name =  "lbConnectionType" ;
			lbConnectionType.Size =  new Size( 84, 19 ) ;
			lbConnectionType.TabIndex =  0 ;
			lbConnectionType.Text =  "連線方式：" ;
			// 
			// panel3
			// 
			tableLayoutPanel1.SetColumnSpan( panel3, 2 );
			panel3.Controls.Add( rdoWS );
			panel3.Controls.Add( rdoTCP );
			panel3.Dock =  DockStyle.Fill ;
			panel3.Location =  new Point( 110, 1 ) ;
			panel3.Margin =  new Padding( 0 ) ;
			panel3.Name =  "panel3" ;
			panel3.Size =  new Size( 977, 85 ) ;
			panel3.TabIndex =  7 ;
			// 
			// rdoWS
			// 
			rdoWS.AutoSize =  true ;
			rdoWS.Location =  new Point( 89, 29 ) ;
			rdoWS.Name =  "rdoWS" ;
			rdoWS.Size =  new Size( 108, 23 ) ;
			rdoWS.TabIndex =  1 ;
			rdoWS.TabStop =  true ;
			rdoWS.Text =  "WebSocket" ;
			rdoWS.UseVisualStyleBackColor =  true ;
			// 
			// rdoTCP
			// 
			rdoTCP.AutoSize =  true ;
			rdoTCP.Location =  new Point( 26, 29 ) ;
			rdoTCP.Name =  "rdoTCP" ;
			rdoTCP.Size =  new Size( 57, 23 ) ;
			rdoTCP.TabIndex =  0 ;
			rdoTCP.TabStop =  true ;
			rdoTCP.Text =  "TCP" ;
			rdoTCP.UseVisualStyleBackColor =  true ;
			// 
			// panel1
			// 
			tableLayoutPanel1.SetColumnSpan( panel1, 2 );
			panel1.Controls.Add( tbPwd );
			panel1.Controls.Add( btnConnection );
			panel1.Controls.Add( label2 );
			panel1.Controls.Add( label1 );
			panel1.Controls.Add( tbUserName );
			panel1.Controls.Add( unConnection );
			panel1.Dock =  DockStyle.Fill ;
			panel1.Location =  new Point( 110, 87 ) ;
			panel1.Margin =  new Padding( 0 ) ;
			panel1.Name =  "panel1" ;
			panel1.Size =  new Size( 977, 114 ) ;
			panel1.TabIndex =  8 ;
			// 
			// panel2
			// 
			tableLayoutPanel1.SetColumnSpan( panel2, 2 );
			panel2.Controls.Add( btnTopic1 );
			panel2.Dock =  DockStyle.Fill ;
			panel2.Location =  new Point( 110, 202 ) ;
			panel2.Margin =  new Padding( 0 ) ;
			panel2.Name =  "panel2" ;
			panel2.Size =  new Size( 977, 85 ) ;
			panel2.TabIndex =  9 ;
			// 
			// panel6
			// 
			tableLayoutPanel1.SetColumnSpan( panel6, 2 );
			panel6.Dock =  DockStyle.Fill ;
			panel6.Location =  new Point( 4, 291 ) ;
			panel6.Name =  "panel6" ;
			panel6.Size =  new Size( 536, 339 ) ;
			panel6.TabIndex =  10 ;
			// 
			// panel7
			// 
			panel7.Controls.Add( rtbMessage );
			panel7.Dock =  DockStyle.Fill ;
			panel7.Location =  new Point( 547, 291 ) ;
			panel7.Name =  "panel7" ;
			panel7.Size =  new Size( 537, 339 ) ;
			panel7.TabIndex =  11 ;
			// 
			// rtbMessage
			// 
			rtbMessage.Dock =  DockStyle.Fill ;
			rtbMessage.Location =  new Point( 0, 0 ) ;
			rtbMessage.Name =  "rtbMessage" ;
			rtbMessage.ReadOnly =  true ;
			rtbMessage.Size =  new Size( 537, 339 ) ;
			rtbMessage.TabIndex =  2 ;
			rtbMessage.Text =  "" ;
			// 
			// panel8
			// 
			panel8.Controls.Add( label3 );
			panel8.Dock =  DockStyle.Fill ;
			panel8.Location =  new Point( 4, 90 ) ;
			panel8.Name =  "panel8" ;
			panel8.Size =  new Size( 102, 108 ) ;
			panel8.TabIndex =  12 ;
			// 
			// label3
			// 
			label3.AutoSize =  true ;
			label3.Location =  new Point( 9, 47 ) ;
			label3.Name =  "label3" ;
			label3.Size =  new Size( 84, 19 ) ;
			label3.TabIndex =  0 ;
			label3.Text =  "連線帳密：" ;
			// 
			// panel9
			// 
			panel9.Controls.Add( label4 );
			panel9.Dock =  DockStyle.Fill ;
			panel9.Location =  new Point( 4, 205 ) ;
			panel9.Name =  "panel9" ;
			panel9.Size =  new Size( 102, 79 ) ;
			panel9.TabIndex =  13 ;
			// 
			// label4
			// 
			label4.AutoSize =  true ;
			label4.Location =  new Point( 9, 25 ) ;
			label4.Name =  "label4" ;
			label4.Size =  new Size( 54, 19 ) ;
			label4.TabIndex =  0 ;
			label4.Text =  "視窗：" ;
			// 
			// FrmStart
			// 
			AutoScaleDimensions =  new SizeF( 9F, 19F ) ;
			AutoScaleMode =  AutoScaleMode.Font ;
			ClientSize =  new Size( 1088, 634 ) ;
			Controls.Add( tableLayoutPanel1 );
			Name =  "FrmStart" ;
			StartPosition =  FormStartPosition.CenterScreen ;
			Text =  "Form1" ;
			tableLayoutPanel1.ResumeLayout( false );
			panel4.ResumeLayout( false );
			panel4.PerformLayout();
			panel3.ResumeLayout( false );
			panel3.PerformLayout();
			panel1.ResumeLayout( false );
			panel1.PerformLayout();
			panel2.ResumeLayout( false );
			panel7.ResumeLayout( false );
			panel8.ResumeLayout( false );
			panel8.PerformLayout();
			panel9.ResumeLayout( false );
			panel9.PerformLayout();
			ResumeLayout( false );
		}

		#endregion

		private Button btnConnection;
		private Button unConnection;
		private Button btnTopic1;
		private TextBox tbUserName;
		private TextBox tbPwd;
		private Label label1;
		private Label label2;
		private TableLayoutPanel tableLayoutPanel1;
		private RichTextBox rtbMessage;
		private RadioButton rdoWS;
		private RadioButton rdoTCP;
		private Panel panel6;
		private Panel panel7;
		private Label lbConnectionType;
		private Label label3;
		private Label label4;
		private Panel panel4;
		private Panel panel3;
		private Panel panel1;
		private Panel panel2;
		private Panel panel8;
		private Panel panel9;
	}
}