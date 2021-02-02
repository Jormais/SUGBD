Imports MySql.Data.MySqlClient

Public Class Form2
    Dim comando As New MySqlCommand
    Dim ruta As String = "Server=directionServer;Database=nameDatabase;Uid=userDatabase ;Pwd=password;Port=porToConect"
    Dim con As New MySqlConnection(ruta)
    Public permisoImg As String
    Private Sub Label2_Click(sender As Object, e As EventArgs) Handles Label2.Click
        Form1.Show()
        Me.Hide()
    End Sub

    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click

        If mediosPropios.Checked = True And mediosExternos.Checked = True Then
            permisoImg = "Medios propios,medios externos"
        ElseIf mediosPropios.Checked = True And mediosExternos.Checked = False Then
            permisoImg = "Medios propios"
        ElseIf mediosPropios.Checked = False And mediosExternos.Checked = True Then
            permisoImg = "Medios externos"
        ElseIf mediosPropios.Checked = False And mediosExternos.Checked = False Then
            permisoImg = "Ninguno"
        End If

        Try
            con.Open()
            comando = New MySqlCommand("INSERT INTO afiliado(`N Empleado`,CP,Nombre,Apellidos,Departamento,Telefono,Dni,Correo,`Situacion Laboral`,`Seguro Salud`,`Protec.Datos`,`Permiso.Img`,ABS,Afiliado) VALUES(@NEmpleado,@NConductor,@Nombre,@Apellidos,@Departamento,@Telefono,@Dni,@Correo,@Jubilacion,@Seguro,@ProtecDatos,@PermisoImg,@ABS,@Afiliado)", con)
            comando.Parameters.AddWithValue("@NEmpleado", NEmpleado.Text)
            comando.Parameters.AddWithValue("@NConductor", NConductor.Text)
            comando.Parameters.AddWithValue("@Apellidos", Apellidos.Text)
            comando.Parameters.AddWithValue("@Nombre", Nombre.Text)
            comando.Parameters.AddWithValue("@Departamento", Departamento.Text)
            comando.Parameters.AddWithValue("@Telefono", Telefono.Text)
            comando.Parameters.AddWithValue("@Dni", DNI.Text)
            comando.Parameters.AddWithValue("@Correo", Email.Text)
            comando.Parameters.AddWithValue("@Jubilacion", Jubilacion.Text)
            comando.Parameters.AddWithValue("@Seguro", seguroSalud.Text)
            comando.Parameters.AddWithValue("@ProtecDatos", ProtecDatos.Text)
            comando.Parameters.AddWithValue("@PermisoImg", permisoImg)
            comando.Parameters.AddWithValue("@ABS", ABS.Text)
            comando.Parameters.AddWithValue("@Afiliado", Afiliado.Text)


            comando.ExecuteNonQuery()
            MsgBox("datos guardados")
            con.Close()

            Me.Hide()
            Form1.Show()
        Catch ex As Exception
            MsgBox("No se conecto por: " & ex.Message)
            con.Close()
        End Try
        con.Close()

    End Sub

    Private Sub Form2_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        ':::Utilizamos el try para capturar posibles errores

    End Sub

    Private Sub Form2_FormClosing(sender As Object, e As FormClosingEventArgs) Handles MyBase.FormClosing
        End
    End Sub
End Class