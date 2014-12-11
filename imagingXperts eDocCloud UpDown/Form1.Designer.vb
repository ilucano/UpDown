<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmMain
    Inherits System.Windows.Forms.Form

    'Form overrides dispose to clean up the component list.
    <System.Diagnostics.DebuggerNonUserCode()> _
    Protected Overrides Sub Dispose(ByVal disposing As Boolean)
        Try
            If disposing AndAlso components IsNot Nothing Then
                components.Dispose()
            End If
        Finally
            MyBase.Dispose(disposing)
        End Try
    End Sub

    'Required by the Windows Form Designer
    Private components As System.ComponentModel.IContainer

    'NOTE: The following procedure is required by the Windows Form Designer
    'It can be modified using the Windows Form Designer.  
    'Do not modify it using the code editor.
    <System.Diagnostics.DebuggerStepThrough()> _
    Private Sub InitializeComponent()
        Me.components = New System.ComponentModel.Container()
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(frmMain))
        Me.tView = New System.Windows.Forms.TreeView()
        Me.imgList = New System.Windows.Forms.ImageList(Me.components)
        Me.tabGen = New System.Windows.Forms.TabControl()
        Me.tab1 = New System.Windows.Forms.TabPage()
        Me.ToolStrip1 = New System.Windows.Forms.ToolStrip()
        Me.btnRefreshList = New System.Windows.Forms.ToolStripButton()
        Me.ToolStripSeparator1 = New System.Windows.Forms.ToolStripSeparator()
        Me.btnDown = New System.Windows.Forms.ToolStripButton()
        Me.tab2 = New System.Windows.Forms.TabPage()
        Me.cntMnu = New System.Windows.Forms.ContextMenuStrip(Me.components)
        Me.ToolStripMenuItem1 = New System.Windows.Forms.ToolStripMenuItem()
        Me.txtFolder = New System.Windows.Forms.ToolStripTextBox()
        Me.btnBrowse = New System.Windows.Forms.ToolStripButton()
        Me.fldBrowse = New System.Windows.Forms.FolderBrowserDialog()
        Me.tabGen.SuspendLayout()
        Me.tab1.SuspendLayout()
        Me.ToolStrip1.SuspendLayout()
        Me.cntMnu.SuspendLayout()
        Me.SuspendLayout()
        '
        'tView
        '
        Me.tView.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.tView.FullRowSelect = True
        Me.tView.ImageIndex = 0
        Me.tView.ImageList = Me.imgList
        Me.tView.Location = New System.Drawing.Point(3, 63)
        Me.tView.Name = "tView"
        Me.tView.SelectedImageIndex = 0
        Me.tView.Size = New System.Drawing.Size(650, 386)
        Me.tView.TabIndex = 7
        '
        'imgList
        '
        Me.imgList.ImageStream = CType(resources.GetObject("imgList.ImageStream"), System.Windows.Forms.ImageListStreamer)
        Me.imgList.TransparentColor = System.Drawing.Color.Transparent
        Me.imgList.Images.SetKeyName(0, "1417928810_system-users-32.png")
        Me.imgList.Images.SetKeyName(1, "1417928829_bookmark-new-32.png")
        Me.imgList.Images.SetKeyName(2, "1417928822_system-file-manager-32.png")
        Me.imgList.Images.SetKeyName(3, "1417928838_folder-32.png")
        Me.imgList.Images.SetKeyName(4, "1417928846_format-justify-fill-32.png")
        '
        'tabGen
        '
        Me.tabGen.Controls.Add(Me.tab1)
        Me.tabGen.Controls.Add(Me.tab2)
        Me.tabGen.Dock = System.Windows.Forms.DockStyle.Fill
        Me.tabGen.Location = New System.Drawing.Point(0, 0)
        Me.tabGen.Name = "tabGen"
        Me.tabGen.SelectedIndex = 0
        Me.tabGen.Size = New System.Drawing.Size(664, 475)
        Me.tabGen.TabIndex = 8
        '
        'tab1
        '
        Me.tab1.Controls.Add(Me.ToolStrip1)
        Me.tab1.Controls.Add(Me.tView)
        Me.tab1.Location = New System.Drawing.Point(4, 22)
        Me.tab1.Name = "tab1"
        Me.tab1.Padding = New System.Windows.Forms.Padding(3)
        Me.tab1.Size = New System.Drawing.Size(656, 449)
        Me.tab1.TabIndex = 0
        Me.tab1.Text = "Download Boxes"
        Me.tab1.UseVisualStyleBackColor = True
        '
        'ToolStrip1
        '
        Me.ToolStrip1.ImageScalingSize = New System.Drawing.Size(50, 50)
        Me.ToolStrip1.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.btnRefreshList, Me.ToolStripSeparator1, Me.btnDown, Me.txtFolder, Me.btnBrowse})
        Me.ToolStrip1.Location = New System.Drawing.Point(3, 3)
        Me.ToolStrip1.Name = "ToolStrip1"
        Me.ToolStrip1.Size = New System.Drawing.Size(650, 57)
        Me.ToolStrip1.TabIndex = 7
        Me.ToolStrip1.Text = "tools"
        '
        'btnRefreshList
        '
        Me.btnRefreshList.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image
        Me.btnRefreshList.Image = CType(resources.GetObject("btnRefreshList.Image"), System.Drawing.Image)
        Me.btnRefreshList.ImageTransparentColor = System.Drawing.Color.White
        Me.btnRefreshList.Name = "btnRefreshList"
        Me.btnRefreshList.Size = New System.Drawing.Size(54, 54)
        Me.btnRefreshList.ToolTipText = "Refresh List"
        '
        'ToolStripSeparator1
        '
        Me.ToolStripSeparator1.Name = "ToolStripSeparator1"
        Me.ToolStripSeparator1.Size = New System.Drawing.Size(6, 57)
        '
        'btnDown
        '
        Me.btnDown.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image
        Me.btnDown.Image = CType(resources.GetObject("btnDown.Image"), System.Drawing.Image)
        Me.btnDown.ImageTransparentColor = System.Drawing.Color.White
        Me.btnDown.Name = "btnDown"
        Me.btnDown.Size = New System.Drawing.Size(54, 54)
        Me.btnDown.Text = "Download Box"
        '
        'tab2
        '
        Me.tab2.Location = New System.Drawing.Point(4, 22)
        Me.tab2.Name = "tab2"
        Me.tab2.Padding = New System.Windows.Forms.Padding(3)
        Me.tab2.Size = New System.Drawing.Size(656, 449)
        Me.tab2.TabIndex = 1
        Me.tab2.Text = "Upload Boxes"
        Me.tab2.UseVisualStyleBackColor = True
        '
        'cntMnu
        '
        Me.cntMnu.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.ToolStripMenuItem1})
        Me.cntMnu.Name = "cntMnu"
        Me.cntMnu.Size = New System.Drawing.Size(151, 26)
        '
        'ToolStripMenuItem1
        '
        Me.ToolStripMenuItem1.Image = CType(resources.GetObject("ToolStripMenuItem1.Image"), System.Drawing.Image)
        Me.ToolStripMenuItem1.Name = "ToolStripMenuItem1"
        Me.ToolStripMenuItem1.Size = New System.Drawing.Size(150, 22)
        Me.ToolStripMenuItem1.Text = "Download Box"
        '
        'txtFolder
        '
        Me.txtFolder.Name = "txtFolder"
        Me.txtFolder.Size = New System.Drawing.Size(150, 57)
        '
        'btnBrowse
        '
        Me.btnBrowse.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image
        Me.btnBrowse.Image = CType(resources.GetObject("btnBrowse.Image"), System.Drawing.Image)
        Me.btnBrowse.ImageTransparentColor = System.Drawing.Color.White
        Me.btnBrowse.Name = "btnBrowse"
        Me.btnBrowse.Size = New System.Drawing.Size(54, 54)
        Me.btnBrowse.Text = "ToolStripButton1"
        '
        'frmMain
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(664, 475)
        Me.Controls.Add(Me.tabGen)
        Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
        Me.Name = "frmMain"
        Me.Text = "imagingXperts - eDocCloud Manager"
        Me.tabGen.ResumeLayout(False)
        Me.tab1.ResumeLayout(False)
        Me.tab1.PerformLayout()
        Me.ToolStrip1.ResumeLayout(False)
        Me.ToolStrip1.PerformLayout()
        Me.cntMnu.ResumeLayout(False)
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents tView As System.Windows.Forms.TreeView
    Friend WithEvents imgList As System.Windows.Forms.ImageList
    Friend WithEvents tabGen As System.Windows.Forms.TabControl
    Friend WithEvents tab1 As System.Windows.Forms.TabPage
    Friend WithEvents ToolStrip1 As System.Windows.Forms.ToolStrip
    Friend WithEvents btnRefreshList As System.Windows.Forms.ToolStripButton
    Friend WithEvents ToolStripSeparator1 As System.Windows.Forms.ToolStripSeparator
    Friend WithEvents btnDown As System.Windows.Forms.ToolStripButton
    Friend WithEvents tab2 As System.Windows.Forms.TabPage
    Friend WithEvents cntMnu As System.Windows.Forms.ContextMenuStrip
    Friend WithEvents ToolStripMenuItem1 As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents txtFolder As System.Windows.Forms.ToolStripTextBox
    Friend WithEvents btnBrowse As System.Windows.Forms.ToolStripButton
    Friend WithEvents fldBrowse As System.Windows.Forms.FolderBrowserDialog

End Class
