Option Strict On
Option Explicit On

''' <summary>
''' Snippet de apoyo para PBI 24152 — Editor de Expresiones Vie HCM.
''' Copiar/adaptar al registrador real de Campos IBC del módulo Nómina.
''' No compila solo: depende de las clases del ERP (ExpressionContext, DTO, etc.).
''' </summary>
Public Module RegisterIbcPensionExpressionFields

    Public Const FieldIbcPensionPrimaMedia As String = "IBC Pensión Prima Media"
    Public Const FieldIbcPensionAccai As String = "IBC Pensión ACCAI"

    ''' <summary>
    ''' Publica en el contexto de expresiones los valores de los labels
    ''' "IBC Pensión - CPM" e "IBC Pensión - ACCAI" de IBC - Control Nómina.
    ''' </summary>
    Public Sub AddFields(context As Object, ibcPensionCpm As Decimal?, ibcPensionAccai As Decimal?)
        ' Reemplazar "context.AddField" por el API real del Expression Editor.
        ' Ejemplos de nombres a buscar en el repo:
        '   AddField / RegisterField / Fields.Add / Variables.Add

        AddOrReplace(context, FieldIbcPensionPrimaMedia, If(ibcPensionCpm, 0D))
        AddOrReplace(context, FieldIbcPensionAccai, If(ibcPensionAccai, 0D))
    End Sub

    ''' <summary>
    ''' Variante cuando el valor ya viene del DTO/entidad de liquidación.
    ''' Mapear las propiedades a las del modelo real (post PBI 27042).
    ''' </summary>
    Public Sub AddFieldsFromSettlement(context As Object, settlement As Object)
        Dim cpm = ReadDecimalProperty(settlement, "IbcPensionCpm", "IbcPensionPrimaMedia", "IBCPensionCPM")
        Dim accai = ReadDecimalProperty(settlement, "IbcPensionAccai", "IBCPensionACCAI")
        AddFields(context, cpm, accai)
    End Sub

    Private Sub AddOrReplace(context As Object, displayName As String, value As Decimal)
        ' TODO equipo HCM: implementar contra el catálogo real.
        ' Mantener el mismo formato de token que Campos existentes, p.ej. [IBC Pensión]
        Throw New NotImplementedException(
            $"Registrar Campo '{displayName}' = {value} usando el patrón de IBC Pensión / IBC Salud.")
    End Sub

    Private Function ReadDecimalProperty(source As Object, ParamArray propertyNames As String()) As Decimal?
        If source Is Nothing Then Return Nothing
        Dim t = source.GetType()
        For Each name In propertyNames
            Dim prop = t.GetProperty(name)
            If prop Is Nothing Then Continue For
            Dim raw = prop.GetValue(source)
            If raw Is Nothing Then Return Nothing
            Return Convert.ToDecimal(raw)
        Next
        Return Nothing
    End Function

End Module
