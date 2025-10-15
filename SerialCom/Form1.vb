Imports System
Imports System.Threading
Imports System.IO.Ports
Imports System.ComponentModel


Public Class Form1
    '------------------------------------------------
    Dim myPort As Array
    Delegate Sub SetTextCallback(ByVal [text] As String) 'Added to prevent threading errors during receiveing of data
    Dim str As String
    Dim arr As Array
    Dim trolleypos As Int32
    Dim datlock As Int32
    Dim alightmnt As Int32
    Dim timbang As Single



    Private Sub Form1_Close()
        SerialPort1.Close()
    End Sub

    '------------------------------------------------
    Private Sub Form1_Load(sender As System.Object, e As System.EventArgs) Handles MyBase.Load

        myPort = IO.Ports.SerialPort.GetPortNames()
        ComboBox1.Items.AddRange(myPort)

        'Button2.Enabled = False

    End Sub


    '------------------------------------------------
    Private Sub ComboBox1_Click(sender As System.Object, e As System.EventArgs) Handles ComboBox1.Click
    End Sub
    '------------------------------------------------
    Private Sub Button1_Click(sender As System.Object, e As System.EventArgs) Handles Button1.Click
        SerialPort1.PortName = ComboBox1.Text
        SerialPort1.BaudRate = ComboBox2.Text
        'SerialPort1.Parity = IO.Ports.Parity.Even
        'SerialPort1.DataBits = IO.Ports.SerialData.Chars
        'SerialPort1.StopBits = IO.Ports.StopBits.One
        SerialPort1.Open()
        Button1.Enabled = False
        'Button2.Enabled = True
        Button4.Enabled = True

    End Sub
    '------------------------------------------------


    Private Sub Button4_Click(sender As System.Object, e As System.EventArgs) Handles Button4.Click
        SerialPort1.Close()
        Button1.Enabled = True
        ' Button2.Enabled = False
        Button4.Enabled = False
    End Sub

    Private Sub SerialPort1_DataReceived(sender As System.Object, e As System.IO.Ports.SerialDataReceivedEventArgs) Handles SerialPort1.DataReceived
        ReceivedText(SerialPort1.ReadExisting())
    End Sub

    Private Sub ReceivedText(ByVal [text] As String) 'input from ReadExisting
        If Me.RichTextBox2.InvokeRequired Then
            Dim x As New SetTextCallback(AddressOf ReceivedText)
            Me.Invoke(x, New Object() {(text)})

        Else
            Me.RichTextBox2.Text = [text] 'append text
            Label3.Text = text

            str = text
            timbang = (Convert.ToInt32(str.Substring(2, 2) + str.Substring(0, 2), 16)) / 10
            dataload.Text = timbang
            datahoist.Text = Convert.ToInt32(str.Substring(6, 2) + str.Substring(4, 2), 16)
            dataGantry.Text = (Convert.ToInt32(str.Substring(14, 2) + str.Substring(12, 2) + str.Substring(10, 2) + str.Substring(8, 2), 16)) / 10
            trolleypos = Convert.ToInt32(str.Substring(18, 2) + str.Substring(16, 2), 16)
            datatrolley.Text = trolleypos
            If trolleypos < 500 Then
                trolleyposition.Text = "area parking"
            Else
                trolleyposition.Text = "area CY"

            End If
            datlock = Convert.ToInt32(str.Substring(22, 2) + str.Substring(20, 2), 16)
            If datlock = 1 Then
                datalock.Text = "lock"
            Else
                datalock.Text = "Unlock"
            End If

            'datachc
            '    dataunlock.Text = Convert.ToInt32(str.Substring(46, 2) + str.Substring(44, 2) + str.Substring(42, 2) + str.Substring(40, 2), 16)

            alightmnt = Convert.ToInt32(str.Substring(26, 2) + str.Substring(24, 2), 16)
            If alightmnt = 1 Then
                dataalightmnet.Text = "ON"
            Else
                dataalightmnet.Text = "OFF"
            End If

        End If
    End Sub

    Private Sub Label8_Click(sender As Object, e As EventArgs) Handles Label8.Click

    End Sub

    Private Sub Label1_Click(sender As Object, e As EventArgs) Handles Label1.Click

    End Sub
End Class
