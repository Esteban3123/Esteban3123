# Acceptance Criteria — Habilitar Facturación Tipo Mandato

Formulario: **Parámetros de Facturación** · Segmento: **Información Adicional**

---

## AC1 — Creación del parámetro

**Dado** que el usuario ingresa al formulario Parámetros de Facturación  
**Y** el campo **Aplica Facturación Básica** tiene valor **SI**  
**Cuando** visualiza el segmento **Información Adicional**  
**Entonces** el sistema muestra el parámetro **Habilitar Facturación Tipo Mandato**.

---

## AC2 — Valores permitidos

**Dado** que el parámetro **Habilitar Facturación Tipo Mandato** está visible  
**Cuando** el usuario abre el listado de opciones  
**Entonces** solo puede seleccionar **Sí** o **No** (SI / NO).

---

## AC3 — Valor predeterminado

**Dado** una unidad operativa / cliente sin configuración previa del parámetro (alta o migración)  
**Cuando** se crea o se consulta por primera vez el registro de parámetros  
**Entonces** el valor predeterminado de **Habilitar Facturación Tipo Mandato** es **No**.

---

## AC4 — Persistencia

**Dado** que el usuario configura **Habilitar Facturación Tipo Mandato** en **Sí** o **No**  
**Y** guarda los Parámetros de Facturación  
**Cuando** cierra la aplicación y vuelve a ingresar al formulario  
**Entonces** el sistema conserva el valor configurado.

---

## AC5 — Visibilidad condicionada

**Dado** que el usuario está en Parámetros de Facturación  
**Cuando** **Aplica Facturación Básica** = **No**  
**Entonces** el parámetro **Habilitar Facturación Tipo Mandato** **no** se postula (no es visible ni editable).

**Cuando** **Aplica Facturación Básica** cambia a **Sí**  
**Entonces** el parámetro se postula nuevamente.

---

## AC6 — Gate funcional (mandato deshabilitado)

**Dado** que **Habilitar Facturación Tipo Mandato** está en **No**  
**(o** Aplica Facturación Básica = No, con efecto equivalente a mandato deshabilitado**)**  
**Cuando** el usuario intenta usar funcionalidades asociadas a Facturación de Mandato  
**Entonces** esas funcionalidades no están disponibles (ocultas y/o bloqueadas por el servicio).

---

## Matriz rápida

| ID | Criterio | Cumple |
|----|----------|--------|
| AC1 | Campo creado en Parámetros de Facturación / Información Adicional | ☐ |
| AC2 | Solo Sí / No | ☐ |
| AC3 | Default No | ☐ |
| AC4 | Persiste tras cerrar y reabrir | ☐ |
| AC5 | Solo visible si Aplica Facturación Básica = Sí | ☐ |
| AC6 | Con No, funciones de mandato no disponibles | ☐ |
