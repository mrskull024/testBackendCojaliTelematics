# Foobar

Proyecto de Backend para Cojali Telematics realizado en .NET Core 9 y MySql con ADO.NET

Instrucciones de ejecución:
- Ejecute el script proporcionado en el repositorio `dbscripts.sql` en su sevidor `MySql`
- En el proyecto `Web.API` editar el archivo `appSettingsDevelopment.json` y cambiar el valor de `DbCn` por los valores validos para conectarse a su servidor `MySql`
- Una vez realizado esos cambios establezca como proyecto de inicio `Web.API` y ejecute el proyecto, posterior en el navegador web de preferencia navegue a la siguiente URL: `https://localhost:7098/swagger/index.html`
