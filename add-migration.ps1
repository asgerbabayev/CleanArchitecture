dotnet ef migrations add Initialize_$(Get-Date -Format "ddMMyyyHHmmss") --project  src/Infrastructure --startup-project   src/WebUI --output-dir  Migrations
