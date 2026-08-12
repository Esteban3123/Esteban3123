' =============================================================================
' Gate — Facturación Tipo Mandato
' =============================================================================
' Usar en menús, botones, presenters y servicios que exponen funciones de mandato.
' Fuente de verdad: parámetro HabilitarFacturacionTipoMandato de la UO activa.
' =============================================================================

''' <summary>
''' Indica si la unidad operativa tiene habilitada la facturación tipo mandato.
''' Reglas:
'''  - Default / null → False (NO)
'''  - Si Aplica Facturación Básica = False → False (el parámetro no aplica)
''' </summary>
Public Function IsMandateBillingEnabled(parameters As BillingParametersDto) As Boolean
    If parameters Is Nothing Then
        Return False
    End If

    ' Ajustar nombres de propiedades al DTO real
    Dim appliesBasic As Boolean = False
    If parameters.GetType().GetProperty("AplicaFacturacionBasica") IsNot Nothing Then
        appliesBasic = CBool(CallByName(parameters, "AplicaFacturacionBasica", CallType.Get))
    ElseIf parameters.GetType().GetProperty("AppliesBasicBilling") IsNot Nothing Then
        appliesBasic = CBool(CallByName(parameters, "AppliesBasicBilling", CallType.Get))
    End If

    If Not appliesBasic Then
        Return False
    End If

    Dim enabled As Boolean = False
    If parameters.GetType().GetProperty("EnableMandateBilling") IsNot Nothing Then
        enabled = CBool(CallByName(parameters, "EnableMandateBilling", CallType.Get))
    ElseIf parameters.GetType().GetProperty("HabilitarFacturacionTipoMandato") IsNot Nothing Then
        enabled = CBool(CallByName(parameters, "HabilitarFacturacionTipoMandato", CallType.Get))
    End If

    Return enabled
End Function

''' <summary>
''' Presentation: ocultar acciones de mandato en toolbar/menú.
''' </summary>
Public Sub ApplyMandateBillingUiGate(parameters As BillingParametersDto,
                                     ParamArray mandateActions As Object())
    Dim enabled As Boolean = IsMandateBillingEnabled(parameters)
    For Each action In mandateActions
        If action Is Nothing Then Continue For
        ' DevExpress BarItem / SimpleButton / LayoutControlItem — adaptar:
        Try
            CallByName(action, "Visibility", CallType.Set,
                       If(enabled,
                          DevExpress.XtraBars.BarItemVisibility.Always,
                          DevExpress.XtraBars.BarItemVisibility.Never))
        Catch
            Try
                CallByName(action, "Enabled", CallType.Set, enabled)
                CallByName(action, "Visible", CallType.Set, enabled)
            Catch
                ' Ignorar controles que no expongan esas propiedades
            End Try
        End Try
    Next
End Sub

''' <summary>
''' Backend: rechazar operación de mandato si el parámetro es NO.
''' </summary>
Public Sub EnsureMandateBillingAllowed(parameters As BillingParametersDto)
    If Not IsMandateBillingEnabled(parameters) Then
        Throw New InvalidOperationException(
            "La Facturación Tipo Mandato no está habilitada para esta unidad operativa. " &
            "Configure el parámetro en Parámetros de Facturación (Información Adicional).")
    End If
End Sub

' -----------------------------------------------------------------------------
' DTO ilustrativo (si se crea/extiende en el proyecto de contratos)
' -----------------------------------------------------------------------------
Public Class BillingParametersDto
    Public Property OperatingUnitId As Integer
    Public Property AplicaFacturacionBasica As Boolean
    ''' <summary>Default False = NO</summary>
    Public Property EnableMandateBilling As Boolean = False
End Class
