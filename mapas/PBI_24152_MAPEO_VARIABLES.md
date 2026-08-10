# Mapeo rápido — PBI 24152

## Variables del Editor de Expresiones

| Variable (Campos) | Token sugerido | Label origen | Ventanilla | Formulario |
|-------------------|----------------|--------------|------------|------------|
| IBC Pensión Prima Media | `[IBC Pensión Prima Media]` | IBC Pensión - CPM | IBC - Control Nómina | Liquidación de Nómina |
| IBC Pensión ACCAI | `[IBC Pensión ACCAI]` | IBC Pensión - ACCAI | IBC - Control Nómina | Liquidación de Nómina |

## Relación con campos IBC ya existentes (referencia UI)

Orden típico en **IBC - Control Nómina**:

1. IBC Periodo  
2. IBC Pensión  
3. **IBC Pensión - CPM** ← alimenta variable Prima Media  
4. **IBC Pensión - ACCAI** ← alimenta variable ACCAI  
5. IBC Salud  
6. IBC ARL / IBC RTF / IBC Cesantías / …

## Fórmulas de ejemplo (post-implementación)

```text
= [IBC Pensión Prima Media]
= [IBC Pensión ACCAI]
= [IBC Pensión Prima Media] + [IBC Pensión ACCAI]
= Salario mínimo * Pensión Prima Media
```

La última solo aplica si `Salario mínimo` y `Pensión Prima Media` ya existen como Campo/Constante en el catálogo.
