1.) First create a database and table for authentication
2.) Add a Register Controller Class
3.) Next we are working with existing DB, so we need to Scaffold DB, install 'EntityFrameworkCore' and 'entityframeworkcore.tools' and 'Microsoft.EntityFrameworkCore.SqlServer' package
4.) Tools -> Nuget Package Manager -> Package Manager Console
5.) Scaffold-DbContext "Server=localhost;Database=NetCoreAuthentication;user=sa;password=sa@123" Microsoft.EntityFrameworkCore.SqlServer -OutputDir Models ( run this command in package manager console to add existing db to this project)
6.) Cretat Folder Utils and Class Common.cs in it.
7.) Create Post method in RegisterController.cs