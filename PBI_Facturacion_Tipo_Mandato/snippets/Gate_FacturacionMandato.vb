' =============================================================================
' Gate — Facturación Tipo Mandato
' Form/Entidad confirmados: FrmSettingBilling / SettingsBilling
' =============================================================================

''' <summary>
''' True solo si Aplica Facturación Básica = Si Y Habilitar Facturación Tipo Mandato = Si.
''' </summary>
Public Function IsMandateBillingEnabled(settings As SettingsBilling) As Boolean
    If settings Is Nothing Then
        Return False
    End If
    If Not settings.ApplyBasicBilling Then
        Return False
    End If
    Return settings.EnableMandateBilling
End Function

''' <summary>
''' Backend: rechazar operación de mandato si el parámetro es No.
''' </summary>
Public Sub EnsureMandateBillingAllowed(settings As SettingsBilling)
    If Not IsMandateBillingEnabled(settings) Then
        Throw New InvalidOperationException(
            "La Facturación Tipo Mandato no está habilitada para esta unidad operativa. " &
            "Configure el parámetro en Parámetros de Facturación (Información Adicional).")
    End If
End Sub

''' <summary>
''' Presentation: ocultar acciones de mandato.
''' </summary>
Public Sub ApplyMandateBillingUiGate(settings As SettingsBilling, ParamArray mandateActions As Object())
    Dim enabled As Boolean = IsMandateBillingEnabled(settings)
    For Each action In mandateActions
        If action Is Nothing Then Continue For
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
            End Try
        End Try
    Next
End Sub
