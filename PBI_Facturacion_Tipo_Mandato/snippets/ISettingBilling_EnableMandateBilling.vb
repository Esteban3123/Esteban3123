'***********************************************************************
' ISettingBilling.vb — Presentacion.Billing.MVP
' PBI: Habilitar Facturación Tipo Mandato
'***********************************************************************
'
' Buscar este bloque exacto:
'
'    Property ApplyBasicBilling As Boolean
'    ''' <summary>
'    ''' Permite definir la creacion o no, de un pagare cuando no existe anticipo del paciente relacionado con copagos
'    ''' </summary>
'    ''' <returns></returns>
'    Property GeneratePromissoryNote As Boolean
'
' Reemplazar por el bloque de abajo (solo se inserta EnableMandateBilling).
'***********************************************************************

    Property ApplyBasicBilling As Boolean

    ''' <summary>
    ''' Habilitar Facturación Tipo Mandato (Si/No). Default: False (No).
    ''' Solo aplica cuando ApplyBasicBilling = True.
    ''' </summary>
    ''' <returns></returns>
    Property EnableMandateBilling As Boolean

    ''' <summary>
    ''' Permite definir la creacion o no, de un pagare cuando no existe anticipo del paciente relacionado con copagos
    ''' </summary>
    ''' <returns></returns>
    Property GeneratePromissoryNote As Boolean
