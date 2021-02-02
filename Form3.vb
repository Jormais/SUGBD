Imports MySql.Data.MySqlClient
Imports System.IO
Imports iTextSharp.text
Imports iTextSharp.text.pdf


Public Class Form3

    Dim adapter As New MySqlDataAdapter
    Dim ruta As String = "Server=directionServer;Database=nameDatabase;Uid=userDatabase ;Pwd=password;Port=porToConect"
    Dim con As New MySqlConnection(ruta)
    Dim datos As DataSet

    Private Sub Label2_Click(sender As Object, e As EventArgs) Handles Label2.Click
        Form1.Show()
        Me.Hide()
    End Sub
    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click


        If idNumberField.Text = "" And NameTextField.Text = "" Then
            MsgBox("La busqueda por nombre y id esta vacia")
            Try
                con.Open()
                'MsgBox("Conectado")
                Dim consulta As String
                consulta = "SELECT * FROM afiliado"
                adapter = New MySqlDataAdapter(consulta, con)
                datos = New DataSet
                adapter.Fill(datos, "afiliado")
                DataGridView1.DataSource = datos
                DataGridView1.DataMember = "afiliado"
                con.Close()
            Catch ex As Exception
                MsgBox(ex.Message)
            End Try
            DataGridView1.Refresh()
        ElseIf NameTextField.Text <> "" Then

            If ComboBox2.Text = "Nombre" Then
                MsgBox("Realizando búsqueda por nombre")
                Dim names As String = NameTextField.Text
                Try
                    con.Open()
                    'MsgBox("Conectado")
                    Dim consulta As String
                    consulta = "SELECT * FROM afiliado WHERE Nombre = '" & names & "';"
                    adapter = New MySqlDataAdapter(consulta, con)
                    datos = New DataSet
                    adapter.Fill(datos, "afiliado")
                    DataGridView1.DataSource = datos
                    DataGridView1.DataMember = "afiliado"

                    con.Close()
                Catch ex As Exception
                    MsgBox(ex.Message)
                End Try
                DataGridView1.Refresh()
            ElseIf ComboBox2.Text = "Apellidos" Then
                MsgBox("Realizando búsqueda por Apellidos")
                Dim names As String = NameTextField.Text
                Try
                    con.Open()
                    'MsgBox("Conectado")
                    Dim consulta As String
                    consulta = "SELECT * FROM afiliado WHERE Apellidos = '" & names & "';"
                    adapter = New MySqlDataAdapter(consulta, con)
                    datos = New DataSet
                    adapter.Fill(datos, "afiliado")
                    DataGridView1.DataSource = datos
                    DataGridView1.DataMember = "afiliado"

                    con.Close()
                Catch ex As Exception
                    MsgBox(ex.Message)
                End Try
                DataGridView1.Refresh()
            End If


        ElseIf idNumberField.Text <> "" Then

            If ComboBox1.Text = "Nº Empleado" Then
                MsgBox("Realizando búsqueda por numero de empleado")
                Dim id As String = idNumberField.Text
                Try
                    con.Open()
                    'MsgBox("Conectado")
                    Dim consulta As String
                    consulta = "SELECT * FROM afiliado WHERE `N Empleado` Like '%" & id & "%';"
                    adapter = New MySqlDataAdapter(consulta, con)
                    datos = New DataSet
                    adapter.Fill(datos, "afiliado")
                    DataGridView1.DataSource = datos
                    DataGridView1.DataMember = "afiliado"
                    con.Close()

                Catch ex As Exception
                    MsgBox(ex.Message)
                End Try
                DataGridView1.Refresh()
            ElseIf ComboBox1.Text = "CP" Then
                MsgBox("Realizando busqueda por número de conductor")
                Dim id As String = idNumberField.Text
                Try
                    con.Open()
                    'MsgBox("Conectado")
                    Dim consulta As String
                    consulta = "SELECT * FROM afiliado WHERE CP Like '%" & id & "%';"
                    adapter = New MySqlDataAdapter(consulta, con)
                    datos = New DataSet
                    adapter.Fill(datos, "afiliado")
                    DataGridView1.DataSource = datos
                    DataGridView1.DataMember = "afiliado"
                    con.Close()

                Catch ex As Exception
                    MsgBox(ex.Message)
                End Try
                DataGridView1.Refresh()
            End If

        End If

            idNumberField.ReadOnly = False
        NameTextField.ReadOnly = True
    End Sub

    Private Sub idNumberField_Click(sender As Object, e As EventArgs) Handles idNumberField.Click
        NameTextField.ReadOnly = True
        idNumberField.ReadOnly = False
    End Sub

    Private Sub NameTextField_Click(sender As Object, e As EventArgs) Handles NameTextField.Click
        idNumberField.ReadOnly = True
        NameTextField.ReadOnly = False
    End Sub

    Private Sub Form3_Load(sender As Object, e As EventArgs) Handles MyBase.Load

        NameTextField.ReadOnly = True
        Try
            con.Open()
            'MsgBox("Conectado")
            Dim consulta As String
            consulta = "SELECT * FROM afiliado"
            adapter = New MySqlDataAdapter(consulta, con)
            datos = New DataSet
            adapter.Fill(datos, "afiliado")
            DataGridView1.DataSource = datos
            DataGridView1.DataMember = "afiliado"
            DataGridView1.Refresh()
            con.Close()

        Catch ex As Exception
            MsgBox(ex.Message)
        End Try
        DataGridView1.Refresh()


    End Sub


    Private Sub Imprimir_pdf_Click(sender As Object, e As EventArgs) Handles Imprimir_pdf.Click

        Dim pTitle As New Font(iTextSharp.text.Font.FontFamily.TIMES_ROMAN, 11, iTextSharp.text.Font.BOLD, BaseColor.BLACK)
        Dim pTable As New Font(iTextSharp.text.Font.FontFamily.TIMES_ROMAN, 8, iTextSharp.text.Font.NORMAL, BaseColor.BLACK)


        Dim widths As Single() = New Single() {30.0F, 10.0F, 30.0F, 30.5F, 35.0F, 30.5F, 25.0F, 40.0F, 25.0F, 20.0F, 30.0F, 30.0F, 15.0F, 20.0F}

        'Creating iTextSharp Table from the DataTable data
        Dim pdfTable As New PdfPTable(DataGridView1.ColumnCount)
        pdfTable.SetWidths(widths)
        pdfTable.DefaultCell.Padding = 2
        pdfTable.TotalWidth = 830.0F
        pdfTable.LockedWidth = True
        pdfTable.HorizontalAlignment = Element.ALIGN_LEFT
        pdfTable.DefaultCell.BorderWidth = 1

        'Adding Header row
        For Each column As DataGridViewColumn In DataGridView1.Columns
            Dim cell As New PdfPCell(New Phrase(column.HeaderText, pTitle))
            cell.BackgroundColor = New BaseColor(240, 240, 240)
            cell.HorizontalAlignment = Element.ALIGN_CENTER
            cell.BorderWidth = 1.2

            pdfTable.AddCell(cell)
        Next

        'Adding DataRow
        For Each row As DataGridViewRow In DataGridView1.Rows
            For Each cell As DataGridViewCell In row.Cells
                pdfTable.AddCell(New Phrase(cell.EditedFormattedValue.ToString, pTable))
            Next
        Next

        'Exporting to PDF
        SaveFileDialog1.Filter = "pdf files (*.pdf)|*.pdf"
        SaveFileDialog1.ShowDialog()
        If SaveFileDialog1.FileName = "" Then
            MsgBox("Introduzca un nombre para el archivo", vbExclamation)
        Else
            Using stream As New FileStream(SaveFileDialog1.FileName, FileMode.Create)
                Dim pdfDoc As New Document(PageSize.A4.Rotate(), 5, 10, 10, 5)
                PdfWriter.GetInstance(pdfDoc, stream)
                pdfDoc.Open()
                pdfDoc.Add(pdfTable)
                pdfDoc.Close()
                stream.Close()
            End Using
            MsgBox("PDF creado correctamente", vbInformation)
        End If
    End Sub

    Private Sub Form3_FormClosing(sender As Object, e As FormClosingEventArgs) Handles MyBase.FormClosing
        End
    End Sub
End Class