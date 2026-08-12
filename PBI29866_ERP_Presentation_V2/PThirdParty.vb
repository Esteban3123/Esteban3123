'***********************************************************************
' Assembly         : Presentacion.Common.MVP
' Author           : Kevin Garay
' Created          : 02-07-2013
'
' Last Modified By : 
' Last Modified On : 
' Description      : Presentador FrmThirdParty - PBI 29866 (DV solo Nacional)
'
' Copyright        : (c) . All rights reserved.
'***********************************************************************
#Region "Imports"

Imports DevExpress.Xpo
Imports Infrastructure.CrossCutting.Base
Imports Infrastructure.Data.Xpo
Imports Infrastructure.Data.Xpo.CommonRepository
Imports Infrastructure.Data.Xpo.PayrollRepository
Imports Presentation.Base
Imports Presentation.Controls.MVP

#End Region

''' <summary>
''' Presentador del frontal de terceros
''' </summary>
''' <remarks></remarks>
Public Class PThirdParty

#Region "Variables and Constructors"

    ''' <summary>
    ''' Variable que se usa para instanciar la interfaz
    ''' </summary>
    Private View As IThirdParty

    ''' <summary>
    ''' Variable que se usa para tratar la corporacion como un objeto
    ''' </summary>
    Private ThirdParty As Object

    ''' <summary>
    ''' Variable que se usa para instanciar la clase singleton
    ''' </summary>
    Private Indigo As SessionValues = SessionValues.Instance

    ''' <summary>
    ''' Inicializa un nuevo constructor para permitir la comunicacion con la interfaz
    ''' </summary>
    ''' <param name="iview">Iview</param>
    ''' <exception cref="System.ArgumentException"></exception>
    Public Sub New(ByRef iview As IThirdParty)
        If iview Is Nothing Then
            Throw New ArgumentException(BaseClass.obtenerExcepcion(EexceptionsResources.MensajeConstructorPresentador))
        Else
            Me.View = iview
        End If
    End Sub

    Public Sub New()
        Me.Indigo = SessionValues.Instance
    End Sub

    ''' <summary>
    ''' Funcion que calcula el digito de verificacion del Nit
    ''' PBI 29866: solo para Clase Nacional (1). Extranjero (2) no calcula DV.
    ''' </summary>
    ''' <param name="Nit">Nit del tercero</param>
    Public Sub Calculate_VerificationCode(ByVal nit As String, personType As Integer, identificationType As Integer, Optional FlagLoadControls As Boolean = True)
        If Not FlagLoadControls Then
            Exit Sub
        End If

        ' Extranjero: no calcular DV
        If View.ClassThirdParty.HasValue AndAlso View.ClassThirdParty.Value = 2 Then
            View.VerificationCode = String.Empty
            Exit Sub
        End If

        ' Solo Nacional (o sin clase aún, comportamiento previo) y NIT numérico
        If View.ClassThirdParty.HasValue AndAlso View.ClassThirdParty.Value = 1 Then
            If Not IsNumeric(nit) Then
                View.VerificationCode = String.Empty
                Exit Sub
            End If
        End If

        If personType = 2 Then
            Me.View.VerificationCode = GetVerificationCode(nit)
        ElseIf personType = 1 AndAlso (identificationType = 0 OrElse identificationType = 7) Then
            Me.View.VerificationCode = GetVerificationCode(nit)
        Else
            Me.View.VerificationCode = 0
        End If
    End Sub

    Private Function GetVerificationCode(ByVal nit As String) As Integer
        nit = Format(Val("" & nit), "000000000000000")
        Dim residue As Integer = 0
        Dim mul As Integer = 0

        For i As Integer = 15 To 1 Step -1
            If i = 15 Then
                mul = 3
            ElseIf i = 14 Then
                mul = 7
            ElseIf i = 13 Then
                mul = 13
            ElseIf i = 12 Then
                mul = 17
            ElseIf i = 11 Then
                mul = 19
            ElseIf i = 10 Then
                mul = 23
            ElseIf i = 9 Then
                mul = 29
            ElseIf i = 8 Then
                mul = 37
            ElseIf i = 7 Then
                mul = 41
            ElseIf i = 6 Then
                mul = 43
            ElseIf i = 5 Then
                mul = 47
            ElseIf i = 4 Then
                mul = 53
            ElseIf i = 3 Then
                mul = 59
            ElseIf i = 2 Then
                mul = 67
            Else
                mul = 71
            End If
            residue = residue + (Val(GetChar(nit, i)) * mul)
        Next
        residue = residue Mod 11

        If residue = 0 Then
            residue = 0
        ElseIf residue = 1 Then
            residue = 1
        Else
            residue = 11 - residue
        End If

        Return residue
    End Function

    Public Sub InitializeCity()
        Using model As New MBusqueda
            View.CitiesXpo = model.ConsultarEntidades(Infrastructure.CrossCutting.Base.eDataSource.ListAllCities, True)
        End Using
    End Sub

    Public Sub InitializeEconomicActivity()
        Using model As New MBusqueda
            View.EconomicActivityXpo = model.ConsultarEntidades(Infrastructure.CrossCutting.Base.eDataSource.ListEconomicActivityByStatus, True)
        End Using
    End Sub

    Public Sub InitializeAccountPayableConcepts()
        Using model As New MBusqueda
            Dim filter() As Object = {True, True, 2}
            View.IVARetentionAccountPayableConceptXpo = model.ConsultarEntidades(Infrastructure.CrossCutting.Base.eDataSource.ListAccountPayableConceptByHandlesRetentionAndConceptType, filter)
        End Using
    End Sub

    ''' <summary>
    ''' Inciializa el datasource de las sucursales
    ''' </summary>
    Public Sub InitializaBranchOffice()
        View.BranchOfficeXpo = XpoServiceEx.Instance(Indigo.TransactionalContainer).PayrollService.ListBranchOfficeByCompanyIsNullAndState(True)
    End Sub

    ''' <summary>
    ''' Obtiene la sucursal xpo por id
    ''' </summary>
    ''' <param name="Id"></param>
    ''' <returns></returns>
    Public Function GetBranchOfficeXpo(Id As Integer) As PayrollBranchOffice
        Dim filtroConsulta As String = "Id = " & Id
        Return XpoServiceEx.Instance(Indigo.TransactionalContainer).PayrollService.GetXPOObject(Of PayrollBranchOffice)(filtroConsulta)
    End Function

    ''' <summary>
    ''' Inciializa el datasource de las sucursales
    ''' </summary>
    Public Sub InitializaFiscalResponsability()
        View.FiscalResponsabilityXpo = XpoServiceEx.Instance(Indigo.TransactionalContainer).CommonService.ListFiscalResponsibilityByStatus(True)
    End Sub

    ''' <summary>
    ''' Obtiene la responsabilidad xpo por Id
    ''' </summary>
    ''' <param name="Id"></param>
    ''' <returns></returns>
    Public Function GetFiscalResponsabilityXpo(Id As Integer) As CommonFiscalResponsibilityXpo
        Dim filtroConsulta As String = "Id = " & Id
        Return XpoServiceEx.Instance(Indigo.TransactionalContainer).CommonService.GetXPOObject(Of CommonFiscalResponsibilityXpo)(filtroConsulta)
    End Function

    ''' <summary>
    ''' Obtiene actividad economica xpo por Id
    ''' </summary>
    ''' <param name="Id"></param>
    ''' <returns></returns>
    Public Function GetEconomicActivityXpoById(Id As Integer) As CommonEconomicActivity
        Dim filtroConsulta As String = "Id = " & Id
        Return XpoServiceEx.Instance(Indigo.TransactionalContainer).CommonService.GetXPOObject(Of CommonEconomicActivity)(filtroConsulta)
    End Function

    ''' <summary>
    ''' Obtiene las exoneraciones tributarias de tipo Documento o Institucion
    ''' </summary>
    ''' <param name="ApplicationType"></param>
    ''' <returns></returns>
    Public Function ListTaxExemptionsByApplicationType(ApplicationType As Boolean) As XPInstantFeedbackSource
        Return XpoServiceEx.Instance(Indigo.TransactionalContainer).CommonService.ListTaxExemptionsByApplicationType(ApplicationType)
    End Function

    ''' <summary>
    ''' Obtiene los diferentes registros de IVA parametrizados
    ''' </summary>
    ''' <returns></returns>
    Public Function ListGeneralLedgerIvaByState(status As Boolean) As XPInstantFeedbackSource
        Return XpoServiceEx.Instance(Indigo.TransactionalContainer).AccountingService.ListXPInstantFeedbackSource(Of InventoryRepository.GeneralLedgerIVAXpo)($"Status={status}", "Id;Code;Name;CodeName")
    End Function

    ''' <summary>
    ''' Asigna el datasource del concepto de retención
    ''' </summary>
    Public Sub InitializeIVARetention()
        View.IVARetentionConceptXpo = XpoServiceEx.Instance(Indigo.TransactionalContainer).AccountingService.ListRetentionConcept()
    End Sub

    ''' <summary>
    ''' metodo que consulta los tipo de identificacion del ehr y se los asigna al datasource del control
    ''' </summary>
    Public Sub InitializaIdentificationType()
        View.IdentificationTypeDatasource = XpoServiceEx.Instance(Indigo.TransactionalContainer).CommonService.ListXPInstantFeedbackSource(Of ADTIPOIDENTIFICAXpo)("ESTADO=1")
        View.IdentificationTypeJuridicDatasource = XpoServiceEx.Instance(Indigo.TransactionalContainer).CommonService.ListXPInstantFeedbackSource(Of ADTIPOIDENTIFICAXpo)("ESTADO=1")
    End Sub
#End Region

End Class
