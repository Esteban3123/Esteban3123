' =============================================================================
' PBI — Habilitar Facturación Tipo Mandato
' Formulario: Parámetros de Facturación · Segmento Información Adicional
' =============================================================================
' PLANTILLA VB.NET / DevExpress — adaptar nombres reales del form corporativo.
' Buscar el patrón de "Aplica Facturación Básica" y duplicarlo para este campo.
' =============================================================================

#Region "Propiedad de binding (null-safe)"

''' <summary>
''' Habilitar Facturación Tipo Mandato (SI/NO). Default: False (NO).
''' </summary>
Public Property EnableMandateBilling As Boolean
    Get
        If INDsleEnableMandateBilling.EditValue Is Nothing Then
            Return False
        End If
        Return CBool(INDsleEnableMandateBilling.EditValue)
    End Get
    Set(value As Boolean)
        INDsleEnableMandateBilling.EditValue = value
    End Set
End Property

#End Region

#Region "Default y carga"

''' <summary>
''' Llamar desde CleanControls / Nuevo / valores por defecto del formulario.
''' </summary>
Private Sub ApplyMandateBillingDefaults()
    EnableMandateBilling = False ' NO
    RefreshMandateBillingVisibility()
End Sub

''' <summary>
''' Tras cargar entidad/DTO desde BD.
''' </summary>
Private Sub LoadMandateBillingFromEntity(entity As Object)
    ' Ajustar al tipo real, p.ej. BillingParametersDto
    Dim value As Boolean = False
    Try
        Dim prop = entity.GetType().GetProperty("EnableMandateBilling")
        If prop Is Nothing Then
            prop = entity.GetType().GetProperty("HabilitarFacturacionTipoMandato")
        End If
        If prop IsNot Nothing AndAlso prop.GetValue(entity) IsNot Nothing Then
            value = CBool(prop.GetValue(entity))
        End If
    Catch
        value = False
    End Try
    EnableMandateBilling = value
    RefreshMandateBillingVisibility()
End Sub

#End Region

#Region "Visibilidad condicionada a Aplica Facturación Básica"

''' <summary>
''' True cuando Aplica Facturación Básica = SI.
''' Reutilizar la propiedad/control real del formulario.
''' </summary>
Private Function AppliesBasicBilling() As Boolean
    ' Ejemplos — dejar solo el que coincida con el form:
    ' Return Me.AplicaFacturacionBasica
    ' Return CBool(INDsleAplicaFacturacionBasica.EditValue)
    If INDsleAplicaFacturacionBasica.EditValue Is Nothing Then
        Return False
    End If
    Return CBool(INDsleAplicaFacturacionBasica.EditValue)
End Function

''' <summary>
''' Solo postular el campo cuando Aplica Facturación Básica = SI.
''' Si se oculta, forzar NO para no persistir SI "escondido".
''' </summary>
Private Sub RefreshMandateBillingVisibility()
    Dim visible As Boolean = AppliesBasicBilling()

    If INDLciEnableMandateBilling IsNot Nothing Then
        INDLciEnableMandateBilling.Visibility =
            If(visible,
               DevExpress.XtraLayout.Utils.LayoutVisibility.Always,
               DevExpress.XtraLayout.Utils.LayoutVisibility.Never)
    End If

    INDsleEnableMandateBilling.Enabled = visible

    If Not visible Then
        EnableMandateBilling = False
    End If
End Sub

''' <summary>
''' Enganche al EditValueChanged del combo Aplica Facturación Básica.
''' </summary>
Private Sub INDsleAplicaFacturacionBasica_EditValueChanged(sender As Object, e As EventArgs) _
    Handles INDsleAplicaFacturacionBasica.EditValueChanged
    RefreshMandateBillingVisibility()
End Sub

#End Region

#Region "Designer — checklist (no es código ejecutable completo)"

' 1) Agregar DevExpress.XtraEditors.LookUpEdit  INDsleEnableMandateBilling
' 2) Agregar LayoutControlItem                 INDLciEnableMandateBilling
' 3) Caption / Text = "Habilitar Facturación Tipo Mandato"
' 4) Mismo DataSource SI/NO que Aplica Facturación Básica
' 5) Parent = layout group "Información Adicional"
' 6) Ubicación: después de Tiquete Electrónico de Venta (o al final del segmento)
' 7) Visibility inicial: Never (se activa en runtime si Facturación Básica = SI)

#End Region

#Region "Guardado — incluir en el DTO/entidad que ya persiste el formulario"

Private Sub MapMandateBillingToEntity(entity As Object)
    Dim prop = entity.GetType().GetProperty("EnableMandateBilling")
    If prop Is Nothing Then
        prop = entity.GetType().GetProperty("HabilitarFacturacionTipoMandato")
    End If
    If prop IsNot Nothing Then
        ' Si Facturación Básica = NO, siempre persistir False
        prop.SetValue(entity, If(AppliesBasicBilling(), EnableMandateBilling, False))
    End If
End Sub

#End Region
