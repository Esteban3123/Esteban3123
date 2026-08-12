# Ubicación UI — Habilitar Facturación Tipo Mandato

## Formulario

**Parámetros de Facturación** (módulo Facturación Salud · VIE RCM).

Pestañas típicas del módulo (referencia mockup): Proveedores · Conceptos de Facturación · **Parámetros de Facturación** · Facturación Básica · …

## Segmento

Columna / layout group: **Información Adicional**

Campos vecinos habituales (orden aproximado del mockup):

1. Estado de Folio Nuevo  
2. Estado de Folio Cerrado  
3. **Aplica Facturación Básica** ← dispara visibilidad del nuevo campo  
4. Entidad Administradora Particulares  
5. Interfaz con Presupuesto  
6. Control de Registro de Número de Autorización  
7. Tipo Bloqueo de Ingreso  
8. Tiquete Electrónico de Venta  
9. **Habilitar Facturación Tipo Mandato** ← **NUEVO** (al final del segmento)

## Comportamiento visual

```text
┌─ Información Adicional ─────────────────────────┐
│  ...                                            │
│  Aplica Facturación Básica          [ SI ▼ ]    │
│  ...                                            │
│  Tiquete Electrónico de Venta       [    ▼ ]    │
│  Habilitar Facturación Tipo Mandato [ NO ▼ ]    │  ← solo si Aplica Facturación Básica = SI
└─────────────────────────────────────────────────┘
```

Si **Aplica Facturación Básica** = NO → ocultar la fila 9.
