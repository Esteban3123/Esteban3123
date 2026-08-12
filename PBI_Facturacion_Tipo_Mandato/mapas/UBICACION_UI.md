# Ubicación UI — Habilitar Facturación Tipo Mandato

## Formulario

`FrmSettingBilling` — pestaña **Parámetros de Facturación**

## Segmento

**Información Adicional**

Controles vecinos confirmados en código:

| Label UI | Control / propiedad |
|----------|---------------------|
| Estado de Folio Nuevo | `INDSleStatusFolioNew` |
| Estado de Folio Cerrado | `INDSleStatusFolioClosed` |
| Aplica Facturación Básica | `INDsleApplyBasicBilling` / `ApplyBasicBilling` |
| Entidad Administradora Particulares | `INDSleParticularHealthAdministratorId` |
| Interfaz con Presupuesto | `INDsleBudgetInterface` |
| Control N° Autorización | `INDGleAuthorizationNumberControl` |
| Tipo Bloqueo de Ingreso | `INDGleIncomeLockType` |
| Tiquete Electrónico de Venta | `INDSleElectronicSalesTicket` / `ApplyElectronicSalesTicket` |
| **Habilitar Facturación Tipo Mandato** | **`INDsleEnableMandateBilling` / `EnableMandateBilling`** ← NUEVO |

## Visibilidad

```text
ApplyBasicBilling = True  →  INDLciEnableMandateBilling.Visibility = Always
ApplyBasicBilling = False →  Never + EnableMandateBilling = False
```

Implementado extendiendo `HideGroup()` (ya disparado por `INDsleApplyBasicBilling_EditValueChanged`).
