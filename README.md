# services.docs
Enterprise document management services

``` powershell
# Restore tools
dotnet tool restore

# Run unit tests (multiple test projects, no threshold)
gci **/TestResults/ | ri -r; dotnet test -c Release -s .runsettings; dotnet reportgenerator -targetdir:coveragereport -reports:**/coverage.cobertura.xml -reporttypes:"html;jsonsummary"; start coveragereport/index.html;

# Run mutation tests and show report
gci **/StrykerOutput/ | ri -r; dotnet stryker -o;
```

## build docker image
``` bash
# The following command passes a sensitive file as a secret to the docker build process (one-time secrets; not needed at run time)
# In this case, it contains the github PAT token to read packages from the private feed :)
docker build -f ".\ne14.services.docs.app\Dockerfile" --force-rm --tag docsservice --secret id=nuget_config_file,src="C:\temp\nuget-docker.golden-path.config" .
```
