# Scraping de Renfe con Selenium

Este es un proyecto de scraping desarrollado en Python con Selenium para recopilar datos de los próximos trenes de Renfe. La información extraída se almacena en una base de datos SQL y se expone a través de una API. Además, he desarrollado una interfaz web en Angular para visualizar los datos obtenidos.

## Tecnologías utilizadas

- **Python** (para el scraping con Selenium)
- **Selenium** (automatización del navegador para extraer los datos)
- **SQL** (base de datos para almacenar los datos de los trenes)
- **Podman** (para contenerizar la base de datos)
- **Isard Virtual Machine** (máquina virtual que facilita la ejecución del entorno)
- **Angular** (para la interfaz web)
- **Node.js & npm** (para ejecutar el frontend y gestionar dependencias)

## Cómo ejecutar el proyecto

Sigo estos pasos para poner en marcha el proyecto en mi entorno y tú también puedes hacerlo:

### Clonar el repositorio

```bash
git  clone https://github.com/Tonaxe/TrenScanner.git
```

### Abrir la máquina virtual de Isard

- Asegúrate de tener la máquina virtual en funcionamiento.
- Usa el contenedor que incluye la base de datos SQL con Podman.

### Instalar dependencias del frontend

```bash
npm install
```

### Iniciar el frontend

```bash
npm start
```

### Levantar la API

- Dirígete a la carpeta donde está el backend.
- Ejecuta el servicio correspondiente para iniciar la API.

Ahora el proyecto estará en funcionamiento y podrás ver los datos extraídos desde la web.
