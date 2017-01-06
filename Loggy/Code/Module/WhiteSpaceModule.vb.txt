
Namespace COR.web.Modules


    ''' <summary>
    ''' Removes whitespace from the webpage.
    ''' </summary>
    Public Class WhitespaceModule
        Implements System.Web.IHttpModule

#Region "IHttpModule Members"

        Private Sub System_Web_IHttpModule_Dispose() Implements System.Web.IHttpModule.Dispose
            ' Nothing to dispose; 
        End Sub

        Private Sub System_Web_IHttpModule_Init(ByVal context As System.Web.HttpApplication) Implements System.Web.IHttpModule.Init
            AddHandler context.BeginRequest, New System.EventHandler(AddressOf context_BeginRequest)
        End Sub

#End Region


        Public Shared Sub MsgBox(ByVal obj As Object)
            If obj IsNot Nothing Then
                System.Windows.Forms.MessageBox.Show(obj.ToString())
            Else
                System.Windows.Forms.MessageBox.Show("obj is NULL")
            End If
        End Sub


        Private Sub context_BeginRequest(ByVal sender As Object, ByVal e As System.EventArgs)
            Dim app As System.Web.HttpApplication = TryCast(sender, System.Web.HttpApplication)

            'Dim ext As String = System.IO.Path.GetExtension(app.Request.RawUrl)
            'If String.IsNullOrEmpty(ext) Then
            'if (app.Request.RawUrl.Contains(".cshtml"))
            If (app.Request.RawUrl.Contains(".aspx")) Then

                If Not StringComparer.OrdinalIgnoreCase.Equals(Environment.MachineName, "pc-steiger") Then
                    app.Response.Filter = New WhitespaceFilter(app.Response.Filter)
                End If

            End If
        End Sub

#Region "Stream filter"

        Private Class WhitespaceFilter
            Inherits System.IO.Stream

            Public Sub New(ByVal sink As System.IO.Stream)
                _sink = sink
            End Sub

            Private _sink As System.IO.Stream
            Private Shared reg As New System.Text.RegularExpressions.Regex("(?<=[^])\t{2,}|(?<=[>])\s{2,}(?=[<])|(?<=[>])\s{2,11}(?=[<])|(?=[\n])\s{2,}")

#Region "Properites"

            Public Overrides ReadOnly Property CanRead() As Boolean
                Get
                    Return True
                End Get
            End Property

            Public Overrides ReadOnly Property CanSeek() As Boolean
                Get
                    Return True
                End Get
            End Property

            Public Overrides ReadOnly Property CanWrite() As Boolean
                Get
                    Return True
                End Get
            End Property

            Public Overrides Sub Flush()
                _sink.Flush()
            End Sub

            Public Overrides ReadOnly Property Length() As Long
                Get
                    Return 0
                End Get
            End Property

            Private _position As Long
            Public Overrides Property Position() As Long
                Get
                    Return _position
                End Get
                Set(ByVal value As Long)
                    _position = value
                End Set
            End Property

#End Region

#Region "Methods"

            Public Overrides Function Read(ByVal buffer As Byte(), ByVal offset As Integer, ByVal count As Integer) As Integer
                Return _sink.Read(buffer, offset, count)
            End Function

            Public Overrides Function Seek(ByVal offset As Long, ByVal origin As System.IO.SeekOrigin) As Long
                Return _sink.Seek(offset, origin)
            End Function

            Public Overrides Sub SetLength(ByVal value As Long)
                _sink.SetLength(value)
            End Sub

            Public Overrides Sub Close()
                _sink.Close()
            End Sub

            Public Overrides Sub Write(ByVal buffer As Byte(), ByVal offset As Integer, ByVal count As Integer)
                Dim data As Byte() = New Byte(count - 1) {}
                System.Buffer.BlockCopy(buffer, offset, data, 0, count)
                Dim html As String = System.Text.Encoding.[Default].GetString(buffer)

                html = reg.Replace(html, String.Empty)

                Dim outdata As Byte() = System.Text.Encoding.[Default].GetBytes(html)
                _sink.Write(outdata, 0, outdata.GetLength(0))
            End Sub

#End Region

        End Class

#End Region

    End Class


End Namespace
