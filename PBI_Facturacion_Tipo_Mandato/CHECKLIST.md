# Checklist de implementación — Habilitar Facturación Tipo Mandato

## Estado de entregables (este PR)

| Pieza | Estado | Archivo |
|-------|--------|---------|
| Guía maestra | ✅ | [APLICAR.md](APLICAR.md) |
| AC | ✅ | [ACCEPTANCE_CRITERIA.md](ACCEPTANCE_CRITERIA.md) |
| `ISettingBilling` (diff exacto) | ✅ confirmado con código | [snippets/ISettingBilling_APLICAR.md](snippets/ISettingBilling_APLICAR.md) |
| `FrmSettingBilling` (parches A1–A7) | ✅ confirmado con código | [snippets/FrmSettingBilling_APLICAR.md](snippets/FrmSettingBilling_APLICAR.md) |
| Designer | ⚠️ guía (falta pegar Designer) | [snippets/FrmSettingBilling_Designer_APLICAR.md](snippets/FrmSettingBilling_Designer_APLICAR.md) |
| `SettingsBilling` + BD | ⚠️ guía (falta pegar entidad) | [snippets/SettingsBilling_APLICAR.md](snippets/SettingsBilling_APLICAR.md) |
| Gate AC6 | ✅ plantilla | [snippets/Gate_FacturacionMandato.vb](snippets/Gate_FacturacionMandato.vb) |
| SQL | ✅ plantilla | [sql/Add_EnableMandateBilling_SettingsBilling.sql](sql/Add_EnableMandateBilling_SettingsBilling.sql) |

## Orden en el repo corporativo

1. [ ] SQL columna `EnableMandateBilling` (default 0 + backfill)
2. [ ] `SettingsBilling` (+ mapping/SP)
3. [ ] `ISettingBilling`
4. [ ] Designer (`INDsle` + `INDLci`)
5. [ ] `FrmSettingBilling` A1–A7
6. [ ] Gate menús/servicios mandato
7. [ ] QA AC1–AC6 + evidencias ADO

## Bloqueos para diff exacto restante

Para cerrar Designer y entidad al mismo nivel que la interfaz, pegar en el chat:

1. Fragmento de `FrmSettingBilling.Designer.vb` donde esté `INDsleApplyBasicBilling` / layout Información Adicional  
2. `SettingsBilling.vb` (o partial) buscando `ApplyBasicBilling`  
3. (Opcional) SP/Get-Save o mapeo EF de esa tabla
