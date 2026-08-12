' SettingsBilling (Domain.Entities) — PBI Habilitar Facturación Tipo Mandato
'
' Buscar: Public Property ApplyBasicBilling As Boolean
' Si es auto-property, pegar la auto-property de abajo.
' Si usa backing field + OnPropertyChanged, copiar ESE patrón (ver SettingsBilling_APLICAR.md).

''' <summary>
''' Habilitar Facturación Tipo Mandato (Si/No). Default: False (No).
''' Solo aplica cuando ApplyBasicBilling = True.
''' </summary>
Public Property EnableMandateBilling As Boolean
