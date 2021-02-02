Imports MySql.Data.MySqlClient
Public Class Form4

    Dim adapter As New MySqlDataAdapter
    Dim command As New MySqlCommand
    Dim ruta As String = "Server=directionServer;Database=nameDatabase;Uid=userDatabase ;Pwd=password;Port=porToConect"
    Dim con As New MySqlConnection(ruta)
    Dim datos, dbox As DataSet
    Dim tdatos As DataView
    Dim names As String


    Private Sub Form4_Load(sender As Object, e As EventArgs) Handles MyBase.Load

        Dim READER As MySqlDataReader
        Try
            con.Open()
            Dim Query As String
            Query = "SELECT * FROM afiliado"
            command = New MySqlCommand(Query, con)
            READER = command.ExecuteReader
            While READER.Read
                Dim sname = READER.GetString("N Empleado")
                ComboBox1.Items.Add(sname)
            End While
            con.Close()
        Catch ex As Exception
            MessageBox.Show(ex.Message)
        Finally
            con.Dispose()

        End Try
    End Sub

    Private Sub ComboBox1_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ComboBox1.SelectedIndexChanged
        Departamento.Items.Clear()
        SL.Items.Clear()
        SS.Items.Clear()
        PD.Items.Clear()
        If MediosPropios.Checked = True Then
            MediosPropios.Checked = False
        End If
        If MediosExternos.Checked = True Then
            MediosExternos.Checked = False
        End If

        ABS.Items.Clear()
        Afiliado.Items.Clear()
        Dim READER As MySqlDataReader

        Try
            con.Open()
            Dim Query As String
            Query = "SELECT * FROM afiliado WHERE `N Empleado`= '" & ComboBox1.Text & "'"
            command = New MySqlCommand(Query, con)
            READER = command.ExecuteReader
            While READER.Read
                'NEmpleado.Text = READER.GetInt32("Nº Empleado")
                CP.Text = READER.GetInt32("CP")
                Nombre.Text = READER.GetString("Nombre")
                Apellidos.Text = READER.GetString("Apellidos")

                With Departamento
                    .Items.Add(READER.GetString("Departamento"))
                    If READER.GetString("Departamento") = "Conductor" Then
                        .Items.Add("Taller")
                        .Items.Add("Administracion")
                    End If
                    If READER.GetString("Departamento") = "Taller" Then
                        .Items.Add("Conductor")
                        .Items.Add("Administracion")
                    End If
                    If READER.GetString("Departamento") = "Administracion" Then
                        .Items.Add("Taller")
                        .Items.Add("Conductor")
                    End If
                    If READER.GetString("Departamento") = "" Then
                        .Items.Add("Conductor")
                        .Items.Add("Taller")
                        .Items.Add("Administracion")
                    End If
                End With
                Departamento.SelectedIndex = 0

                Telefono.Text = READER.GetString("Telefono")
                DNI.Text = READER.GetString("Dni")
                Correo.Text = READER.GetString("Correo")
                With SL
                    .Items.Add(READER.GetString("Situacion Laboral"))
                    If READER.GetString("Situacion Laboral") = "Activo" Then
                        .Items.Add("I.Temporal")
                        .Items.Add("Jubilado")
                        .Items.Add("Prejubilado")
                    End If
                    If READER.GetString("Situacion Laboral") = "I.Temporal" Then
                        .Items.Add("Activo")
                        .Items.Add("Jubilado")
                        .Items.Add("Prejubilado")
                    End If
                    If READER.GetString("Situacion Laboral") = "Jubilado" Then
                        .Items.Add("Activo")
                        .Items.Add("I.Temporal")
                        .Items.Add("Prejubilado")
                    End If
                    If READER.GetString("Situacion Laboral") = "Prejubilado" Then
                        .Items.Add("Activo")
                        .Items.Add("Jubilado")
                        .Items.Add("I.Temporal")
                    End If
                    If READER.GetString("Situacion Laboral") = "" Then
                        .Items.Add("Activo")
                        .Items.Add("Jubilado")
                        .Items.Add("I.Temporal")
                        .Items.Add("Prejubilado")
                    End If
                End With
                SL.SelectedIndex = 0

                With SS
                    .Items.Add(READER.GetString("Seguro Salud"))
                    If READER.GetString("Seguro Salud") = "Si" Then
                        .Items.Add("No")
                    End If
                    If READER.GetString("Seguro Salud") = "No" Then
                        .Items.Add("Si")
                    End If
                    If READER.GetString("Seguro Salud") = "" Then
                        .Items.Add("Si")
                        .Items.Add("No")
                    End If
                End With
                SS.SelectedIndex = 0

                With PD
                    .Items.Add(READER.GetString("Protec.Datos"))
                    If READER.GetString("Protec.Datos") = "Si" Then
                        .Items.Add("No")
                    End If
                    If READER.GetString("Protec.Datos") = "No" Then
                        .Items.Add("Si")
                    End If
                    If READER.GetString("Protec.Datos") = "" Then
                        .Items.Add("Si")
                        .Items.Add("No")
                    End If
                End With
                PD.SelectedIndex = 0

                Dim medios As String = READER.GetString("permiso.Img")
                If medios = "Medios propios,medios externos" Then
                    MediosPropios.Checked = True
                    MediosExternos.Checked = True
                ElseIf medios = "Medios propios" Then
                    MediosPropios.Checked = True
                    MediosExternos.Checked = False
                ElseIf medios = "medios externos" Then
                    MediosPropios.Checked = False
                    MediosExternos.Checked = True
                ElseIf medios = "Ninguno" Then
                    MediosPropios.Checked = False
                    MediosExternos.Checked = False
                End If

                With ABS
                    .Items.Add(READER.GetString("ABS"))
                    If READER.GetString("ABS") = "Si" Then
                        .Items.Add("No")
                    End If
                    If READER.GetString("ABS") = "No" Then
                        .Items.Add("Si")
                    End If
                    If READER.GetString("ABS") = "" Then
                        .Items.Add("Si")
                        .Items.Add("No")
                    End If
                End With
                ABS.SelectedIndex = 0

                With Afiliado
                    .Items.Add(READER.GetString("Afiliado"))
                    If READER.GetString("Afiliado") = "SUG" Then
                        .Items.Add("CCOO")
                        .Items.Add("OTRO")
                        .Items.Add("NINGUNO")
                    End If
                    If READER.GetString("Afiliado") = "CCOO" Then
                        .Items.Add("SUG")
                        .Items.Add("OTRO")
                        .Items.Add("NINGUNO")
                    End If
                    If READER.GetString("Afiliado") = "OTRO" Then
                        .Items.Add("SUG")
                        .Items.Add("CCOO")
                        .Items.Add("NINGUNO")
                    End If
                    If READER.GetString("Afiliado") = "NINGUNO" Then
                        .Items.Add("SUG")
                        .Items.Add("CCOO")
                        .Items.Add("OTRO")
                    End If
                    If READER.GetString("Afiliado") = "" Then
                        .Items.Add("SUG")
                        .Items.Add("CCOO")
                        .Items.Add("OTRO")
                        .Items.Add("NINGUNO")
                    End If
                End With
                Afiliado.SelectedIndex = 0
            End While
            con.Close()
        Catch ex As Exception
            MessageBox.Show(ex.Message)
        Finally
            con.Dispose()
        End Try
    End Sub

    Private Sub Label15_Click(sender As Object, e As EventArgs) Handles Label15.Click
        Form1.Show()
        Me.Hide()

    End Sub

    Private Sub Form4_FormClosing(sender As Object, e As FormClosingEventArgs) Handles MyBase.FormClosing
        End
    End Sub

    Private Sub Button2_Click(sender As Object, e As EventArgs) Handles Button2.Click
        Dim medios As String
        If MediosPropios.Checked = True And MediosExternos.Checked = True Then
            medios = "Medios propios,medios externos"
        End If
        If MediosPropios.Checked = False And MediosExternos.Checked = True Then
            medios = "Medios externos"
        End If
        If MediosPropios.Checked = True And MediosExternos.Checked = False Then
            medios = "Medios propios"
        End If
        If MediosPropios.Checked = False And MediosExternos.Checked = False Then
            medios = "Ninguno"
        End If

        Dim st As String = ComboBox1.SelectedItem
        MsgBox(st)
        Dim NEmpl As Integer = CInt(st)
        Try
            con.Open()
            'MsgBox("Conectado")
            Dim consulta As String
            consulta = "UPDATE jonaybri_sugbbdd.afiliado SET CP=@CP,Nombre=@Nombre,Apellidos=@Apellidos,Departamento=@Departamento,Telefono=@Telefono,Dni=@Dni,Correo=@Correo,`Situacion Laboral`=@ST,`Seguro Salud`=@SS,`Protec.Datos`=@PD,`Permiso.Img`=@PI,ABS=@ABS,Afiliado=@Afiliado WHERE `N Empleado`='" & NEmpl & "'"
            command = New MySqlCommand(consulta, con)
            'command.Parameters.AddWithValue("@NEmpleado", NEmpleado.Text)

            command.Parameters.AddWithValue("@CP", CP.Text)
            command.Parameters.AddWithValue("@Nombre", Nombre.Text)
            command.Parameters.AddWithValue("@Apellidos", Apellidos.Text)
            command.Parameters.AddWithValue("@Departamento", Departamento.Text)
            command.Parameters.AddWithValue("@Telefono", Telefono.Text)
            command.Parameters.AddWithValue("@Dni", DNI.Text)
            command.Parameters.AddWithValue("@Correo", Correo.Text)
            command.Parameters.AddWithValue("@ST", SL.Text)
            command.Parameters.AddWithValue("@SS", SS.Text)
            command.Parameters.AddWithValue("@PD", PD.Text)
            command.Parameters.AddWithValue("@PI", medios)
            command.Parameters.AddWithValue("@ABS", ABS.Text)
            command.Parameters.AddWithValue("@Afiliado", Afiliado.Text)
            command.ExecuteNonQuery()
            con.Close()
            Form1.Show()
            Me.Hide()

        Catch ex As Exception
            MsgBox(ex.Message)
            Form1.Show()
            Me.Hide()
        End Try
    End Sub
End Class