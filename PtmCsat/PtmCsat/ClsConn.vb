Imports System.Data.SqlClient
Imports System.Web.Configuration
Imports Microsoft.VisualBasic
Imports System.IO
Public Class ClsConn
    Inherits System.Web.UI.Page
    Protected sql As String
    Protected cn As SqlConnection
    Protected cmd As SqlCommand
    Protected da As SqlDataAdapter
    Protected ds As DataSet
    Protected dt As DataTable
    Protected dr As SqlDataReader
    Dim tmplogin As String
    Public Function opencn() As Boolean
        Try
            cn = New SqlConnection(WebConfigurationManager.ConnectionStrings("DefaultConnection").ConnectionString)
            cn.Open()

            If cn.State <> ConnectionState.Open Then
                Return False
            Else
                Return True
            End If
        Catch ex As Exception
            'Call GetExceptionInfo(ex)
            Return Nothing
        End Try
    End Function

    Public Sub Closecn()
        Try
            If Not IsNothing(cn) Then
                cn.Close()
                cn = Nothing
            End If
        Catch ex As Exception
            'Call GetExceptionInfo(ex)
        End Try

    End Sub

    Public Function ExecuteQuery(ByVal sql As String) As DataTable
        Try
            If Not opencn() Then
                Return Nothing
                'ErrorTRAPZ(Now & vbTab & "Connection Failed")
                Exit Function
            End If

            cmd = New SqlCommand(sql, cn)
            da = New SqlDataAdapter
            ds = New Data.DataSet

            da.SelectCommand = cmd
            da.Fill(ds)

            dt = ds.Tables(0)
            Return dt

        Catch ex As Exception

            'Call GetExceptionInfo(ex)
        Finally

            dt = Nothing
            da = Nothing
            cmd = Nothing
            ds = Nothing

            Closecn()

        End Try
    End Function

    Public Sub ExecuteNonQuery(ByVal sql As String)
        Try
            If Not opencn() = True Then
                'ErrorTRAPZ(Now & vbTab & "Connection Failed")
                Exit Sub
            End If

            cmd = New SqlCommand
            cmd.Connection = cn
            cmd.CommandText = sql
            cmd.ExecuteNonQuery()
        Catch ex As Exception

            'Call GetExceptionInfo(ex)
        Finally

            cmd = Nothing
            Closecn()
        End Try

    End Sub

    Public Function ExecuteReader(ByVal sql As String)
        Try
            If Not opencn() = True Then
                Return Nothing
                'ErrorTRAPZ(Now & vbTab & "Connection Failed")
                Exit Function
            End If

            cmd = New SqlCommand(sql, cn)
            dr = cmd.ExecuteReader(CommandBehavior.CloseConnection)
            'dr = cmd.ExecuteReader()

            Return dr

        Catch ex As Exception
            Return Nothing
            'Call GketExceptionInfo(ex)
        End Try
    End Function

    Public Sub CloseDR()
        Try
            If Not IsNothing(cn) Then
                dr.Close()
                cn.Close()
                cn = Nothing
            End If
        Catch ex As Exception

            'Call GetExceptionInfo(ex)
        End Try
    End Sub

    Private Shared Function GetChatConnection() As SqlConnection
        Dim chatconn As New SqlConnection(ConfigurationManager.ConnectionStrings("chat").ConnectionString)
        Return chatconn
    End Function

    Private Shared Function GetWeberConnection() As SqlConnection
        Dim weberConnection As New SqlConnection(ConfigurationManager.ConnectionStrings("DefaultConnection").ConnectionString)

        Return weberConnection
    End Function
    Public Shared Function selectTable(ByVal tableName As String, ByVal fieldsName As String, ByVal conditionSelect As String) As String()

        Dim Com As SqlCommand
        Dim Dr As SqlDataReader
        Dim weberConnection As SqlConnection = GetWeberConnection()
        Dim sqlCekMailKirim As String = "select " & fieldsName & " from " & tableName & " where " & conditionSelect & ""
        Com = New SqlCommand(sqlCekMailKirim, weberConnection)
        Try
            weberConnection.Open()
            Dr = Com.ExecuteReader()
            Dr.Read()
            Dim Xtring As String = fieldsName
            Dim strArray() As String = Xtring.Split(",")

            Dim i As Integer = 0
            While (i < strArray.Length.ToString())
                strArray(i) = Dr(strArray(i)).ToString
                i = i + 1
            End While
            Dr.Close()
            weberConnection.Close()
            Return strArray

        Catch ex As Exception

        End Try


    End Function

    Public Shared Function countTable(ByVal tableName As String, ByVal fieldsName As String, ByVal conditionSelect As String) As String
        Dim strArray As String = ""
        Dim Com As SqlCommand
        Dim Dr As SqlDataReader
        Dim weberConnection As SqlConnection = GetWeberConnection()
        Dim sqlCekMailKirim As String = "select " & fieldsName & " as iniCountTable from " & tableName & " where " & conditionSelect & ""
        Com = New SqlCommand(sqlCekMailKirim, weberConnection)
        weberConnection.Open()
        Dr = Com.ExecuteReader()
        If Dr.Read() Then
            strArray = Dr("iniCountTable").ToString
        Else
            strArray = ""
        End If
        'Dim Xtring As String = fieldsName
        'Dim strArray() As String = Xtring.Split(",")

        'Dim i As Integer = 0
        'While (i < strArray.Length.ToString())
        '    strArray(i) = Dr(strArray(i)).ToString
        '    i = i + 1
        'End While

        Dr.Close()
        weberConnection.Close()
        Return strArray

    End Function

    Public Shared Function countTable_chat(ByVal tableName As String, ByVal fieldsName As String, ByVal conditionSelect As String) As String
        Dim strArray As String = ""
        Dim Com As SqlCommand
        Dim Dr As SqlDataReader
        Dim chatConnection As SqlConnection = GetChatConnection()
        Dim sqlCekMailKirim As String = "select " & fieldsName & " as iniCountTable from " & tableName & " where " & conditionSelect & ""
        Com = New SqlCommand(sqlCekMailKirim, chatConnection)
        chatConnection.Open()
        Dr = Com.ExecuteReader()
        If Dr.Read() Then
            strArray = Dr("iniCountTable").ToString
        Else
            strArray = ""
        End If
        'Dim Xtring As String = fieldsName
        'Dim strArray() As String = Xtring.Split(",")

        'Dim i As Integer = 0
        'While (i < strArray.Length.ToString())
        '    strArray(i) = Dr(strArray(i)).ToString
        '    i = i + 1
        'End While

        Dr.Close()
        chatConnection.Close()
        Return strArray

    End Function

    Public Shared Function insertTable(ByVal tableName As String, ByVal fieldsName As String, ByVal valuesName As String) As Boolean
        Dim Com As SqlCommand
        Dim weberConnection As SqlConnection = GetWeberConnection()

        Dim XtringField As String = fieldsName
        Dim strArrayField() As String = XtringField.Split(",")
        Dim resultFields As String = ""

        Dim XtringValue As String = valuesName
        Dim strArrayValue() As String = XtringValue.Split(",")
        Dim resultValues As String = ""

        If strArrayField.Length.ToString() <> strArrayValue.Length.ToString() Then
            Return False
        Else
            Dim i As Integer = 0
            While (i < strArrayField.Length.ToString())

                resultFields &= "" & strArrayField(i).ToString & "" & ","
                resultValues &= "'" & strArrayValue(i).ToString & "'" & ","

                i = i + 1
            End While

            Dim insertQuery As String = "insert into " & tableName & " (" & RemoveLastCharacter(resultFields.ToString) & ") values (" & RemoveLastCharacter(resultValues.ToString) & ")"
            Com = New SqlCommand(insertQuery, weberConnection)
            weberConnection.Open()
            Com.ExecuteNonQuery()
            weberConnection.Close()
            Return True
        End If
    End Function

    Public Shared Function updateTable(ByVal tableName As String, ByVal fieldsName As String, ByVal valuesName As String, ByVal conditionUpdate As String) As Boolean
        Dim Com As SqlCommand
        Dim weberConnection As SqlConnection = GetWeberConnection()

        Dim XtringField As String = fieldsName
        Dim strArrayField() As String = XtringField.Split(",")
        Dim resultFields As String = ""

        Dim XtringValue As String = valuesName
        Dim strArrayValue() As String = XtringValue.Split(",")
        Dim resultValues As String = ""

        If strArrayField.Length.ToString() <> strArrayValue.Length.ToString() Then

            Return False

        Else

            Dim i As Integer = 0
            Dim updateContent As String = ""
            While (i < strArrayField.Length.ToString())

                resultFields &= "" & strArrayField(i).ToString & "='" & strArrayValue(i).ToString & "'" & ","


                i = i + 1
            End While
            updateContent = resultFields
            Dim updateQuery As String = "update " & tableName & " SET " & RemoveLastCharacter(updateContent.ToString) & " WHERE " & conditionUpdate & ""
            Com = New SqlCommand(updateQuery, weberConnection)
            weberConnection.Open()
            Com.ExecuteNonQuery()
            weberConnection.Close()
            Return True
        End If
    End Function

    Public Shared Function deleteRowTable(ByVal tableName As String, ByVal conditionUpdate As String) As Boolean
        Dim Com As SqlCommand
        Dim weberConnection As SqlConnection = GetWeberConnection()

        Dim deleteQuery As String = "delete from " & tableName & " WHERE " & conditionUpdate & ""
        Com = New SqlCommand(deleteQuery, weberConnection)
        weberConnection.Open()
        If Com.ExecuteNonQuery() Then
            Return True
        Else
            Return False
        End If
        weberConnection.Close()
    End Function

    Public Shared Function RemoveLastCharacter(ByVal Text As String) As String
        Return Text.Substring(0, Text.Length - 1)
    End Function

    Public Shared Function disabledButtonDelete(ByVal valPublish As String) As Boolean

        Return False

    End Function

    Public Shared Function replaceAnything(ByVal valueString As String, ByVal ygDiReplace As String, ByVal menjadi As String)


        Return valueString.Replace(ygDiReplace, menjadi)


    End Function

    Public Shared Function cekSessionLogin(ByVal LevelUser As String, ByVal PageFile As String) As Boolean
        Dim Com As SqlCommand
        Dim Dr As SqlDataReader
        Dim weberConnection As SqlConnection = GetWeberConnection()
        Dim sqlCekMailKirim As String = "select * from msUserTrustee where LevelUser = '" & LevelUser & "'"
        Com = New SqlCommand(sqlCekMailKirim, weberConnection)
        Try
            weberConnection.Open()
            Dr = Com.ExecuteReader()
            While Dr.Read()
                If Dr("LevelUser").ToString = LevelUser Then
                    If PageFile = "RPT" Then
                        If Dr("RPT").ToString = True Then
                            Return True
                        Else
                            Return False
                        End If
                    End If
                    If PageFile = "MasterTable" Then
                        If Dr("MasterTable").ToString = True Then
                            Return True
                        Else
                            Return False
                        End If
                    End If
                    If PageFile = "TH" Then
                        If Dr("TH").ToString = True Then
                            Return True
                        Else
                            Return False
                        End If
                    End If
                    If PageFile = "TIC" Then
                        If Dr("TIC").ToString = True Then
                            Return True
                        Else
                            Return False
                        End If
                    End If
                    If PageFile = "KB" Then
                        If Dr("KB").ToString = True Then
                            Return True
                        Else
                            Return False
                        End If
                    End If
                    If PageFile = "Email" Then
                        If Dr("Email").ToString = True Then
                            Return True
                        Else
                            Return False
                        End If
                    End If
                    If PageFile = "Customer" Then
                        If Dr("Customer").ToString = True Then
                            Return True
                        Else
                            Return False
                        End If
                    End If

                Else
                    Return False
                End If
            End While
            Dr.Close()
        Catch ex As Exception

        Finally
            weberConnection.Close()
        End Try
    End Function


    Public Sub writedata(ByVal strdata As String)
        Dim objreader As System.IO.StreamWriter
        Dim dt As String = Date.Now.ToString("MM-dd-yyyy")
        Dim namalog, nmdir As String
        nmdir = Server.MapPath("log")
        namalog = Server.MapPath("log\logFile-" & dt & ".txt")
        Dim f As System.IO.FileStream
        Dim p As DirectoryInfo
        If System.IO.Directory.Exists(nmdir) = False Then
            p = Directory.CreateDirectory(nmdir)
            p.Create()
        End If
        If System.IO.File.Exists(namalog) = False Then
            f = File.Create(namalog)
            f.Close()
        End If
        Try
            objreader = New System.IO.StreamWriter(namalog, True)
            objreader.Write(Now & " : " & strdata)
            objreader.WriteLine()
            objreader.Close()
        Catch ex As Exception
            Throw New Exception(ex.Message)
        End Try
    End Sub

    Public Sub LogAddressEmail(ByVal strdata As String)
        Dim objreader As System.IO.StreamWriter
        Dim dt As String = Date.Now.ToString("MM-dd-yyyy")
        Dim namalog, nmdir As String
        nmdir = Server.MapPath("log")
        namalog = Server.MapPath("log\SendEmail-" & dt & ".txt")
        Dim f As System.IO.FileStream
        Dim p As DirectoryInfo
        If System.IO.Directory.Exists(nmdir) = False Then
            p = Directory.CreateDirectory(nmdir)
            p.Create()
        End If
        If System.IO.File.Exists(namalog) = False Then
            f = File.Create(namalog)
            f.Close()
        End If
        Try
            objreader = New System.IO.StreamWriter(namalog, True)
            objreader.Write(Now & " : " & strdata)
            objreader.WriteLine()
            objreader.Close()
        Catch ex As Exception
            Throw New Exception(ex.Message)
        End Try
    End Sub

    Public Sub LogMasterData(ByVal strdata As String)
        Dim objreader As System.IO.StreamWriter
        Dim dt As String = Date.Now.ToString("MM-dd-yyyy")
        Dim namalog, nmdir As String
        nmdir = Server.MapPath("log")
        namalog = Server.MapPath("log\LogMasterTable-" & dt & ".txt")
        Dim f As System.IO.FileStream
        Dim p As DirectoryInfo
        If System.IO.Directory.Exists(nmdir) = False Then
            p = Directory.CreateDirectory(nmdir)
            p.Create()
        End If
        If System.IO.File.Exists(namalog) = False Then
            f = File.Create(namalog)
            f.Close()
        End If
        Try
            objreader = New System.IO.StreamWriter(namalog, True)
            objreader.Write(Now & " : " & strdata)
            objreader.WriteLine()
            objreader.Close()
        Catch ex As Exception
            Throw New Exception(ex.Message)
        End Try
    End Sub

    Public Sub LogSendEmailDispacth(ByVal strdata As String)
        Dim objreader As System.IO.StreamWriter
        Dim dt As String = Date.Now.ToString("MM-dd-yyyy")
        Dim namalog, nmdir As String
        nmdir = Server.MapPath("log")
        namalog = Server.MapPath("log\SendEmailDispacth-" & dt & ".txt")
        Dim f As System.IO.FileStream
        Dim p As DirectoryInfo
        If System.IO.Directory.Exists(nmdir) = False Then
            p = Directory.CreateDirectory(nmdir)
            p.Create()
        End If
        If System.IO.File.Exists(namalog) = False Then
            f = File.Create(namalog)
            f.Close()
        End If
        Try
            objreader = New System.IO.StreamWriter(namalog, True)
            objreader.Write(Now & " : " & strdata)
            objreader.WriteLine()
            objreader.Close()
        Catch ex As Exception
            Throw New Exception(ex.Message)
        End Try
    End Sub


    Public Sub LogNotSendEmailDispacth(ByVal strdata As String)
        Dim objreader As System.IO.StreamWriter
        Dim dt As String = Date.Now.ToString("MM-dd-yyyy")
        Dim namalog, nmdir As String
        nmdir = Server.MapPath("log")
        namalog = Server.MapPath("log\NotSendEmailDispacth-" & dt & ".txt")
        Dim f As System.IO.FileStream
        Dim p As DirectoryInfo
        If System.IO.Directory.Exists(nmdir) = False Then
            p = Directory.CreateDirectory(nmdir)
            p.Create()
        End If
        If System.IO.File.Exists(namalog) = False Then
            f = File.Create(namalog)
            f.Close()
        End If
        Try
            objreader = New System.IO.StreamWriter(namalog, True)
            objreader.Write(Now & " : " & strdata)
            objreader.WriteLine()
            objreader.Close()
        Catch ex As Exception
            Throw New Exception(ex.Message)
        End Try
    End Sub

    Public Sub LogCreateTicket(ByVal strdata As String)
        Dim objreader As System.IO.StreamWriter
        Dim dt As String = Date.Now.ToString("MM-dd-yyyy")
        Dim namalog, nmdir As String
        nmdir = Server.MapPath("log")
        namalog = Server.MapPath("log\logCreateTicket-" & dt & ".txt")
        Dim f As System.IO.FileStream
        Dim p As DirectoryInfo
        If System.IO.Directory.Exists(nmdir) = False Then
            p = Directory.CreateDirectory(nmdir)
            p.Create()
        End If
        If System.IO.File.Exists(namalog) = False Then
            f = File.Create(namalog)
            f.Close()
        End If
        Try
            objreader = New System.IO.StreamWriter(namalog, True)
            objreader.Write(Now & " : " & strdata)
            objreader.WriteLine()
            objreader.Close()
        Catch ex As Exception
            Throw New Exception(ex.Message)
        End Try
    End Sub

    Public Sub LogPrankCall(ByVal strdata As String)
        Dim objreader As System.IO.StreamWriter
        Dim dt As String = Date.Now.ToString("MM-dd-yyyy")
        Dim namalog, nmdir As String
        nmdir = Server.MapPath("log")
        namalog = Server.MapPath("log\logPrankCall-" & dt & ".txt")
        Dim f As System.IO.FileStream
        Dim p As DirectoryInfo
        If System.IO.Directory.Exists(nmdir) = False Then
            p = Directory.CreateDirectory(nmdir)
            p.Create()
        End If
        If System.IO.File.Exists(namalog) = False Then
            f = File.Create(namalog)
            f.Close()
        End If
        Try
            objreader = New System.IO.StreamWriter(namalog, True)
            objreader.Write(Now & " : " & strdata)
            objreader.WriteLine()
            objreader.Close()
        Catch ex As Exception
            Throw New Exception(ex.Message)
        End Try
    End Sub


    Public Sub LogActionOverSLA(ByVal strdata As String)
        Dim objreader As System.IO.StreamWriter
        Dim dt As String = Date.Now.ToString("MM-dd-yyyy")
        Dim namalog, nmdir As String
        nmdir = Server.MapPath("log")
        namalog = Server.MapPath("log\OverSLATicket-" & dt & ".txt")
        Dim f As System.IO.FileStream
        Dim p As DirectoryInfo
        If System.IO.Directory.Exists(nmdir) = False Then
            p = Directory.CreateDirectory(nmdir)
            p.Create()
        End If
        If System.IO.File.Exists(namalog) = False Then
            f = File.Create(namalog)
            f.Close()
        End If
        Try
            objreader = New System.IO.StreamWriter(namalog, True)
            objreader.Write(Now & " : " & strdata)
            objreader.WriteLine()
            objreader.Close()
        Catch ex As Exception
            Throw New Exception(ex.Message)
        End Try
    End Sub

    Public Sub LogNotSendEmail(ByVal strdata As String)
        Dim objreader As System.IO.StreamWriter
        Dim dt As String = Date.Now.ToString("MM-dd-yyyy")
        Dim namalog, nmdir As String
        nmdir = Server.MapPath("log")
        namalog = Server.MapPath("log\NoSendEmail-" & dt & ".txt")
        Dim f As System.IO.FileStream
        Dim p As DirectoryInfo
        If System.IO.Directory.Exists(nmdir) = False Then
            p = Directory.CreateDirectory(nmdir)
            p.Create()
        End If
        If System.IO.File.Exists(namalog) = False Then
            f = File.Create(namalog)
            f.Close()
        End If
        Try
            objreader = New System.IO.StreamWriter(namalog, True)
            objreader.Write(Now & " : " & strdata)
            objreader.WriteLine()
            objreader.Close()
        Catch ex As Exception
            Throw New Exception(ex.Message)
        End Try
    End Sub

    Public Sub loginsertcustomer(ByVal strdata As String)
        Dim objreader As System.IO.StreamWriter
        Dim dt As String = Date.Now.ToString("MM-dd-yyyy")
        Dim namalog, nmdir As String
        nmdir = Server.MapPath("log")
        namalog = Server.MapPath("log\insertcustomer\" & dt & ".txt")
        Dim f As System.IO.FileStream
        Dim p As DirectoryInfo
        If System.IO.Directory.Exists(nmdir) = False Then
            p = Directory.CreateDirectory(nmdir)
            p.Create()
        End If
        If System.IO.File.Exists(namalog) = False Then
            f = File.Create(namalog)
            f.Close()
        End If
        Try
            objreader = New System.IO.StreamWriter(namalog, True)
            objreader.Write(Now & " : " & strdata)
            objreader.WriteLine()
            objreader.Close()
        Catch ex As Exception
            Throw New Exception(ex.Message)
        End Try
    End Sub
    Public Sub loginsertunit(ByVal strdata As String)
        Dim objreader As System.IO.StreamWriter
        Dim dt As String = Date.Now.ToString("MM-dd-yyyy")
        Dim namalog, nmdir As String
        nmdir = Server.MapPath("log")
        namalog = Server.MapPath("log\insertunit\" & dt & ".txt")
        Dim f As System.IO.FileStream
        Dim p As DirectoryInfo
        If System.IO.Directory.Exists(nmdir) = False Then
            p = Directory.CreateDirectory(nmdir)
            p.Create()
        End If
        If System.IO.File.Exists(namalog) = False Then
            f = File.Create(namalog)
            f.Close()
        End If
        Try
            objreader = New System.IO.StreamWriter(namalog, True)
            objreader.Write(Now & " : " & strdata)
            objreader.WriteLine()
            objreader.Close()
        Catch ex As Exception
            Throw New Exception(ex.Message)
        End Try
    End Sub
    Public Sub LogSuccess(ByVal agentName As String, strValue As String)
        Dim message As String = String.Format("Time: {0}", DateTime.Now.ToString("dd/MM/yyyy hh:mm:ss tt"))
        message += Environment.NewLine
        message += "---------------------------Success-------------------------------------------------------"
        message += Environment.NewLine
        message += String.Format("Message: {0}", strValue)
        message += Environment.NewLine
        message += "---------------------------Success-------------------------------------------------------"
        message += Environment.NewLine

        Try
            Dim DirectoryX As String = Path.Combine(Server.MapPath("~/HTML/ErrorLog/" & agentName & "/" & DateTime.Now.ToString("ddMMyyyy")))
            If Not System.IO.Directory.Exists(DirectoryX) Then
                System.IO.Directory.CreateDirectory(DirectoryX)
            End If
        Catch exX As Exception
            ''Try catch untuk error create folder
            Dim pathXX As String = HttpContext.Current.Server.MapPath("~/HTML/ErrorLog/" & agentName & "/" & DateTime.Now.ToString("ddMMyyyy") & ".txt")
            Dim messageXX As String = String.Format("Time: {0}", DateTime.Now.ToString("dd/MM/yyyy hh:mm:ss tt"))
            messageXX += Environment.NewLine
            messageXX += "---------------------------Success-------------------------------------------------------"
            messageXX += Environment.NewLine
            messageXX += String.Format("Message: {0}", exX.Message)
            messageXX += Environment.NewLine
            messageXX += String.Format("StackTrace: {0}", exX.StackTrace)
            messageXX += Environment.NewLine
            messageXX += String.Format("Source: {0}", exX.Source)
            messageXX += Environment.NewLine
            messageXX += String.Format("TargetSite: {0}", exX.TargetSite.ToString())
            messageXX += Environment.NewLine
            messageXX += "---------------------------Success-------------------------------------------------------"
            messageXX += Environment.NewLine
            Using writer As New StreamWriter(pathXX, True)
                writer.WriteLine(messageXX)
                writer.Close()
            End Using
        Finally
            Dim pathX As String = HttpContext.Current.Server.MapPath("~/HTML/ErrorLog/" & agentName & "/" & DateTime.Now.ToString("ddMMyyyy") & "/" & DateTime.Now.ToString("ddMMyyyy") & ".txt")
            Using writer As New StreamWriter(pathX, True)
                writer.WriteLine(message)
                writer.Close()
            End Using
        End Try
    End Sub
    Public Sub LogError(ByVal agentName As String, ex As Exception, strUser As String)
        Dim message As String = String.Format("Time: {0}", DateTime.Now.ToString("dd/MM/yyyy hh:mm:ss tt"))
        message += Environment.NewLine
        message += "---------------------------Failed-------------------------------------------------------"
        message += Environment.NewLine
        message += String.Format("Message: {0}", strUser)
        message += Environment.NewLine
        message += String.Format("Message: {0}", ex.Message)
        message += Environment.NewLine
        message += String.Format("StackTrace: {0}", ex.StackTrace)
        message += Environment.NewLine
        message += String.Format("Source: {0}", ex.Source)
        message += Environment.NewLine
        message += String.Format("TargetSite: {0}", ex.TargetSite.ToString())
        message += Environment.NewLine
        message += "---------------------------Failed-------------------------------------------------------"
        message += Environment.NewLine

        Try
            Dim DirectoryX As String = Path.Combine(Server.MapPath("~/HTML/ErrorLog/" & agentName & "/" & DateTime.Now.ToString("ddMMyyyy")))
            If Not System.IO.Directory.Exists(DirectoryX) Then
                System.IO.Directory.CreateDirectory(DirectoryX)
            End If
        Catch exX As Exception
            ''Try catch untuk error create folder
            Dim pathXX As String = HttpContext.Current.Server.MapPath("~/HTML/ErrorLog/" & agentName & "/" & DateTime.Now.ToString("ddMMyyyy") & ".txt")
            Dim messageXX As String = String.Format("Time: {0}", DateTime.Now.ToString("dd/MM/yyyy hh:mm:ss tt"))
            messageXX += Environment.NewLine
            messageXX += "---------------------------Failed-----------------------------------------------"
            messageXX += Environment.NewLine
            messageXX += String.Format("Message: {0}", strUser)
            messageXX += Environment.NewLine
            messageXX += String.Format("Message: {0}", exX.Message)
            messageXX += Environment.NewLine
            messageXX += String.Format("StackTrace: {0}", exX.StackTrace)
            messageXX += Environment.NewLine
            messageXX += String.Format("Source: {0}", exX.Source)
            messageXX += Environment.NewLine
            messageXX += String.Format("TargetSite: {0}", exX.TargetSite.ToString())
            messageXX += Environment.NewLine
            messageXX += "---------------------------Failed------------------------------------------------"
            messageXX += Environment.NewLine
            Using writer As New StreamWriter(pathXX, True)
                writer.WriteLine(messageXX)
                writer.Close()
            End Using
        Finally
            Dim pathX As String = HttpContext.Current.Server.MapPath("~/HTML/ErrorLog/" & agentName & "/" & DateTime.Now.ToString("dd_MM_yyyy") & "/" & DateTime.Now.ToString("ddMMyyyy") & ".txt")
            Using writer As New StreamWriter(pathX, True)
                writer.WriteLine(message)
                writer.Close()
            End Using
        End Try
    End Sub
End Class
