# APLICAR — ISettingBilling.vb (Presentacion.Billing.MVP)

Archivo confirmado por el usuario. Cambio mínimo: una propiedad nueva.

## Ubicación

Después de:

```vb
    Property ApplyBasicBilling As Boolean
```

Antes de:

```vb
    ''' <summary>
    ''' Permite definir la creacion o no, de un pagare cuando no existe anticipo del paciente relacionado con copagos
    ''' </summary>
    ''' <returns></returns>
    Property GeneratePromissoryNote As Boolean
```

## Diff a aplicar

**Antes:**
```vb
    Property ApplyBasicBilling As Boolean
    ''' <summary>
    ''' Permite definir la creacion o no, de un pagare cuando no existe anticipo del paciente relacionado con copagos
    ''' </summary>
    ''' <returns></returns>
    Property GeneratePromissoryNote As Boolean
```

**Después:**
```vb
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
```

## Checklist

- [ ] Compila `Presentacion.Billing.MVP`
- [ ] `FrmSettingBilling` implementa `ISettingBilling.EnableMandateBilling` (parche A1 de [APLICAR.md](../APLICAR.md))
- [ ] Sin otras propiedades tocadas

## Nota

La interfaz solo declara el contrato. Persistencia = entidad `SettingsBilling` + BD. UI = `FrmSettingBilling` + Designer.
