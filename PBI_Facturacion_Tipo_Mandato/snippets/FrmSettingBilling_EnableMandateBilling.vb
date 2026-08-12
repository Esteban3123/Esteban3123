' =============================================================================
' Parches listos para FrmSettingBilling.vb
' PBI: Habilitar Facturación Tipo Mandato
' =============================================================================

#Region "A1 — Propiedad (después de ApplyBasicBilling)"

    ''' <summary>
    ''' Habilitar Facturación Tipo Mandato (Si/No). Default: False (No).
    ''' Visible solo cuando ApplyBasicBilling = True.
    ''' </summary>
    Public Property EnableMandateBilling As Boolean Implements ISettingBilling.EnableMandateBilling
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

#Region "A2 — InitializeTuples (DataSource Si/No)"

        ' Agregar junto a las otras asignaciones de listApply:
        INDsleEnableMandateBilling.Properties.DataSource = listApply.ToList()

#End Region

#Region "A3 — HideGroup (visibilidad condicionada)"

    ''' <summary>
    ''' Fragmento a integrar dentro del HideGroup() existente.
    ''' </summary>
    Private Sub HideGroup_EnableMandateBilling_Fragment()
        If ApplyBasicBilling Then
            ' ... existentes Always ...
            INDLciEnableMandateBilling.AllowHide = False
            INDLciEnableMandateBilling.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always
        Else
            ' ... existentes Never ...
            INDLciEnableMandateBilling.AllowHide = True
            INDLciEnableMandateBilling.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never
            EnableMandateBilling = False
        End If
    End Sub

#End Region

#Region "A4 — Load default"

        EnableMandateBilling = False

#End Region

#Region "A5 — CleanControls"

        EnableMandateBilling = False

#End Region

#Region "A6 — LoadControls (lectura entidad)"

                            EnableMandateBilling = .EnableMandateBilling

#End Region

#Region "A7 — AssigningValues (guardado)"

            If ApplyBasicBilling Then
                .EnableMandateBilling = EnableMandateBilling
            Else
                .EnableMandateBilling = False
            End If

#End Region
